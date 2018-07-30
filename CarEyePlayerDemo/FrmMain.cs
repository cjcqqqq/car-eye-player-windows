using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using CarEyePlayerDemo.Player;

namespace CarEyePlayerDemo
{
	public partial class FrmMain : Form
	{
		/// <summary>
		/// 当前分屏数
		/// </summary>
		private byte mSplitCount = 4;
		/// <summary>
		/// 被选中的视频窗口
		/// </summary>
		private CarEyeViewer mSelectedViewer;
		/// <summary>
		/// 直播窗口集合
		/// </summary>
		private List<CarEyeViewer> mViewers = new List<CarEyeViewer>(4);
		/// <summary>
		/// 注册密钥
		/// </summary>
		private const string KEY = "6A342B4E6B4969576B5A734144316C62704B6277772B744459584A4665575651624746355A584A455A5731764C6D56345A536C58444661672F385067523246326157346D516D466962334E68514449774D545A4659584E355247467964326C75564756686257566863336B3D";


		public FrmMain()
		{
			InitializeComponent();
			mViewers.Add(this.viewer1);
			mViewers.Add(this.carEyeViewer1);
			mViewers.Add(this.carEyeViewer2);
			mViewers.Add(this.carEyeViewer3);
		}

		/// <summary>
		/// 窗体载入过程
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FrmMain_Load(object sender, EventArgs e)
		{
			CE_ACTIVE_RESULT result = (CE_ACTIVE_RESULT)PlayerMethods.CEPlayer_Authorize(KEY);
			if (result != CE_ACTIVE_RESULT.SUCCESS)
			{
				HideAllViewer();
				if (result == CE_ACTIVE_RESULT.INVALID_KEY)
				{
					MessageBox.Show("无效的激活密钥。。。");
				}
				else if (result == CE_ACTIVE_RESULT.INVALID_TIME)
				{
					MessageBox.Show("该密钥有效时间已到期。。。");
				}
				else if (result == CE_ACTIVE_RESULT.INVALID_PROGRAM)
				{
					MessageBox.Show("非本密钥对应的应用进程。。。");
				}
				else
				{
					MessageBox.Show($"控件激活失败，代码：{(int)result}");
				}
				return;
			}
			SetLayout();
		}

		/// <summary>
		/// 根据窗口尺寸进行分屏切换
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FrmMain_SizeChanged(object sender, EventArgs e)
		{
			SetLayout();
		}

		/// <summary>
		/// 隐藏所有的视频输出窗口
		/// </summary>
		private void HideAllViewer()
		{
			foreach (var tmpItem in mViewers)
			{
				tmpItem.Visible = false;
			}
		}

		/// <summary>
		/// 设置全屏模式
		/// </summary>
		private void SetFullScreen()
		{
			if (mSelectedViewer == null)
			{
				if (mViewers == null || mViewers.Count < 1)
				{
					return;
				}
				mSelectedViewer = mViewers[0];
			}

			HideAllViewer();
			mSelectedViewer.Location = new Point(0, 0);
			mSelectedViewer.Size = this.ClientSize;
			mSelectedViewer.Visible = true;
			mSplitCount = 1;
		}

		/// <summary>
		/// 符合4、9、16矩阵的分屏布局
		/// </summary>
		private void SetMatrixScreen(byte aCount)
		{
			HideAllViewer();

			// 每行每列的视频输出窗口个数
			int tmpCount = (int)Math.Sqrt(aCount);
			int perWidth = this.ClientSize.Width / tmpCount;
			int perHeight = this.ClientSize.Height / tmpCount;
			Size perSize = new Size(perWidth, perHeight);

			for (int i = 0; i < tmpCount; i++)
			{
				for (int j = 0; j < tmpCount; j++)
				{
					CarEyeViewer tmpViewer = mViewers[i * tmpCount + j];
					tmpViewer.Location = new Point(j * perWidth, i * perHeight);
					tmpViewer.Size = perSize;
					tmpViewer.Visible = true;
					if (j == tmpCount - 1)
					{
						// 最后一列对齐控件右边
						tmpViewer.Width += this.ClientRectangle.Right - tmpViewer.Right;
					}
					if (i == tmpCount - 1)
					{
						tmpViewer.Height += this.ClientRectangle.Bottom - tmpViewer.Bottom;
					}
				}
			}

			mSplitCount = aCount;
			if (mSelectedViewer == null)
			{
				return;
			}
		}

		/// <summary>
		/// 更新页面布局
		/// </summary>
		private void SetLayout()
		{
			switch (mSplitCount)
			{
				case 1:
					SetFullScreen();
					break;

				case 4:
					SetMatrixScreen(mSplitCount);
					break;

				default:
					break;
			}
		}

		/// <summary>
		/// 设置选中指定Viewer
		/// </summary>
		/// <param name="aViewer"></param>
		private void SelectViewer(CarEyeViewer aViewer)
		{
			if (mSelectedViewer != null)
			{
				mSelectedViewer.BackColor = Color.Firebrick;
			}

			mSelectedViewer = aViewer;
			mSelectedViewer.BackColor = Color.LimeGreen;
		}

		/// <summary>
		/// 选中指定播放窗口
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void viewer1_Click(object sender, EventArgs e)
		{
			SelectViewer(sender as CarEyeViewer);
		}

		/// <summary>
		/// 双击切换全屏4分屏
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void viewer1_DoubleClick(object sender, EventArgs e)
		{
			viewer1_Click(sender, e);

			mSplitCount = (byte)(mSplitCount == 1 ? 4 : 1);
			SetLayout();
		}
	}
}
