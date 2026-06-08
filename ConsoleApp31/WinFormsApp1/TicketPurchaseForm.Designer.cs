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
            SuspendLayout();
            // 
            // seatPickerControl1
            // 
            seatPickerControl1.AutoScroll = true;
            seatPickerControl1.BackColor = Color.LightSkyBlue;
            seatPickerControl1.Dock = DockStyle.Fill;
            seatPickerControl1.Location = new Point(0, 0);
            seatPickerControl1.Name = "seatPickerControl1";
            seatPickerControl1.Size = new Size(800, 450);
            seatPickerControl1.TabIndex = 0;
            seatPickerControl1.Load += seatPickerControl1_Load;
            // 
            // labelSelectedSeat
            // 
            labelSelectedSeat.AutoSize = true;
            labelSelectedSeat.Location = new Point(638, 421);
            labelSelectedSeat.Name = "labelSelectedSeat";
            labelSelectedSeat.Size = new Size(0, 20);
            labelSelectedSeat.TabIndex = 1;
            labelSelectedSeat.Click += label1_Click;
            // 
            // button1
            // 
            button1.Location = new Point(122, 412);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 2;
            button1.Text = "Book";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(12, 412);
            button2.Name = "button2";
            button2.Size = new Size(94, 29);
            button2.TabIndex = 3;
            button2.Text = "Cancel";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // TicketPurchaseForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.LightSkyBlue;
            ClientSize = new Size(800, 450);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(labelSelectedSeat);
            Controls.Add(seatPickerControl1);
            Name = "TicketPurchaseForm";
            Text = "TicketPurchaseForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Controls.SeatPickerControl seatPickerControl1;
        private Label labelSelectedSeat;
        private Button button1;
        private Button button2;
    }
}