namespace CarEyePlayerDemo.Player
{
	partial class CarEyeViewer
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

		#region 组件设计器生成的代码

		/// <summary> 
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.pnlBase = new System.Windows.Forms.Panel();
			this.trackVolume = new System.Windows.Forms.TrackBar();
			this.lblTotalTime = new System.Windows.Forms.Label();
			this.lblCurTime = new System.Windows.Forms.Label();
			this.pgrPlay = new System.Windows.Forms.TrackBar();
			this.btnRecord = new System.Windows.Forms.Button();
			this.btnScreenshot = new System.Windows.Forms.Button();
			this.btnFast = new System.Windows.Forms.Button();
			this.btnSlow = new System.Windows.Forms.Button();
			this.btnPlay = new System.Windows.Forms.Button();
			this.txtUrl = new System.Windows.Forms.TextBox();
			this.lblView = new CarEyePlayerDemo.Player.CarEyeLabel();
			this.bkWorker = new System.ComponentModel.BackgroundWorker();
			this.tmrTip = new System.Windows.Forms.Timer(this.components);
			this.tmrPlay = new System.Windows.Forms.Timer(this.components);
			this.pnlBase.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackVolume)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pgrPlay)).BeginInit();
			this.SuspendLayout();
			// 
			// pnlBase
			// 
			this.pnlBase.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pnlBase.BackColor = System.Drawing.SystemColors.Control;
			this.pnlBase.Controls.Add(this.trackVolume);
			this.pnlBase.Controls.Add(this.lblTotalTime);
			this.pnlBase.Controls.Add(this.lblCurTime);
			this.pnlBase.Controls.Add(this.pgrPlay);
			this.pnlBase.Controls.Add(this.btnRecord);
			this.pnlBase.Controls.Add(this.btnScreenshot);
			this.pnlBase.Controls.Add(this.btnFast);
			this.pnlBase.Controls.Add(this.btnSlow);
			this.pnlBase.Controls.Add(this.btnPlay);
			this.pnlBase.Controls.Add(this.txtUrl);
			this.pnlBase.Controls.Add(this.lblView);
			this.pnlBase.Location = new System.Drawing.Point(1, 1);
			this.pnlBase.Name = "pnlBase";
			this.pnlBase.Size = new System.Drawing.Size(454, 342);
			this.pnlBase.TabIndex = 1;
			// 
			// trackVolume
			// 
			this.trackVolume.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.trackVolume.AutoSize = false;
			this.trackVolume.Enabled = false;
			this.trackVolume.LargeChange = 1;
			this.trackVolume.Location = new System.Drawing.Point(429, 31);
			this.trackVolume.Name = "trackVolume";
			this.trackVolume.Orientation = System.Windows.Forms.Orientation.Vertical;
			this.trackVolume.Size = new System.Drawing.Size(21, 267);
			this.trackVolume.TabIndex = 6;
			this.trackVolume.TickStyle = System.Windows.Forms.TickStyle.None;
			this.trackVolume.ValueChanged += new System.EventHandler(this.trackVolume_ValueChanged);
			// 
			// lblTotalTime
			// 
			this.lblTotalTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lblTotalTime.AutoSize = true;
			this.lblTotalTime.Location = new System.Drawing.Point(414, 324);
			this.lblTotalTime.Name = "lblTotalTime";
			this.lblTotalTime.Size = new System.Drawing.Size(35, 12);
			this.lblTotalTime.TabIndex = 5;
			this.lblTotalTime.Text = "00:00";
			this.lblTotalTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblCurTime
			// 
			this.lblCurTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblCurTime.AutoSize = true;
			this.lblCurTime.Location = new System.Drawing.Point(7, 324);
			this.lblCurTime.Name = "lblCurTime";
			this.lblCurTime.Size = new System.Drawing.Size(35, 12);
			this.lblCurTime.TabIndex = 5;
			this.lblCurTime.Text = "00:00";
			this.lblCurTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// pgrPlay
			// 
			this.pgrPlay.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.pgrPlay.AutoSize = false;
			this.pgrPlay.Enabled = false;
			this.pgrPlay.Location = new System.Drawing.Point(2, 300);
			this.pgrPlay.Name = "pgrPlay";
			this.pgrPlay.Size = new System.Drawing.Size(449, 18);
			this.pgrPlay.TabIndex = 2;
			this.pgrPlay.TickStyle = System.Windows.Forms.TickStyle.None;
			this.pgrPlay.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pgrPlay_MouseDown);
			this.pgrPlay.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pgrPlay_MouseUp);
			// 
			// btnRecord
			// 
			this.btnRecord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRecord.Enabled = false;
			this.btnRecord.Location = new System.Drawing.Point(411, 2);
			this.btnRecord.Name = "btnRecord";
			this.btnRecord.Size = new System.Drawing.Size(38, 23);
			this.btnRecord.TabIndex = 3;
			this.btnRecord.Text = "录像";
			this.btnRecord.UseVisualStyleBackColor = true;
			this.btnRecord.Click += new System.EventHandler(this.btnRecord_Click);
			// 
			// btnScreenshot
			// 
			this.btnScreenshot.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnScreenshot.Enabled = false;
			this.btnScreenshot.Location = new System.Drawing.Point(370, 2);
			this.btnScreenshot.Name = "btnScreenshot";
			this.btnScreenshot.Size = new System.Drawing.Size(38, 23);
			this.btnScreenshot.TabIndex = 2;
			this.btnScreenshot.Text = "截图";
			this.btnScreenshot.UseVisualStyleBackColor = true;
			this.btnScreenshot.Click += new System.EventHandler(this.btnScreenshot_Click);
			// 
			// btnFast
			// 
			this.btnFast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFast.Enabled = false;
			this.btnFast.Location = new System.Drawing.Point(329, 2);
			this.btnFast.Name = "btnFast";
			this.btnFast.Size = new System.Drawing.Size(38, 23);
			this.btnFast.TabIndex = 1;
			this.btnFast.Text = "快进";
			this.btnFast.UseVisualStyleBackColor = true;
			this.btnFast.Click += new System.EventHandler(this.btnFast_Click);
			// 
			// btnSlow
			// 
			this.btnSlow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSlow.Enabled = false;
			this.btnSlow.Location = new System.Drawing.Point(288, 2);
			this.btnSlow.Name = "btnSlow";
			this.btnSlow.Size = new System.Drawing.Size(38, 23);
			this.btnSlow.TabIndex = 1;
			this.btnSlow.Text = "慢放";
			this.btnSlow.UseVisualStyleBackColor = true;
			this.btnSlow.Click += new System.EventHandler(this.btnSlow_Click);
			// 
			// btnPlay
			// 
			this.btnPlay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPlay.Location = new System.Drawing.Point(247, 2);
			this.btnPlay.Name = "btnPlay";
			this.btnPlay.Size = new System.Drawing.Size(38, 23);
			this.btnPlay.TabIndex = 1;
			this.btnPlay.Text = "播放";
			this.btnPlay.UseVisualStyleBackColor = true;
			this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
			// 
			// txtUrl
			// 
			this.txtUrl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtUrl.Location = new System.Drawing.Point(9, 3);
			this.txtUrl.Name = "txtUrl";
			this.txtUrl.Size = new System.Drawing.Size(236, 21);
			this.txtUrl.TabIndex = 0;
			this.txtUrl.Text = "rtmp://www.car-eye.cn:10085/live/123";
			// 
			// lblView
			// 
			this.lblView.AllowDrop = true;
			this.lblView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblView.BackColor = System.Drawing.Color.Black;
			this.lblView.Font = new System.Drawing.Font("宋体", 10.5F);
			this.lblView.ForeColor = System.Drawing.Color.WhiteSmoke;
			this.lblView.Location = new System.Drawing.Point(0, 25);
			this.lblView.Name = "lblView";
			this.lblView.Size = new System.Drawing.Size(428, 273);
			this.lblView.TabIndex = 4;
			this.lblView.Text = "无 图 像 ...";
			this.lblView.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lblView.ConnectStatusChanged += new System.EventHandler<CarEyePlayerDemo.Player.PlayerStatusEventArgs>(this.lblView_ConnectStatusChanged);
			this.lblView.SizeChanged += new System.EventHandler(this.lblView_SizeChanged);
			this.lblView.Click += new System.EventHandler(this.lblView_Click);
			this.lblView.DragDrop += new System.Windows.Forms.DragEventHandler(this.lblView_DragDrop);
			this.lblView.DragEnter += new System.Windows.Forms.DragEventHandler(this.lblView_DragEnter);
			this.lblView.DoubleClick += new System.EventHandler(this.lblView_DoubleClick);
			// 
			// bkWorker
			// 
			this.bkWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bkWorker_DoWork);
			// 
			// tmrTip
			// 
			this.tmrTip.Tick += new System.EventHandler(this.tmrTip_Tick);
			// 
			// tmrPlay
			// 
			this.tmrPlay.Interval = 200;
			this.tmrPlay.Tick += new System.EventHandler(this.tmrPlay_Tick);
			// 
			// CarEyeViewer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Firebrick;
			this.Controls.Add(this.pnlBase);
			this.Name = "CarEyeViewer";
			this.Size = new System.Drawing.Size(456, 344);
			this.pnlBase.ResumeLayout(false);
			this.pnlBase.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.trackVolume)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pgrPlay)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel pnlBase;
		private CarEyeLabel lblView;
		private System.Windows.Forms.Button btnPlay;
		private System.Windows.Forms.TextBox txtUrl;
		private System.ComponentModel.BackgroundWorker bkWorker;
		private System.Windows.Forms.Button btnRecord;
		private System.Windows.Forms.Button btnScreenshot;
		private System.Windows.Forms.Timer tmrTip;
		private System.Windows.Forms.TrackBar pgrPlay;
		private System.Windows.Forms.Button btnFast;
		private System.Windows.Forms.Button btnSlow;
		private System.Windows.Forms.Label lblCurTime;
		private System.Windows.Forms.Label lblTotalTime;
		private System.Windows.Forms.Timer tmrPlay;
		private System.Windows.Forms.TrackBar trackVolume;
	}
}
