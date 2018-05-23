namespace PetroClient
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.submit = new System.Windows.Forms.Button();
            this.checkBoxContainer = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.curveGridView = new System.Windows.Forms.DataGridView();
            this.xml_Export = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.messageBox = new System.Windows.Forms.Label();
            this.curveHeader = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.curveGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(535, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(164, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Clent Socket Program";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(97, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(253, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Select the curves to get the details";
            // 
            // submit
            // 
            this.submit.Location = new System.Drawing.Point(151, 170);
            this.submit.Name = "submit";
            this.submit.Size = new System.Drawing.Size(126, 57);
            this.submit.TabIndex = 4;
            this.submit.Text = "Submit";
            this.submit.UseVisualStyleBackColor = true;
            this.submit.Click += new System.EventHandler(this.submit_Click);
            // 
            // checkBoxContainer
            // 
            this.checkBoxContainer.Location = new System.Drawing.Point(372, 44);
            this.checkBoxContainer.Name = "checkBoxContainer";
            this.checkBoxContainer.Size = new System.Drawing.Size(548, 84);
            this.checkBoxContainer.TabIndex = 5;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(342, 170);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(129, 52);
            this.button2.TabIndex = 6;
            this.button2.Text = "Stop Connection";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.stop_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(539, 170);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(121, 52);
            this.button3.TabIndex = 7;
            this.button3.Text = "Start Connection";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.start_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.curveGridView);
            this.panel1.Location = new System.Drawing.Point(88, 284);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(879, 296);
            this.panel1.TabIndex = 8;
            // 
            // curveGridView
            // 
            this.curveGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.curveGridView.Location = new System.Drawing.Point(63, 16);
            this.curveGridView.Name = "curveGridView";
            this.curveGridView.RowTemplate.Height = 28;
            this.curveGridView.Size = new System.Drawing.Size(439, 267);
            this.curveGridView.TabIndex = 0;
            // 
            // xml_Export
            // 
            this.xml_Export.Location = new System.Drawing.Point(708, 170);
            this.xml_Export.Name = "xml_Export";
            this.xml_Export.Size = new System.Drawing.Size(148, 52);
            this.xml_Export.TabIndex = 9;
            this.xml_Export.Text = "Export Grid Data To XML";
            this.xml_Export.UseVisualStyleBackColor = true;
            this.xml_Export.Click += new System.EventHandler(this.xml_Export_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(147, 252);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(170, 20);
            this.label3.TabIndex = 10;
            this.label3.Text = "Selected Curve Details";
            // 
            // messageBox
            // 
            this.messageBox.AutoSize = true;
            this.messageBox.Location = new System.Drawing.Point(650, 252);
            this.messageBox.Name = "messageBox";
            this.messageBox.Size = new System.Drawing.Size(0, 20);
            this.messageBox.TabIndex = 11;
            // 
            // curveHeader
            // 
            this.curveHeader.Location = new System.Drawing.Point(971, 170);
            this.curveHeader.Name = "curveHeader";
            this.curveHeader.Size = new System.Drawing.Size(99, 52);
            this.curveHeader.TabIndex = 12;
            this.curveHeader.Text = "Refresh Header";
            this.curveHeader.UseVisualStyleBackColor = true;
            this.curveHeader.Click += new System.EventHandler(this.curveHeader_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1144, 636);
            this.Controls.Add(this.curveHeader);
            this.Controls.Add(this.messageBox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.xml_Export);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.checkBoxContainer);
            this.Controls.Add(this.submit);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.curveGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button submit;
        private System.Windows.Forms.Panel checkBoxContainer;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView curveGridView;
        private System.Windows.Forms.Button xml_Export;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label messageBox;
        private System.Windows.Forms.Button curveHeader;
    }
}

