namespace SteadyHandVersion5
{
    partial class ShootingRange
    {
        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ShootingRange));
            this.label1 = new System.Windows.Forms.Label();
            this.vystrel1 = new SteadyHandVersion5.Target();
            this.vystrel2 = new SteadyHandVersion5.Target();
            this.vystrel3 = new SteadyHandVersion5.Target();
            this.vystrel4 = new SteadyHandVersion5.Target();
            this.vystrel5 = new SteadyHandVersion5.Target();
            this.vystrel6 = new SteadyHandVersion5.Target();
            this.vystrel7 = new SteadyHandVersion5.Target();
            this.vystrel8 = new SteadyHandVersion5.Target();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(284, 373);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // vystrel1
            // 
            this.vystrel1.BackColor = System.Drawing.Color.Transparent;
            this.vystrel1.Location = new System.Drawing.Point(164, 85);
            this.vystrel1.Name = "vystrel1";
            this.vystrel1.Size = new System.Drawing.Size(80, 80);
            this.vystrel1.TabIndex = 2;
            this.vystrel1.Text = "vystrel1";
            this.vystrel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TargetHit);
            // 
            // vystrel2
            // 
            this.vystrel2.BackColor = System.Drawing.Color.Transparent;
            this.vystrel2.Location = new System.Drawing.Point(305, 118);
            this.vystrel2.Name = "vystrel2";
            this.vystrel2.Size = new System.Drawing.Size(80, 80);
            this.vystrel2.TabIndex = 3;
            this.vystrel2.Text = "vystrel2";
            this.vystrel2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TargetHit);
            // 
            // vystrel3
            // 
            this.vystrel3.BackColor = System.Drawing.Color.Transparent;
            this.vystrel3.Location = new System.Drawing.Point(458, 72);
            this.vystrel3.Name = "vystrel3";
            this.vystrel3.Size = new System.Drawing.Size(80, 80);
            this.vystrel3.TabIndex = 4;
            this.vystrel3.Text = "vystrel3";
            this.vystrel3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TargetHit);
            // 
            // vystrel4
            // 
            this.vystrel4.BackColor = System.Drawing.Color.Transparent;
            this.vystrel4.Location = new System.Drawing.Point(635, 122);
            this.vystrel4.Name = "vystrel4";
            this.vystrel4.Size = new System.Drawing.Size(80, 80);
            this.vystrel4.TabIndex = 5;
            this.vystrel4.Text = "vystrel4";
            this.vystrel4.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TargetHit);
            // 
            // vystrel5
            // 
            this.vystrel5.BackColor = System.Drawing.Color.Transparent;
            this.vystrel5.Location = new System.Drawing.Point(153, 251);
            this.vystrel5.Name = "vystrel5";
            this.vystrel5.Size = new System.Drawing.Size(80, 80);
            this.vystrel5.TabIndex = 6;
            this.vystrel5.Text = "vystrel5";
            this.vystrel5.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TargetHit);
            // 
            // vystrel6
            // 
            this.vystrel6.BackColor = System.Drawing.Color.Transparent;
            this.vystrel6.Location = new System.Drawing.Point(316, 285);
            this.vystrel6.Name = "vystrel6";
            this.vystrel6.Size = new System.Drawing.Size(80, 80);
            this.vystrel6.TabIndex = 7;
            this.vystrel6.Text = "vystrel6";
            this.vystrel6.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TargetHit);
            // 
            // vystrel7
            // 
            this.vystrel7.BackColor = System.Drawing.Color.Transparent;
            this.vystrel7.Location = new System.Drawing.Point(491, 251);
            this.vystrel7.Name = "vystrel7";
            this.vystrel7.Size = new System.Drawing.Size(80, 80);
            this.vystrel7.TabIndex = 8;
            this.vystrel7.Text = "vystrel7";
            this.vystrel7.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TargetHit);
            // 
            // vystrel8
            // 
            this.vystrel8.BackColor = System.Drawing.Color.Transparent;
            this.vystrel8.Location = new System.Drawing.Point(665, 259);
            this.vystrel8.Name = "vystrel8";
            this.vystrel8.Size = new System.Drawing.Size(80, 80);
            this.vystrel8.TabIndex = 9;
            this.vystrel8.Text = "vystrel8";
            this.vystrel8.MouseClick += new System.Windows.Forms.MouseEventHandler(this.TargetHit);
            // 
            // ShootingRange
            // 
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(834, 401);
            this.Controls.Add(this.vystrel8);
            this.Controls.Add(this.vystrel7);
            this.Controls.Add(this.vystrel6);
            this.Controls.Add(this.vystrel5);
            this.Controls.Add(this.vystrel4);
            this.Controls.Add(this.vystrel3);
            this.Controls.Add(this.vystrel2);
            this.Controls.Add(this.vystrel1);
            this.Controls.Add(this.label1);
            this.Name = "ShootingRange";
            this.Text = "Тир";
            this.Load += new System.EventHandler(this.Targets_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private Target vystrel1;
        private Target vystrel2;
        private Target vystrel3;
        private Target vystrel4;
        private Target vystrel5;
        private Target vystrel6;
        private Target vystrel7;
        private Target vystrel8;
    }
}