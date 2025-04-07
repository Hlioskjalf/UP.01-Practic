namespace Zadanie8
{
    partial class Zadanie8
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
            this.components = new System.ComponentModel.Container();
            this.switchButton = new System.Windows.Forms.Button();
            this.gridPanel = new System.Windows.Forms.Panel();
            this.gridSizeLabel = new System.Windows.Forms.Label();
            this.timeIntervalLabel = new System.Windows.Forms.Label();
            this.reinfectionChanceLabel = new System.Windows.Forms.Label();
            this.gridSizeTextBox = new System.Windows.Forms.TextBox();
            this.timeIntervalTextBox = new System.Windows.Forms.TextBox();
            this.reinfectionChanceTextBox = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // switchButton
            // 
            this.switchButton.BackColor = System.Drawing.Color.DarkGreen;
            this.switchButton.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.switchButton.ForeColor = System.Drawing.SystemColors.ControlText;
            this.switchButton.Location = new System.Drawing.Point(12, 546);
            this.switchButton.Name = "switchButton";
            this.switchButton.Size = new System.Drawing.Size(500, 50);
            this.switchButton.TabIndex = 0;
            this.switchButton.Text = "Start";
            this.switchButton.UseVisualStyleBackColor = false;
            // 
            // gridPanel
            // 
            this.gridPanel.Location = new System.Drawing.Point(12, 12);
            this.gridPanel.Name = "gridPanel";
            this.gridPanel.Size = new System.Drawing.Size(500, 500);
            this.gridPanel.TabIndex = 1;
            // 
            // gridSizeLabel
            // 
            this.gridSizeLabel.AutoSize = true;
            this.gridSizeLabel.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gridSizeLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.gridSizeLabel.Location = new System.Drawing.Point(12, 515);
            this.gridSizeLabel.Name = "gridSizeLabel";
            this.gridSizeLabel.Size = new System.Drawing.Size(86, 25);
            this.gridSizeLabel.TabIndex = 2;
            this.gridSizeLabel.Text = "Size (N):";
            // 
            // timeIntervalLabel
            // 
            this.timeIntervalLabel.AutoSize = true;
            this.timeIntervalLabel.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.timeIntervalLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.timeIntervalLabel.Location = new System.Drawing.Point(167, 515);
            this.timeIntervalLabel.Name = "timeIntervalLabel";
            this.timeIntervalLabel.Size = new System.Drawing.Size(86, 25);
            this.timeIntervalLabel.TabIndex = 3;
            this.timeIntervalLabel.Text = "Interval:";
            // 
            // reinfectionChanceLabel
            // 
            this.reinfectionChanceLabel.AutoSize = true;
            this.reinfectionChanceLabel.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.reinfectionChanceLabel.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.reinfectionChanceLabel.Location = new System.Drawing.Point(322, 515);
            this.reinfectionChanceLabel.Name = "reinfectionChanceLabel";
            this.reinfectionChanceLabel.Size = new System.Drawing.Size(113, 25);
            this.reinfectionChanceLabel.TabIndex = 4;
            this.reinfectionChanceLabel.Text = "Reinfection";
            // 
            // gridSizeTextBox
            // 
            this.gridSizeTextBox.Location = new System.Drawing.Point(104, 520);
            this.gridSizeTextBox.Name = "gridSizeTextBox";
            this.gridSizeTextBox.Size = new System.Drawing.Size(57, 20);
            this.gridSizeTextBox.TabIndex = 5;
            this.gridSizeTextBox.Text = "29";
            // 
            // timeIntervalTextBox
            // 
            this.timeIntervalTextBox.Location = new System.Drawing.Point(259, 520);
            this.timeIntervalTextBox.Name = "timeIntervalTextBox";
            this.timeIntervalTextBox.Size = new System.Drawing.Size(57, 20);
            this.timeIntervalTextBox.TabIndex = 6;
            this.timeIntervalTextBox.Text = "400";
            // 
            // reinfectionChanceTextBox
            // 
            this.reinfectionChanceTextBox.Location = new System.Drawing.Point(441, 518);
            this.reinfectionChanceTextBox.Name = "reinfectionChanceTextBox";
            this.reinfectionChanceTextBox.Size = new System.Drawing.Size(57, 20);
            this.reinfectionChanceTextBox.TabIndex = 7;
            this.reinfectionChanceTextBox.Text = "0.1";
            // 
            // Zadanie8
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(524, 606);
            this.Controls.Add(this.reinfectionChanceTextBox);
            this.Controls.Add(this.timeIntervalTextBox);
            this.Controls.Add(this.gridSizeTextBox);
            this.Controls.Add(this.reinfectionChanceLabel);
            this.Controls.Add(this.timeIntervalLabel);
            this.Controls.Add(this.gridSizeLabel);
            this.Controls.Add(this.gridPanel);
            this.Controls.Add(this.switchButton);
            this.Name = "Zadanie8";
            this.Text = "Infection";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button switchButton;
        private System.Windows.Forms.Panel gridPanel;
        private System.Windows.Forms.Label gridSizeLabel;
        private System.Windows.Forms.Label timeIntervalLabel;
        private System.Windows.Forms.Label reinfectionChanceLabel;
        private System.Windows.Forms.TextBox gridSizeTextBox;
        private System.Windows.Forms.TextBox timeIntervalTextBox;
        private System.Windows.Forms.TextBox reinfectionChanceTextBox;
        private System.Windows.Forms.Timer timer1;
    }
}

