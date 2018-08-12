using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Forms;
using System.Linq;

namespace CarEyePlayerDemo.Player
{
	public partial class CarEyeViewer : UserControl
	{
		/// <summary>
		/// 本控件所在窗口
		/// </summary>
		private Form mParent;
		/// <summary>
		/// 播放URL链接
		/// </summary>
		public string Url
		{
			get
			{
				return this.txtUrl.Text;
			}
			set
			{
				this.txtUrl.Text = value;
			}
		}
		/// <summary>
		/// 播放器句柄
		/// </summary>
		private IntPtr mPlayer = IntPtr.Zero;
		/// <summary>
		/// 是否正在录像
		/// </summary>
		private bool mIsRecording = false;
		/// <summary>
		/// 消息提示计时
		/// </summary>
		private int mTipCount = 0;
		/// <summary>
		/// C#与C++的字符串交互问题，要定义个不会被GC回收的字符串
		/// </summary>
		private string mTipString = string.Empty;
		/// <summary>
		/// 播放速度，20~180, 100为正常速度
		/// </summary>
		private int mPlaySpeed = 100;
		/// <summary>
		/// 本播放器支持的文件后缀
		/// </summary>
		private readonly string[] SUPPORT_SUFFIX = { "mp4", "mkv", "avi" };
		/// <summary>
		/// 播放总长度，如果能获取到的话
		/// </summary>
		private Int64 mTotalTime = 0;
		/// <summary>
		/// 当前播放时间
		/// </summary>
		private Int64 mCurrentTime = 0;

		public CarEyeViewer()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 视频监控窗体控件
		/// </summary>
		public CarEyeViewer(Form aParent)
			: this()
		{
			UpdateParent(aParent);
		}

		/// <summary>
		/// 更新本窗口所在的父窗口
		/// </summary>
		/// <param name="aParent"></param>
		public void UpdateParent(Form aParent)
		{
			mParent = aParent;
		}

        /// <summary>
        /// 开始播放流媒体
        /// </summary>
        /// <returns></returns>
        [HandleProcessCorruptedStateExceptions]
		[SecurityCritical]
		private bool StartPlay(string aUrl)
		{
			if (string.IsNullOrEmpty(aUrl))
			{
				return false;
			}

			StopPlay();
			Url = aUrl;

			this.btnPlay.Enabled = false;
			mTotalTime = 0;
			mCurrentTime = 0;
			this.mPlaySpeed = 100;
			try
			{
				Debug.WriteLine("Start play...");
				// 				mPlayer = (IntPtr)Invoke(new Func<IntPtr>(() => PlayerMethods.CEPlayer_Open(Url, this.lblView.Handle, CE_VIDEO_RENDER_TYPE.VIDEO_RENDER_TYPE_GDI,
				// 								CE_VIDEO_SCALE_MODE.VIDEO_MODE_STRETCHED, 100, 50)));
				this.trackVolume.Value = 7;
                mPlayer = PlayerMethods.CEPlayer_Open(this.Url, this.lblView.Handle, CE_VIDEO_RENDER_TYPE.VIDEO_RENDER_TYPE_GDI,
                                                                 CE_VIDEO_SCALE_MODE.VIDEO_MODE_STRETCHED, 100, 95);
                // 								mPlayer = PlayerMethods.player_open(this.Url, this.lblView.Handle, IntPtr.Zero);
                Debug.WriteLine("Start playing...");
			}
			catch (AccessViolationException ex)
			{
				Debug.WriteLine($"Stop ESC ex: {ex.Message}");
			}
			catch (Exception ex)
			{
				mPlayer = IntPtr.Zero;
				MessageBox.Show($"播放流媒体失败：{ex.Message}");
				this.btnPlay.Enabled = true;
				return false;
			}

			if (mPlayer == IntPtr.Zero)
			{
				MessageBox.Show("播放流媒体失败...");
				this.btnPlay.Enabled = true;
				return false;
			}

			PlayerMethods.CEPlayer_Play(mPlayer);
			this.btnPlay.Enabled = true;
			return true;
		}

		/// <summary>
		/// 停止播放流媒体
		/// </summary>
		[HandleProcessCorruptedStateExceptions]
		[SecurityCritical]
		private void StopPlay()
		{
			this.btnPlay.Enabled = false;
			this.btnRecord.Enabled = this.btnScreenshot.Enabled = false;
			this.btnFast.Enabled = this.btnSlow.Enabled = false;
			this.pgrPlay.Enabled = false;
			this.trackVolume.Enabled = false;
			this.tmrPlay.Stop();
			this.lblCurTime.Text = "00:00";
			if (mPlayer != IntPtr.Zero)
			{
				Debug.WriteLine("Stop record...");
				StopRecord();
				while (this.bkWorker.IsBusy)
				{
					System.Threading.Thread.Sleep(10);
				}
				this.bkWorker.RunWorkerAsync();
			}
		}

		private void btnPlay_Click(object sender, EventArgs e)
		{
			if (this.mPlayer != IntPtr.Zero)
			{
				this.StopPlay();
				return;
			}

			string tmpUrl = this.txtUrl.Text.Trim();

			if (string.IsNullOrEmpty(tmpUrl))
			{
				MessageBox.Show("请输入播放地址。。。");
				return;
			}

			if (this.StartPlay(tmpUrl))
			{
				this.btnPlay.Text = "停止";
//				this.btnRecord.Enabled = this.btnScreenshot.Enabled = true;
			}
			this.btnPlay.Enabled = true;
		}

		private void lblView_Click(object sender, EventArgs e)
		{
			this.OnClick(e);
		}

		private void lblView_DoubleClick(object sender, EventArgs e)
		{
			this.OnDoubleClick(e);
		}

		/// <summary>
		/// 切换播放区域大小
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lblView_SizeChanged(object sender, EventArgs e)
		{
			if (mPlayer != IntPtr.Zero)
			{
				try
				{
					PlayerMethods.CEPlayer_Resize(mPlayer, 0, 0, 0, this.lblView.Width, this.lblView.Height);
				}
				catch
				{
				}
			}
		}

		private void bkWorker_DoWork(object sender, DoWorkEventArgs e)
		{
			if (mPlayer == IntPtr.Zero)
			{
				return;
			}

			// 防止线程抢占
			IntPtr tmpPlayer = mPlayer;
			mPlayer = IntPtr.Zero;
			try
			{
				Debug.WriteLine("Stop playing...");
				PlayerMethods.CEPlayer_Close(tmpPlayer);
				Debug.WriteLine("Stop played...");
			}
			catch (AccessViolationException ex)
			{
				Debug.WriteLine($"Stop ESC ex: {ex.Message}");
			}
			catch (Exception ex1)
			{
				Debug.WriteLine($"Stop ex: {ex1.Message}");
			}

			this.Invoke(new Action(() =>
			 				{
			 					this.btnPlay.Text = "播放";
			 					this.btnPlay.Enabled = true;
								tmrTip.Stop();
								mTipCount = 0;
								this.lblView.Refresh();
			 				}));
		}

		/// <summary>
		/// 截图
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnScreenshot_Click(object sender, EventArgs e)
		{
			if (mPlayer == IntPtr.Zero)
			{
				MessageBox.Show("未播放，无法截图。。。");
			}
			else
			{
				string snapPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Snapshot");
				if (!Directory.Exists(snapPath))
				{
					Directory.CreateDirectory(snapPath);
				}
				string fileName = Path.Combine(snapPath, DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg");
				try
				{
					if (PlayerMethods.CEPlayer_Snapshot(mPlayer, fileName, 0, 0, 0) == 0)
					{
						//						MessageBox.Show($"截图成功：[{fileName}]");
						ShowTipString("截图成功");
					}
					else
					{
						MessageBox.Show("截图失败。。。");
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show($"截图失败：{ex.Message}");
				}
			}
		}

		/// <summary>
		/// 停止播放
		/// </summary>
		private void StopRecord()
		{
			if (!mIsRecording)
			{
				return;
			}

			this.btnRecord.Enabled = false;
			this.btnRecord.Text = "录像";
			if (mPlayer != IntPtr.Zero)
			{
				try
				{
					PlayerMethods.CEPlayer_Stoprecord(mPlayer);
					ShowTipString("停止录像");
				}
				catch
				{

				}
			}
			mIsRecording = false;
			this.btnRecord.Enabled = true;
		}

		/// <summary>
		/// 录像
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnRecord_Click(object sender, EventArgs e)
		{
			if (this.btnRecord.Text == "停止")
			{
				StopRecord();
				return;
			}

			if (mPlayer == IntPtr.Zero)
			{
				MessageBox.Show("未播放，无法录像。。。");
			}
			else
			{
				this.btnRecord.Enabled = false;
				string recordPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Record");
				if (!Directory.Exists(recordPath))
				{
					Directory.CreateDirectory(recordPath);
				}
				string fileName = Path.Combine(recordPath, DateTime.Now.ToString("yyyyMMddHHmmss") + ".mp4");
				try
				{
					if (PlayerMethods.CEPlayer_Record(mPlayer, fileName) == 0)
					{
						mIsRecording = true;
						this.btnRecord.Text = "停止";
						ShowTipString("开始录像...");
					}
					else
					{
						MessageBox.Show("录像失败。。。");
					}
				}
				catch (Exception ex)
				{
					MessageBox.Show($"录像出现异常：{ex.Message}");
				}
				this.btnRecord.Enabled = true;
			}
		}
		
		/// <summary>
		/// 显示OSD提示信息
		/// </summary>
		/// <param name="aMsg"></param>
		private void ShowTipString(string aMsg)
		{
			if (mPlayer == IntPtr.Zero)
			{
				return;
			}

			tmrTip.Stop();
			mTipString = string.Copy(aMsg);
			try
			{
				// 再次进行判断，防止已经被关掉
				if (mPlayer == IntPtr.Zero)
				{
					return;
				}
				PlayerMethods.CEPlayer_SetOSD(mPlayer, 20, 20, Color.FromArgb(0, 255, 0).ToArgb(), mTipString);

				mTipCount = 0;
				tmrTip.Start();
			}
			catch
			{

			}
		}

		/// <summary>
		/// 连接状态变更
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lblView_ConnectStatusChanged(object sender, PlayerStatusEventArgs e)
		{
			switch (e.Status)
			{
				case PlayerMethods.MSG_OPEN_DONE:
					if (mPlayer == IntPtr.Zero)
					{
						// 有时候结束也会触发该事件
						break;
					}
					PlayerMethods.CEPlayer_SetOSDFont(mPlayer, "微软雅黑", 24);
					ShowTipString($"成功打开链接{DateTime.Now.ToLongTimeString()}...");
					Debug.WriteLine("Open done...");
					mTotalTime = PlayerMethods.GetLongParam(mPlayer, CE_PARAM_ID.PARAM_MEDIA_DURATION);
					Debug.WriteLine($"Total time is {mTotalTime}ms");
					this.BeginInvoke(new Action(() =>
					{
						this.btnRecord.Enabled = true;
						this.btnScreenshot.Enabled = true;
						this.btnFast.Enabled = this.btnSlow.Enabled = true;
						this.trackVolume.Enabled = true;
						this.pgrPlay.Enabled = (mTotalTime > 999);
						int totalSecond = (int)(mTotalTime / 1000);
						mCurrentTime = 0;
						this.pgrPlay.Value = 0;
						this.pgrPlay.Maximum = (int)mTotalTime;
						this.lblCurTime.Text = "00:00";
						this.lblTotalTime.Text = string.Format("{0:D2}:{1:D2}", totalSecond / 60, totalSecond % 60);
						this.tmrPlay.Start();
					}));
					break;

				case PlayerMethods.MSG_OPEN_FAILED:
					Debug.WriteLine("Open fail...");
					break;

				case PlayerMethods.MSG_PLAY_COMPLETED:
					Debug.WriteLine("Play completed...");
					if (mTotalTime > 1000)
					{
						StopPlay();
					}
					break;

				case PlayerMethods.MSG_STREAM_CONNECTED:
					Debug.WriteLine("Connected...");
					break;

				case PlayerMethods.MSG_STREAM_DISCONNECT:
					Debug.WriteLine("Disconnected...");
					this.BeginInvoke(new Action(() => this.lblView.Refresh()));
					break;

				default:
					break;
			}
		}

		/// <summary>
		/// 提示取消定时器
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tmrTip_Tick(object sender, EventArgs e)
		{
			if (mTipCount++ > 20)
			{
				if (mPlayer != IntPtr.Zero)
				{
					try
					{
						mTipString = string.Empty;
						PlayerMethods.CEPlayer_SetOSD(mPlayer, 20, 20, 0, mTipString);
					}
					catch
					{

					}
				}
				mTipCount = 0;
				tmrTip.Stop();
			}
		}

		/// <summary>
		/// 慢放
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSlow_Click(object sender, EventArgs e)
		{
			if (mPlayer == IntPtr.Zero)
			{
				return;
			}
			if (mPlaySpeed >= 100)
			{
				mPlaySpeed = 80;
			}
			else
			{
				mPlaySpeed -= 20;
			}
			if (mPlaySpeed < 20)
			{
				mPlaySpeed = 100;
			}

			PlayerMethods.SetParam(mPlayer, CE_PARAM_ID.PARAM_PLAY_SPEED_VALUE, mPlaySpeed);

			if (mPlaySpeed == 100)
			{
				ShowTipString("正常播放");
			}
			else
			{
				ShowTipString($"X{(100 - mPlaySpeed) / 10}慢速播放");
			}
		}

		/// <summary>
		/// 快放
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnFast_Click(object sender, EventArgs e)
		{
			if (mPlayer == IntPtr.Zero)
			{
				return;
			}
			if (mPlaySpeed <= 100)
			{
				mPlaySpeed = 120;
			}
			else
			{
				mPlaySpeed += 20;
			}
			if (mPlaySpeed > 180)
			{
				mPlaySpeed = 100;
			}

			PlayerMethods.SetParam(mPlayer, CE_PARAM_ID.PARAM_PLAY_SPEED_VALUE, mPlaySpeed);

			if (mPlaySpeed == 100)
			{
				ShowTipString("正常播放");
			}
			else
			{
				ShowTipString($"X{(mPlaySpeed - 100) / 10}快速播放");
			}
		}

		/// <summary>
		/// 判断文件是否支持播放
		/// </summary>
		/// <param name="aFile"></param>
		/// <returns></returns>
		private bool IsSupportFile(string aFile)
		{
			string fileType = Path.GetExtension(aFile).ToLower().TrimStart('.');
			return SUPPORT_SUFFIX.Contains(fileType);
		}

		/// <summary>
		/// 拖动文件检测是否为本播放器支持文件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lblView_DragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				string fileName = Convert.ToString(((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0));
				if (IsSupportFile(fileName))
				{
					e.Effect = DragDropEffects.Link;
					return;
				}
			}

			e.Effect = DragDropEffects.None;
		}

		/// <summary>
		/// 拖动完进行播放
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void lblView_DragDrop(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				string fileName = Convert.ToString(((Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0));
				if (IsSupportFile(fileName))
				{
					this.txtUrl.Text = fileName;
					btnPlay_Click(null, null);
				}
			}
		}

		/// <summary>
		/// 进度条更新控制
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tmrPlay_Tick(object sender, EventArgs e)
		{
			if (mPlayer == IntPtr.Zero || mTotalTime < 1000)
			{
				return;
			}
			// 当前秒
			mCurrentTime = PlayerMethods.GetLongParam(mPlayer, CE_PARAM_ID.PARAM_MEDIA_POSITION);
			int curSecond = (int)(mCurrentTime / 1000);
			this.lblCurTime.Text = string.Format("{0:D2}:{1:D2}", curSecond / 60, curSecond % 60);
			this.pgrPlay.Value = (int)(mCurrentTime > this.pgrPlay.Maximum ? this.pgrPlay.Maximum : mCurrentTime);
		}

		/// <summary>
		/// Trackbar控件鼠标按下时暂停播放以进行进度切换
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pgrPlay_MouseDown(object sender, MouseEventArgs e)
		{
			Debug.WriteLine("Mouse down");
			if (mPlayer == IntPtr.Zero || mTotalTime < 1000)
			{
				return;
			}
			PlayerMethods.CEPlayer_Pause(mPlayer);
			tmrPlay.Stop();
			this.pgrPlay.Value = this.pgrPlay.Maximum * e.Location.X / this.pgrPlay.Width;
		}

		/// <summary>
		/// 鼠标抬起后进行进度切换，并等待切换完成
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pgrPlay_MouseUp(object sender, MouseEventArgs e)
		{
			Debug.WriteLine("Mouse up");
			if (mPlayer == IntPtr.Zero || mTotalTime < 1000)
			{
				return;
			}
			try
			{
				PlayerMethods.CEPlayer_Play(mPlayer);
				PlayerMethods.CEPlayer_Seek(mPlayer, this.pgrPlay.Value);
				mCurrentTime = PlayerMethods.GetLongParam(mPlayer, CE_PARAM_ID.PARAM_MEDIA_POSITION);
			}
			catch
			{

			}
			this.BeginInvoke(new Action(() =>
			{
				for (int i = 0; i < 200; i++)
				{
					System.Threading.Thread.Sleep(5);
					Int64 curMs = PlayerMethods.GetLongParam(mPlayer, CE_PARAM_ID.PARAM_MEDIA_POSITION);
					if (Math.Abs(curMs - mCurrentTime) > 500)
					{
						break;
					}
				}
				tmrPlay.Start();
			}));
		}

		/// <summary>
		/// 修改音量
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void trackVolume_ValueChanged(object sender, EventArgs e)
		{
			if (mPlayer == IntPtr.Zero)
			{
				return;
			}

			try
			{
				int volume = -255 + this.trackVolume.Value * 50;
				PlayerMethods.SetParam(mPlayer, CE_PARAM_ID.PARAM_AUDIO_VOLUME, volume);
			}
			catch
			{

			}
		}
	}
}
