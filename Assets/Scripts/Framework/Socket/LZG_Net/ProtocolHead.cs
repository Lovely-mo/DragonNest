using System;
using System.IO;
using UnityEngine;
using XUtliPoolLib;

namespace XMainClient
{
    public class ProtocolHead
    {
        public static ProtocolHead SharedHead = new ProtocolHead();

        public static byte[] sharedUIntBuffer = new byte[4];

        public uint len;

        public uint type;

        public uint flag;

        public uint tagID;

        public const uint MinSize = 12u;

        public bool IsPtc
        {
            get
            {
                return !TestBit(flag, 0);
            }
        }

        public bool IsRpc
        {
            get
            {
                return TestBit(flag, 0);
            }
        }

        public bool IsRpcReply
        {
            get
            {
                return !TestBit(flag, 1);
            }
        }

        public bool IsRpcRequest
        {
            get
            {
                return TestBit(flag, 1);
            }
        }

        public bool IsCompressed
        {
            get
            {
                return TestBit(flag, 2);
            }
        }

        public bool IsRpcNull
        {
            get
            {
                return TestBit(flag, 3);
            }
        }

        public int Size
        {
            get
            {
                if (IsRpc)
                {
                    return 16;
                }
                return 12;
            }
        }

        public bool TestBit(uint value, int bit)
        {
            return (value & (1 << bit)) != 0;
        }

        public ProtocolHead()
        {
            len = 0u;
            type = 0u;
            flag = 0u;
        }

        public void Reset()
        {
            len = 0u;
            type = 0u;
            flag = 0u;
            tagID = 0u;
        }

        public void Deserialize(byte[] bytes)
        {
            len = BitConverter.ToUInt32(bytes, 0);
            type = BitConverter.ToUInt32(bytes, 4);
            flag = BitConverter.ToUInt32(bytes, 8);
            if (IsRpc)
            {
                tagID = BitConverter.ToUInt32(bytes, 12);
            }
        }

        private uint ToUInt32(ref SmallBuffer<byte> sb, int startIndex)
        {
            uint num = sb[startIndex];
            uint num2 = sb[startIndex + 1];
            uint num3 = sb[startIndex + 2];
            uint num4 = sb[startIndex + 3];
            return num | (num2 << 8) | (num3 << 16) | (num4 << 24);
        }

        public void Deserialize(ref SmallBuffer<byte> sb)
        {
            len = ToUInt32(ref sb, 0);
            type = ToUInt32(ref sb, 4);
            flag = ToUInt32(ref sb, 8);

            if (IsRpc)
            {
                tagID = ToUInt32(ref sb, 12);
            }
           // Debug.LogError(string.Format(" msg Len ={0}   type = {1}  flag = {2}  tagID= {3}  ", len, type, flag, !IsPtc ? tagID : 0));
        }

        private byte[] GetBytes(uint value)
        {
            sharedUIntBuffer[0] = (byte)(value & 0xFF);
            sharedUIntBuffer[1] = (byte)((value & 0xFF00) >> 8);
            sharedUIntBuffer[2] = (byte)((value & 0xFF0000) >> 16);
            sharedUIntBuffer[3] = (byte)((uint)((int)value & -16777216) >> 24);
            return sharedUIntBuffer;
        }

        public void Serialize(MemoryStream stream)
        {
            stream.Write(GetBytes(len), 0, 4);
            stream.Write(GetBytes(type), 0, 4);
            stream.Write(GetBytes(flag), 0, 4);
            if (IsRpc)
            {
                stream.Write(GetBytes(tagID), 0, 4);
            }
        }
    }
}
