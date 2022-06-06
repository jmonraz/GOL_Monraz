
namespace GOL_Monraz
{
    partial class Options_Dialog
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
            this.numericUpDownMiliseconds = new System.Windows.Forms.NumericUpDown();
            this.OkButton = new System.Windows.Forms.Button();
            this.cancelButton = new System.Windows.Forms.Button();
            this.numericUpDownWidthCells = new System.Windows.Forms.NumericUpDown();
            this.numericUpDownHeightCells = new System.Windows.Forms.NumericUpDown();
            this.timeInterval = new System.Windows.Forms.Label();
            this.widthCells = new System.Windows.Forms.Label();
            this.heightCells = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMiliseconds)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidthCells)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeightCells)).BeginInit();
            this.SuspendLayout();
            // 
            // numericUpDownMiliseconds
            // 
            this.numericUpDownMiliseconds.Location = new System.Drawing.Point(226, 29);
            this.numericUpDownMiliseconds.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDownMiliseconds.Name = "numericUpDownMiliseconds";
            this.numericUpDownMiliseconds.Size = new System.Drawing.Size(63, 20);
            this.numericUpDownMiliseconds.TabIndex = 0;
            // 
            // OkButton
            // 
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(85, 184);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            // 
            // cancelButton
            // 
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.Location = new System.Drawing.Point(194, 184);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(75, 23);
            this.cancelButton.TabIndex = 2;
            this.cancelButton.Text = "Cancel";
            this.cancelButton.UseVisualStyleBackColor = true;
            // 
            // numericUpDownWidthCells
            // 
            this.numericUpDownWidthCells.Location = new System.Drawing.Point(226, 65);
            this.numericUpDownWidthCells.Name = "numericUpDownWidthCells";
            this.numericUpDownWidthCells.Size = new System.Drawing.Size(63, 20);
            this.numericUpDownWidthCells.TabIndex = 3;
            // 
            // numericUpDownHeightCells
            // 
            this.numericUpDownHeightCells.Location = new System.Drawing.Point(226, 105);
            this.numericUpDownHeightCells.Name = "numericUpDownHeightCells";
            this.numericUpDownHeightCells.Size = new System.Drawing.Size(63, 20);
            this.numericUpDownHeightCells.TabIndex = 4;
            // 
            // timeInterval
            // 
            this.timeInterval.AutoSize = true;
            this.timeInterval.Location = new System.Drawing.Point(68, 36);
            this.timeInterval.Name = "timeInterval";
            this.timeInterval.Size = new System.Drawing.Size(137, 13);
            this.timeInterval.TabIndex = 5;
            this.timeInterval.Text = "Time Interval in Miliseconds";
            // 
            // widthCells
            // 
            this.widthCells.AutoSize = true;
            this.widthCells.Location = new System.Drawing.Point(68, 72);
            this.widthCells.Name = "widthCells";
            this.widthCells.Size = new System.Drawing.Size(128, 13);
            this.widthCells.TabIndex = 6;
            this.widthCells.Text = "Width of Universe in Cells";
            // 
            // heightCells
            // 
            this.heightCells.AutoSize = true;
            this.heightCells.Location = new System.Drawing.Point(68, 107);
            this.heightCells.Name = "heightCells";
            this.heightCells.Size = new System.Drawing.Size(131, 13);
            this.heightCells.TabIndex = 7;
            this.heightCells.Text = "Height of Universe in Cells";
            // 
            // Options_Dialog
            // 
            this.AcceptButton = this.OkButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(348, 237);
            this.Controls.Add(this.heightCells);
            this.Controls.Add(this.widthCells);
            this.Controls.Add(this.timeInterval);
            this.Controls.Add(this.numericUpDownHeightCells);
            this.Controls.Add(this.numericUpDownWidthCells);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.numericUpDownMiliseconds);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Options_Dialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Options Dialog";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownMiliseconds)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownWidthCells)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownHeightCells)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown numericUpDownMiliseconds;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button cancelButton;
        private System.Windows.Forms.NumericUpDown numericUpDownWidthCells;
        private System.Windows.Forms.NumericUpDown numericUpDownHeightCells;
        private System.Windows.Forms.Label timeInterval;
        private System.Windows.Forms.Label widthCells;
        private System.Windows.Forms.Label heightCells;
    }
}