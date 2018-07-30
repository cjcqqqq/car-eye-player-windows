using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Forms;

namespace CarEyePlayerDemo.Player
{
	public partial class CarEyeLabel : Label
	{
		/// <summary>
		/// 网络连接状态
		/// </summary>
		[Browsable(false)]
		public int ConnectStatus { get; private set; }
		/// <summary>
		/// 状态变更触发事件
		/// </summary>
		[Category("行为"),
		Description("播放器连接状态发生变更是触发本事件")]
		public event EventHandler<PlayerStatusEventArgs> ConnectStatusChanged;

		protected override void DefWndProc(ref Message m)
		{
			switch (m.Msg)
			{
				case PlayerMethods.MSG_CAREYE_PLAYER:
					ConnectStatus = (int)m.WParam;
					ConnectStatusChanged?.Invoke(this, new PlayerStatusEventArgs(ConnectStatus));
					//Debug.WriteLine($"Label Player msg: 0x{m.WParam.ToString("X8")}...");
					break;

				default:
					base.DefWndProc(ref m);
					break;
			}
		}
	}
}
