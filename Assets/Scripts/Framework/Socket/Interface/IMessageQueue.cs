using System;
using System.Collections.Generic;

namespace Networks
{
    /// <summary>
    /// 网络包消息结构体
    /// </summary>
    public struct PackData
    {
        /// <summary>
        /// 消息编号
        /// </summary>
        public int MsgID { get; set; }

        /// <summary>
        /// 消息体
        /// </summary>
        public byte[] MsgBody { get; set; }
    }
    public interface IMessageQueue : IDisposable
    {

        void Add(byte[] o);

        void MoveTo(List<byte[]> bytesList);
        
        bool Empty();
    }
}
