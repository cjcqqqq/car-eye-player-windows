namespace CarEyePlayerDemo
{
	partial class FrmMain
	{
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.carEyeViewer2 = new CarEyePlayerDemo.Player.CarEyeViewer();
			this.carEyeViewer3 = new CarEyePlayerDemo.Player.CarEyeViewer();
			this.carEyeViewer1 = new CarEyePlayerDemo.Player.CarEyeViewer();
			this.viewer1 = new CarEyePlayerDemo.Player.CarEyeViewer();
			this.SuspendLayout();
			// 
			// carEyeViewer2
			// 
			this.carEyeViewer2.BackColor = System.Drawing.Color.Firebrick;
			this.carEyeViewer2.Location = new System.Drawing.Point(448, 300);
			this.carEyeViewer2.Name = "carEyeViewer2";
			this.carEyeViewer2.Size = new System.Drawing.Size(450, 300);
			this.carEyeViewer2.TabIndex = 3;
			this.carEyeViewer2.Url = "rtmp://www.car-eye.cn:10085/live/123";
			this.carEyeViewer2.Click += new System.EventHandler(this.viewer1_Click);
			this.carEyeViewer2.DoubleClick += new System.EventHandler(this.viewer1_DoubleClick);
			// 
			// carEyeViewer3
			// 
			this.carEyeViewer3.BackColor = System.Drawing.Color.Firebrick;
			this.carEyeViewer3.Location = new System.Drawing.Point(0, 300);
			this.carEyeViewer3.Name = "carEyeViewer3";
			this.carEyeViewer3.Size = new System.Drawing.Size(450, 300);
			this.carEyeViewer3.TabIndex = 2;
			this.carEyeViewer3.Url = "rtmp://live.hkstv.hk.lxdns.com/live/hks";
			this.carEyeViewer3.Click += new System.EventHandler(this.viewer1_Click);
			this.carEyeViewer3.DoubleClick += new System.EventHandler(this.viewer1_DoubleClick);
			// 
			// carEyeViewer1
			// 
			this.carEyeViewer1.BackColor = System.Drawing.Color.Firebrick;
			this.carEyeViewer1.Location = new System.Drawing.Point(448, 0);
			this.carEyeViewer1.Name = "carEyeViewer1";
			this.carEyeViewer1.Size = new System.Drawing.Size(450, 300);
			this.carEyeViewer1.TabIndex = 1;
			this.carEyeViewer1.Url = "rtmp://www.car-eye.cn:10085/live/13510671870&channel=1";
			this.carEyeViewer1.Click += new System.EventHandler(this.viewer1_Click);
			this.carEyeViewer1.DoubleClick += new System.EventHandler(this.viewer1_DoubleClick);
			// 
			// viewer1
			// 
			this.viewer1.BackColor = System.Drawing.Color.Firebrick;
			this.viewer1.Location = new System.Drawing.Point(0, 0);
			this.viewer1.Name = "viewer1";
			this.viewer1.Size = new System.Drawing.Size(450, 300);
			this.viewer1.TabIndex = 0;
			this.viewer1.Url = "rtmp://www.car-eye.cn:10085/live/123";
			this.viewer1.Click += new System.EventHandler(this.viewer1_Click);
			this.viewer1.DoubleClick += new System.EventHandler(this.viewer1_DoubleClick);
			// 
			// FrmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(899, 603);
			this.Controls.Add(this.carEyeViewer2);
			this.Controls.Add(this.carEyeViewer3);
			this.Controls.Add(this.carEyeViewer1);
			this.Controls.Add(this.viewer1);
			this.Name = "FrmMain";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "CarEye player演示程序";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
			this.Load += new System.EventHandler(this.FrmMain_Load);
			this.SizeChanged += new System.EventHandler(this.FrmMain_SizeChanged);
			this.ResumeLayout(false);

		}

		#endregion

		private Player.CarEyeViewer viewer1;
		private Player.CarEyeViewer carEyeViewer1;
		private Player.CarEyeViewer carEyeViewer2;
		private Player.CarEyeViewer carEyeViewer3;
	}
}

