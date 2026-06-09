namespace WinFormsApp1
{
    partial class TicketPurchaseForm
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
            seatPickerControl1 = new Controls.SeatPickerControl();
            labelSelectedSeat = new Label();
            button1 = new Button();
            button2 = new Button();
            panel1 = new Panel();
            label1 = new Label();
            panel2 = new Panel();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // seatPickerControl1
            // 
            seatPickerControl1.AutoScroll = true;
            seatPickerControl1.BackColor = Color.LightSkyBlue;
            seatPickerControl1.Dock = DockStyle.Fill;
            seatPickerControl1.Location = new Point(10, 10);
            seatPickerControl1.Name = "seatPickerControl1";
            seatPickerControl1.Size = new Size(760, 280);
            seatPickerControl1.TabIndex = 0;
            seatPickerControl1.Load += seatPickerControl1_Load;
            // 
            // labelSelectedSeat
            // 
            labelSelectedSeat.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            labelSelectedSeat.AutoSize = true;
            labelSelectedSeat.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            labelSelectedSeat.Location = new Point(20, 410);
            labelSelectedSeat.Name = "labelSelectedSeat";
            labelSelectedSeat.Size = new Size(144, 28);
            labelSelectedSeat.TabIndex = 1;
            labelSelectedSeat.Text = "Selected Seat:";
            labelSelectedSeat.Click += label1_Click;
            // 
            // button1
            // 
            button1.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button1.BackColor = Color.FromArgb(0, 123, 255);
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            button1.ForeColor = Color.White;
            button1.Location = new Point(551, 400);
            button1.Name = "button1";
            button1.Size = new Size(100, 35);
            button1.TabIndex = 2;
            button1.Text = "Book";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            button2.BackColor = Color.Crimson;
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button2.ForeColor = Color.White;
            button2.Location = new Point(669, 400);
            button2.Name = "button2";
            button2.Size = new Size(100, 35);
            button2.TabIndex = 3;
            button2.Text = "Exit";
            button2.UseVisualStyleBackColor = false;
            button2.Click += button2_Click;
            // 
            // panel1
            // 
            panel1.BackColor = Color.FromArgb(0, 123, 255);
            panel1.Controls.Add(label1);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(800, 65);
            panel1.TabIndex = 4;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
            label1.ForeColor = Color.White;
            label1.Location = new Point(20, 15);
            label1.Name = "label1";
            label1.Size = new Size(242, 41);
            label1.TabIndex = 0;
            label1.Text = "Select Your Seat";
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel2.Controls.Add(seatPickerControl1);
            panel2.Location = new Point(10, 80);
            panel2.Name = "panel2";
            panel2.Padding = new Padding(10);
            panel2.Size = new Size(780, 300);
            panel2.TabIndex = 5;
            // 
            // TicketPurchaseForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightSkyBlue;
            ClientSize = new Size(800, 450);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(labelSelectedSeat);
            Font = new Font("Segoe UI", 11F);
            Name = "TicketPurchaseForm";
            Text = "Book a Ticket";
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls.SeatPickerControl seatPickerControl1;
        private System.Windows.Forms.Label labelSelectedSeat;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
    }
}
