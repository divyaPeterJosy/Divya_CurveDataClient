#region Assemblies
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
#endregion


namespace PetroClient
{
    public partial class curveDisplay : Form
    {
        System.Net.Sockets.TcpClient clientSocket;
        IPAddress ip = Dns.GetHostEntry("localhost").AddressList[0];
        CancellationTokenSource tokenSource;

        public curveDisplay()
        {            
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            StartConnection();            
            List<string> curveHeader = GetResponse("CurveHeader").Split(',').ToList();
            GenerateDynamicCheckbox(curveHeader);
            messageBox.ForeColor = Color.Red;
        }        


        /// <summary>
        /// Submit button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void submit_Click(object sender, EventArgs e)
        {
            curveGridView.Columns.Clear();
            messageBox.Text = "";            
            List<string> curveSelected = CurveSelected();
            if(curveSelected.Count > 0 && clientSocket != null && clientSocket.Connected)
            { 
                //to cancel a task, if connection stop requested by client.
                tokenSource = new CancellationTokenSource();
                var token = tokenSource.Token;

                int gridColumn = curveSelected.Count + 1;
                curveGridView.ColumnCount = gridColumn;

                curveGridView.Columns[0].Name = "Index";
                curveGridView.Columns[0].ValueType = typeof(long);
                for (int i = 1; i <= curveSelected.Count; i++)
                {
                    curveGridView.Columns[i].Name = curveSelected[i - 1];
                    curveGridView.Columns[i].ValueType = typeof(double);
                }
                curveGridView.AutoResizeColumns();
                curveGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                //to execute the function using thread
                Task.Run(() =>
                {
                    string indexList = GetResponse("Index");

                    WriteToDataGrid(indexList, "Index");
                    for (int i = 0; i < curveSelected.Count; i++)
                    {
                        if (clientSocket.Connected)
                        {
                            string returnData = GetResponse(curveSelected[i]);
                            WriteToDataGrid(returnData, curveSelected[i]);
                        }
                    }
                }, token);
            }
            else
            {
                if(clientSocket !=null || clientSocket.Connected)
                {
                    messageBox.Text = "Please select any curve for dispaying data";                   
                }
                else
                {
                    messageBox.Text = "Please start the connection";
                }                
            }            
        }

        /// <summary>
        /// To stop connection click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stop_Click(object sender, EventArgs e)
        {
            // to check for an active task and stop if exist
            if (tokenSource != null && !tokenSource.IsCancellationRequested)
            {
                tokenSource.Cancel();
                tokenSource.Dispose();
                tokenSource = null;
            }
            if (clientSocket.Connected)
            {
                clientSocket.Close();                
            }
        }

        /// <summary>
        /// Start connection click handler
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void start_Click(object sender, EventArgs e)
        {
            if (clientSocket != null && !clientSocket.Connected)
            {
                messageBox.Text = "";
                StartConnection();                
            }
        }

        /// <summary>
        /// to refresh check box 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void curveHeader_Click(object sender, EventArgs e)
        {
            if (clientSocket != null && clientSocket.Connected)
            {
                List<string> curveHeader = GetResponse("CurveHeader").Split(',').ToList();
                GenerateDynamicCheckbox(curveHeader);
            }
            else
            {
                messageBox.Text = "Please start the server first and click Start Connection button";
            }
            
        }

        /// <summary>
        /// To export grid data to xml file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xml_Export_Click(object sender, EventArgs e)
        {
            if(curveGridView != null && curveGridView.Columns.Count > 0 && curveGridView.Rows.Count >0)
            {
                string curveXmlPath = @"D:\XMLCurve\";
                if (!Directory.Exists(curveXmlPath))
                {
                    Directory.CreateDirectory(curveXmlPath);
                }      

                XmlTextWriter writer = new XmlTextWriter(curveXmlPath + "Curves.xml", System.Text.Encoding.UTF8);
                writer.WriteStartDocument(true);
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 2;
                writer.WriteStartElement("log");

                DataTable gridData = new DataTable();
                List<string> curveHeader = new List<string>();

                foreach (DataGridViewColumn col in curveGridView.Columns)
                {
                    gridData.Columns.Add(col.Name);
                    curveHeader.Add(col.Name);
                    var maxIndex = curveGridView.Rows.Cast<DataGridViewRow>()
                            .Max(r => Convert.ToDouble(string.IsNullOrEmpty(Convert.ToString(r.Cells[col.Name].Value)) ? 0 : r.Cells[col.Name].Value));

                    var minIndex = curveGridView.Rows.Cast<DataGridViewRow>()
                            .Min(r => Convert.ToDouble(string.IsNullOrEmpty(Convert.ToString(r.Cells[col.Name].Value)) ? 0 : r.Cells[col.Name].Value));

                    createNode(col.ValueType.ToString(), col.Name, Convert.ToInt32(maxIndex), Convert.ToInt32(minIndex), "logCurveInfo", "typeLogData", writer);
                }

                writer.WriteStartElement("logData");
                createNode("", string.Join(",", curveHeader), 0, 0, "header", "", writer);
                List<string> curveData;
                foreach (DataGridViewRow row in curveGridView.Rows)
                {
                    if(row.Cells[0].Value != null)
                    {
                        curveData = new List<string>();
                        DataRow dRow = gridData.NewRow();
                        foreach (DataGridViewCell cell in row.Cells)
                        {
                            dRow[cell.ColumnIndex] = cell.Value;
                            curveData.Add(Convert.ToString(cell.Value));
                        }
                        gridData.Rows.Add(dRow);
                        createNode("", string.Join(",", curveData), 0, 0, "data", "", writer);
                    }                    
                    
                }
                writer.WriteEndElement();
                writer.WriteEndElement();
                writer.WriteEndDocument();
                writer.Close();
            }
            else
            {
                messageBox.Text = "Please Select Curves Details First";
            }

        }


        /// <summary>
        /// Curve Selected by client
        /// </summary>
        /// <returns></returns>
        private List<string> CurveSelected()
        {
            List<string> curveSelected = new List<string>();
            int count = 0;
            foreach (Control control in checkBoxContainer.Controls)
            {
                count++;
                CheckBox cb = checkBoxContainer.Controls["cb_" + count] as CheckBox;
                if (cb != null && cb.Checked)
                {
                    curveSelected.Add(cb.Text);
                }
            }
            return curveSelected;
        }

        /// <sumTmary>
        /// To get the response from server
        /// </summary>
        /// <param name="selectedValue"></param>
        /// <returns></returns>
        private string GetResponse(string selectedValue)
        {
            try
            {
                NetworkStream serverStream = clientSocket.GetStream();
                byte[] outStream = System.Text.Encoding.ASCII.GetBytes(selectedValue + "$");
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();

                byte[] inStream = new byte[1500];
                serverStream.Read(inStream, 0, inStream.Length);
                return System.Text.Encoding.ASCII.GetString(inStream).Replace("\0", "");
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        /// <summary>
        /// To add data to text box
        /// </summary>
        /// <param name="mesg"></param>
        private void WriteToDataGrid(string response, string curve = "")
        {
            //To check whether the action is performed by main thread or child thread
            if (InvokeRequired)
            {
                //to execute the code in main thread context
                Invoke((MethodInvoker)delegate ()
                {                    
                    if(curve != "")
                    {
                        AddDataToGrid(response, curve);
                    }
                });
            }
            else
            {                
                if (curve != "" )
                {
                    AddDataToGrid(response, curve);
                }
            }
        }

        /// <summary>
        /// To add data to grid view
        /// </summary>
        /// <param name="response"></param>
        /// <param name="curve"></param>
        private void AddDataToGrid(string response, string curve)
        {
            string[] curves = response.Split(',');
            int i = 0;
            if (curve == "Index")
            {
                foreach (string curveValue in curves)
                {
                    curveGridView.Rows.Add(curveValue);
                }
            }
            else
            {
                foreach (string curveValue in curves)
                {
                    curveGridView.Rows[i].Cells[curve].Value = curveValue;
                    i++;
                }
            }
        }

        /// <summary>
        /// To generate check box based on the Curve header 
        /// </summary>
        /// <param name="mylist"></param>
        private void GenerateDynamicCheckbox(List<string> mylist)
        {
            CheckBox box;
            for (int i = 1; i < mylist.Count; i++)
            {
                box = new CheckBox();
                box.Name = "cb_" + i;
                box.Tag = mylist[i];
                box.Text = mylist[i];
                box.AutoSize = true;
                box.Location = new Point(i * 50, 40); //horizontal
                checkBoxContainer.Controls.Add(box);
            }
        }
        
        /// <summary>
        /// To start TCP Client connection
        /// </summary>
        private void StartConnection()
        {
            try
            {
                clientSocket = new System.Net.Sockets.TcpClient();
                clientSocket.Connect("127.0.0.1", 7633);
            }
            catch(Exception ex)
            {
                messageBox.Text = "Please start the server";
            }            
        }

        /// <summary>
        /// To add child node to xml file
        /// </summary>
        /// <param name="nodeDataType"></param>
        /// <param name="nodeData"></param>
        /// <param name="maxIndex"></param>
        /// <param name="minIndex"></param>
        /// <param name="childNode"></param>
        /// <param name="childNode2"></param>
        /// <param name="writer"></param>
        private void createNode(string nodeDataType, string nodeData, int maxIndex, int minIndex, string childNode, string childNode2, XmlTextWriter writer)
        {
            writer.WriteStartElement(childNode);
            if(nodeDataType != "")
            {
                writer.WriteAttributeString("id", nodeData);
            }
            else
            {
                writer.WriteString(nodeData);
            }            

            if (maxIndex > 0 && nodeData!="Index")
            {
                writer.WriteStartElement("minIndex");
                writer.WriteString(minIndex.ToString());
                writer.WriteEndElement();
                writer.WriteStartElement("maxIndex");
                writer.WriteString(maxIndex.ToString());
                writer.WriteEndElement();
            }
            if(childNode2 != "")
            {
                writer.WriteStartElement(childNode2);
                writer.WriteString(nodeDataType);
                writer.WriteEndElement();
            }
                        
            writer.WriteEndElement();
        }

        
    }
}
