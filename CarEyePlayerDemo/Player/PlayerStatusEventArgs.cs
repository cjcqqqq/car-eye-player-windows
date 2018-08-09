using System;

namespace CarEyePlayerDemo.Player
{
    public class PlayerStatusEventArgs : EventArgs
    {
        /// <summary>
        /// 当前的播放器状态，参考PlayerMethods中的消息常量定义
        /// </summary>
        public int Status { get; private set; }

        public PlayerStatusEventArgs(int aStatus)
        {
            this.Status = aStatus;
        }
    }
}
