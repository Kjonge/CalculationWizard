namespace CalculationWizard
{
    partial class CalcGroupfrm
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
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btnAddCalcGroup = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnRemoveCalcGroup = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(12, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(947, 484);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // btnAddCalcGroup
            // 
            this.btnAddCalcGroup.Location = new System.Drawing.Point(36, 519);
            this.btnAddCalcGroup.Name = "btnAddCalcGroup";
            this.btnAddCalcGroup.Size = new System.Drawing.Size(218, 39);
            this.btnAddCalcGroup.TabIndex = 1;
            this.btnAddCalcGroup.Text = "Add the Calculation Group";
            this.btnAddCalcGroup.UseVisualStyleBackColor = true;
            this.btnAddCalcGroup.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(806, 519);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(153, 39);
            this.btnExit.TabIndex = 2;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = true;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // btnRemoveCalcGroup
            // 
            this.btnRemoveCalcGroup.Location = new System.Drawing.Point(273, 519);
            this.btnRemoveCalcGroup.Name = "btnRemoveCalcGroup";
            this.btnRemoveCalcGroup.Size = new System.Drawing.Size(252, 39);
            this.btnRemoveCalcGroup.TabIndex = 3;
            this.btnRemoveCalcGroup.Text = "Remove the Calculation Group";
            this.btnRemoveCalcGroup.UseVisualStyleBackColor = true;
            this.btnRemoveCalcGroup.Click += new System.EventHandler(this.btnRemoveCalcGroup_Click);
            // 
            // CalcGroupfrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(971, 586);
            this.Controls.Add(this.btnRemoveCalcGroup);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnAddCalcGroup);
            this.Controls.Add(this.richTextBox1);
            this.Name = "CalcGroupfrm";
            this.Text = "Calculation group wizard";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btnAddCalcGroup;
        private System.Windows.Forms.Button btnExit;
        private System.Windows.Forms.Button btnRemoveCalcGroup;
    }
}

