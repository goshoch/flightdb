namespace WinFormsApp1.Controls
{
    partial class SeatPickerControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelSeats = new Panel();
            SuspendLayout();
            // 
            // panelSeats
            // 
            panelSeats.AutoScroll = true;
            panelSeats.Dock = DockStyle.Fill;
            panelSeats.Location = new Point(0, 0);
            panelSeats.Name = "panelSeats";
            panelSeats.Size = new Size(1348, 706);
            panelSeats.TabIndex = 0;
            panelSeats.Paint += panelSeats_Paint;
            // 
            // SeatPickerControl
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightSkyBlue;
            Controls.Add(panelSeats);
            Name = "SeatPickerControl";
            Size = new Size(1348, 706);
            Load += SeatPickerControl_Load;
            ResumeLayout(false);
        }

        #endregion

        private Panel panelSeats;
    }
}
