using System;

namespace CarEyePlayerDemo.Player
{
    public class PlayerStatusEventArgs : EventArgs
    {
        /// <summary>
        /// 当前的播放器状态，参考PlayerMethods中的消息常量定义
        /// </summary>
        public int Status { get; private set; }
		/// <summary>
		/// 携带参数
		/// </summary>
		public Int64 Param { get; private set; }

        public PlayerStatusEventArgs(int aStatus, Int64 aParam)
        {
            this.Status = aStatus;
			this.Param = aParam;
        }
    }
}
