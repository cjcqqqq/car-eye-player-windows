using System;
using System.Runtime.InteropServices;

namespace CarEyePlayerDemo.Player
{
	/// <summary>
	/// 播放器库方法定义
	/// </summary>
	internal static class PlayerMethods
	{
        // 消息ID定义
        // 来自CarEye播放器的消息
        public const int MSG_CAREYE_PLAYER = 0x8001;

        /// <summary>
        /// 连接打开成功
        /// </summary>
        public const int MSG_OPEN_DONE = (('O' << 24) | ('P' << 16) | ('E' << 8) | ('N' << 0));
        /// <summary>
        /// 连接打开失败
        /// </summary>
        public const int MSG_OPEN_FAILED = (('F' << 24) | ('A' << 16) | ('I' << 8) | ('L' << 0));
        /// <summary>
        /// 播放完成
        /// </summary>
        public const int MSG_PLAY_COMPLETED = (('E' << 24) | ('N' << 16) | ('D' << 8) | (' ' << 0));
        /// <summary>
        /// 已经连接上
        /// </summary>
        public const int MSG_STREAM_CONNECTED = (('C' << 24) | ('N' << 16) | ('C' << 8) | ('T' << 0));
        /// <summary>
        /// 断开链接
        /// </summary>
        public const int MSG_STREAM_DISCONNECT = (('D' << 24) | ('C' << 16) | ('N' << 8) | ('T' << 0));
		/// <summary>
		/// 码率计算通知
		/// </summary>
		public const int MSG_VIDEO_BITRATE = (('B' << 24) | ('I' << 16) | ('T' << 8) | ('R' << 0));

		/// <summary>
		/// 激活播放器
		/// </summary>
		/// <param name="license">注册码</param>
		/// <returns></returns>
		[DllImport("libCarEyePlayer.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CEPlayer_Authorize")]
		public static extern int CEPlayer_Authorize(string license);

		/// <summary>
		/// 打开一个媒体流或者媒体文件进行播放，同时返回一个 player 对象指针
		/// </summary>
		/// <param name="url">文件路径（可以是网络流媒体的 URL）</param>
		/// <param name="hWnd">Win32 的窗口句柄/其他平台渲染显示设备句柄</param>
		/// <param name="renderType">视频渲染模式，详见CE_VIDEO_RENDER_TYPE</param>
		/// <param name="videoMode">视频显示模式，详见CE_VIDEO_SCALE_MODE</param>
		/// <param name="speed">播放速度，0-100慢放，100以上快放</param>
		/// <param name="valume">播放音量，-255 - +255</param>
		/// <returns>播放器对象句柄</returns>
		[DllImport("libCarEyePlayer.dll", SetLastError = true, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl, EntryPoint = "CEPlayer_Open")]
		public static extern IntPtr CEPlayer_Open(string url, IntPtr hWnd, CE_VIDEO_RENDER_TYPE renderType, CE_VIDEO_SCALE_MODE videoMode, int speed, int valume);

		/// <summary>
		/// 关闭播放
		/// </summary>
		/// <param name="player">播放器对象句柄</param>
		[DllImport("libCarEyePlayer.dll", SetLastError = true, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl, EntryPoint = "CEPlayer_Close")]
		public static extern void CEPlayer_Close(IntPtr player);

		/// <summary>
		/// 开始播放，注意：媒体流或者文件打开后不需要调用此函数即开始播放，
		/// 此函数在暂停、单步播放的时候调用，返回正常播放逻辑
		/// </summary>
		/// <param name="player">播放器对象句柄</param>
		[DllImport("libCarEyePlayer.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CEPlayer_Play")]
		public static extern void CEPlayer_Play(IntPtr player);

		/// <summary>
		/// 单步播放，一次播放一帧，调用CEPlayer_Play返回正常播放
		/// </summary>
		/// <param name="player">播放器对象句柄</param>
		[DllImport("libCarEyePlayer.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CEPlayer_StepPlay")]
		public static extern void CEPlayer_StepPlay(IntPtr player);

		/// <summary>
		/// 暂停播放，调用CEPlayer_Play返回正常播放
		/// </summary>
		/// <param name="player">播放器对象句柄</param>
		[DllImport("libCarEyePlayer.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CEPlayer_Pause")]
		public static extern void CEPlayer_Pause(IntPtr player);

		/// <summary>
		/// 跳转到指定位置
		/// </summary>
		/// <param name="player">播放器对象句柄</param>
		/// <param name="seek">指定位置，以毫秒为单位</param>
		[DllImport("libCarEyePlayer.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CEPlayer_Seek")]
		public static extern void CEPlayer_Seek(IntPtr player, long seek);

		/// <summary>
		/// 设置显示区域，有两种显示区域，视频显示区和视觉效果显示区
		/// </summary>
		/// <param name="player">播放器对象句柄</param>
		/// <param name="type">指定区域类型  0 - video rect, 1 - audio visual effect rect</param>
		/// <param name="x">显示区域X坐标</param>
		/// <param name="y">显示区域Y坐标</param>
		/// <param name="width">显示区域宽度</param>
		/// <param name="height">显示区域高度</param>
		[DllImport("libCarEyePlayer.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CEPlayer_Resize")]
		public static extern void CEPlayer_Resize(IntPtr player, int type, int x, int y, int width, int height);

		/// <summary>
		/// 视频播放截图
		/// </summary>
		/// <param name="player">播放器对象句柄</param>
		/// <param name="filePath">图片存放路径，以.xxx结束（xxx 目前只支持 jpeg 格式）</param>
		/// <param name="width">指定图片宽高，如果 小于等于 0 则默认使用视频宽高</param>
		/// <param name="height"></param>
		/// <param name="waitTime">是否等待截图完成 0 - 不等待，>0 等待超时 ms 为单位</param>
		/// <returns></returns>
		[DllImport("libCarEyePlayer.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CEPlayer_Snapshot")]
		public static extern int CEPlayer_Snapshot(IntPtr player, string filePath, int width, int height, int waitTime);

		/// <summary>
		/// 视频播放录像
		/// </summary>
		/// <param name="player">播放器对象句柄</param>
		/// <param name="filePath"> 图片存放路径，以.xxx结束（xxx 目前只支持 mp4 格式）</param>
		/// <returns></returns>
		[DllImport("libCarEyePlayer.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CEPlayer_Record")]
		public static extern int CEPlayer_Record(IntPtr player, string filePath);

		/// <summary>
		/// 视频播放停止录像
		/// </summary>
		/// <param name="player">播放器对象句柄</param>
		/// <returns></returns>
		[DllImport("libCarEyePlayer.dll", SetLastError = true, ThrowOnUnmappableChar = true, CallingConvention = CallingConvention.Cdecl, EntryPoint = "CEPlayer_Stoprecord")]
		public static extern int CEPlayer_Stoprecord(IntPtr player);

		/// <summary>
		/// 设置叠加字幕
		/// </summary>
		/// <param name="hplayer">播放器对象句柄</param>
		/// <param name="x">字幕显示左上角位置x坐标</param>
		/// <param name="y">字幕显示左上角位置y坐标</param>
		/// <param name="color">颜色</param>
		/// <param name="tittleContent">OSD显示内容</param>
		/// <returns></returns>
		[DllImport("libCarEyePlayer.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl, EntryPoint = "CEPlayer_SetOSD")]
		public static extern int CEPlayer_SetOSD(IntPtr hplayer, int x, int y, int color, string tittleContent);

		/// <summary>
		/// 设置水印字体属性
		/// </summary>
		/// <param name="hplayer">播放器对象句柄</param>
		/// <param name="fontName">字体名</param>
		/// <param name="fontSize">字体大小</param>
		/// <returns></returns>
		[DllImport("libCarEyePlayer.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl, EntryPoint = "CEPlayer_SetOSDFont")]
		public static extern int CEPlayer_SetOSDFont(IntPtr hplayer, string fontName, int fontSize);

		/// <summary>
		/// 设置参数
		/// </summary>
		/// <param name="player">播放器对象句柄</param>
		/// <param name="param_id">参数ID，见CE_PARAM_ID定义</param>
		/// <param name="param">参数指针</param>
		[DllImport("libCarEyePlayer.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CEPlayer_SetParam")]
		public static extern void CEPlayer_SetParam(IntPtr player, CE_PARAM_ID param_id, IntPtr param);

		/// <summary>
		/// 获取参数
		/// </summary>
		/// <param name="player">播放器对象句柄</param>
		/// <param name="param_id">参数ID，见CE_PARAM_ID定义</param>
		/// <param name="param">参数指针</param>
		[DllImport("libCarEyePlayer.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "CEPlayer_GetParam")]
		public static extern void CEPlayer_GetParam(IntPtr player, CE_PARAM_ID param_id, IntPtr param);

		/// <summary>
		/// 设置32位的参数
		/// </summary>
		/// <param name="player"></param>
		/// <param name="param_id"></param>
		/// <param name="aValue"></param>
		public static void SetParam(IntPtr player, CE_PARAM_ID param_id, int aValue)
		{
			IntPtr prmHandle = Marshal.AllocHGlobal(sizeof(int));
			int[] tmpValue = new int[1] { aValue};
			Marshal.Copy(tmpValue, 0, prmHandle, 1);
			CEPlayer_SetParam(player, param_id, prmHandle);
			Marshal.FreeHGlobal(prmHandle);
		}

		/// <summary>
		/// 设置64位整形
		/// </summary>
		/// <param name="player"></param>
		/// <param name="param_id"></param>
		/// <param name="aValue"></param>
		public static void SetParam(IntPtr player, CE_PARAM_ID param_id, Int64 aValue)
		{
			IntPtr prmHandle = Marshal.AllocHGlobal(sizeof(Int64));
			Int64[] tmpValue = new Int64[1] { aValue };
			Marshal.Copy(tmpValue, 0, prmHandle, 1);
			CEPlayer_SetParam(player, param_id, prmHandle);
			Marshal.FreeHGlobal(prmHandle);
		}

		/// <summary>
		/// 获取32位整形参数
		/// </summary>
		/// <param name="player"></param>
		/// <param name="param_id"></param>
		/// <returns></returns>
		public static int GetIntParam(IntPtr player, CE_PARAM_ID param_id)
		{
			IntPtr prmHandle = Marshal.AllocHGlobal(sizeof(int));
			int[] tmpValue = new int[1];
			CEPlayer_GetParam(player, param_id, prmHandle);
			Marshal.Copy(prmHandle, tmpValue, 0, 1);
			Marshal.FreeHGlobal(prmHandle);

			return tmpValue[0];
		}

		/// <summary>
		/// 获取64位整形参数
		/// </summary>
		/// <param name="player"></param>
		/// <param name="param_id"></param>
		/// <returns></returns>
		public static Int64 GetLongParam(IntPtr player, CE_PARAM_ID param_id)
		{
			IntPtr prmHandle = Marshal.AllocHGlobal(sizeof(Int64));
			Int64[] tmpValue = new Int64[1];
			CEPlayer_GetParam(player, param_id, prmHandle);
			Marshal.Copy(prmHandle, tmpValue, 0, 1);
			Marshal.FreeHGlobal(prmHandle);

			return tmpValue[0];
		}
	}
}
