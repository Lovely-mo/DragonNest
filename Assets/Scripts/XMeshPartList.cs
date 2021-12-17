using System;
using System.Collections.Generic;
using UnityEngine;

namespace XUtliPoolLib
{
	public class XMeshPartList
	{
	 
		public static byte ConvertType(byte type)
		{
			byte result;
			if (type != 1)
			{
				if (type != 3)
				{
					if (type != 7)
					{
						result = 3;
					}
					else
					{
						result = 7;
					}
				}
				else
				{
					result = 3;
				}
			}
			else
			{
				result = 3;
			}
			return result;
		}

	 
		public void Load()
		{
			Debug.Log("开始");
			if (this.meshPartsInfo != null)
			{
				this.meshPartsInfo.Clear();
			}
			if (this.replaceMeshPartsInfo != null)
			{
				this.replaceMeshPartsInfo.Clear();
			}
			XBinaryReader xbinaryReader = XSingleton<XResourceLoaderMgr>.singleton.ReadBinary("Equipments/equipmentInfo", ".bytes", true, true);
			if (xbinaryReader != null)
			{
				try
				{
					int num = xbinaryReader.ReadInt32();
					Debug.Log(num);
					this.proPrefix = new string[num];
					for (int i = 0; i < num; i++)
					{
						this.proPrefix[i] = xbinaryReader.ReadString(-1);
					}
					int num2 = xbinaryReader.ReadInt32();
					for (int j = 0; j < num2; j++)
					{
						bool flag4 = j < this.partSuffix.Length;
						if (flag4)
						{
							this.partSuffix[j] = xbinaryReader.ReadString(-1);
						}
					}
					this.sharedPrefix = xbinaryReader.ReadString(-1);
					int num3 = xbinaryReader.ReadInt32();
					string[] array = new string[num3];
					for (int k = 0; k < num3; k++)
					{
						array[k] = xbinaryReader.ReadString(-1);
					}
					int num4 = xbinaryReader.ReadInt32();
					bool flag5 = this.meshPartsInfo == null;
					if (flag5)
					{
						this.meshPartsInfo = new Dictionary<uint, byte>(num4);
					}
					for (int l = 0; l < num4; l++)
					{
						uint key = xbinaryReader.ReadUInt32();
						byte value = xbinaryReader.ReadByte();
						this.meshPartsInfo[key] = value;
					}
					num4 = xbinaryReader.ReadInt32();
					bool flag6 = this.replaceMeshPartsInfo == null;
					if (flag6)
					{
						this.replaceMeshPartsInfo = new Dictionary<uint, string>(num4);
					}
					for (int m = 0; m < num4; m++)
					{
						uint key2 = xbinaryReader.ReadUInt32();
						ushort num5 = xbinaryReader.ReadUInt16();
						string value2 = array[(int)num5];
						this.replaceMeshPartsInfo[key2] = value2;
					}
					num4 = xbinaryReader.ReadInt32();
					bool flag7 = this.replaceTexPartsInfo == null;
					if (flag7)
					{
						this.replaceTexPartsInfo = new Dictionary<uint, string>(num4);
					}
					for (int n = 0; n < num4; n++)
					{
						uint key3 = xbinaryReader.ReadUInt32();
						ushort num6 = xbinaryReader.ReadUInt16();
						string value3 = array[(int)num6];
						this.replaceTexPartsInfo[key3] = value3;
					}
				}
				catch (Exception ex)
				{
					Debug.Log("错误");
				}
				finally
				{
					if (xbinaryReader != null)
					{
						xbinaryReader.Close(true);
					}
				}
			}

            foreach (var item in this.proPrefix)
            {
				Debug.Log(item);
            }
			Debug.Log("===========");
			foreach (var item in this.partSuffix)
            {
				Debug.Log(item);
			}
			//XSingleton<XResourceLoaderMgr>.singleton.ClearBinary(xbinaryReader, true);
		}

		public bool GetMeshInfo(string location, int professionType, int part, string srcDir, out byte partType, ref string meshLocation, ref string texLocation)
		{
			partType = 0;
			uint key = XSingleton<XCommon>.singleton.XHash(location);
			bool flag = this.meshPartsInfo.TryGetValue(key, out partType);
			bool result;
			if (flag)
			{
				bool flag2 = partType > XMeshPartList.CutoutPart;
				if (flag2)
				{
					bool flag3 = (partType & XMeshPartList.ReplacePart) > 0;
					if (flag3)
					{
						partType &= (byte)~XMeshPartList.ReplacePart;
						bool flag4 = this.replaceMeshPartsInfo.TryGetValue(key, out meshLocation);
						if (flag4)
						{
							string text = this.proPrefix[professionType];
							string arg = this.partSuffix[part];
							meshLocation = string.Format("Equipments/{0}/{1}{2}", meshLocation, text, arg);
							key = XSingleton<XCommon>.singleton.XHash(meshLocation);
						}
					}
					bool flag5 = (partType & XMeshPartList.ReplaceTex) > 0;
					if (flag5)
					{
						partType &= (byte)~XMeshPartList.ReplaceTex;
						bool flag6 = this.replaceTexPartsInfo.TryGetValue(key, out texLocation);
						if (flag6)
						{
							bool flag7 = texLocation.StartsWith("/");
							if (flag7)
							{
								bool flag8 = string.IsNullOrEmpty(srcDir);
								if (flag8)
								{
									texLocation = null;
								}
								else
								{
									bool flag9 = srcDir.StartsWith("/");
									if (flag9)
									{
										texLocation = string.Format("Equipments{0}{1}", srcDir, texLocation);
									}
									else
									{
										texLocation = string.Format("Equipments/{0}{1}", srcDir, texLocation);
									}
								}
							}
							else
							{
								texLocation = string.Format("Equipments/Player/{0}", texLocation);
							}
						}
					}
					else
					{
						bool flag10 = (partType & XMeshPartList.ShareTex) > 0;
						if (flag10)
						{
							partType &= (byte)~XMeshPartList.ShareTex;
							bool flag11 = this.proPrefix != null && professionType < this.proPrefix.Length;
							if (flag11)
							{
								string text = this.proPrefix[professionType];
								texLocation = location.Replace(text, this.sharedPrefix);
							}
						}
					}
				}
				result = true;
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x040005F9 RID: 1529
		public static byte NormalPart = 1;

		// Token: 0x040005FA RID: 1530
		public static byte OnePart = 3;

		// Token: 0x040005FB RID: 1531
		public static byte CutoutPart = 7;

		// Token: 0x040005FC RID: 1532
		public static byte ReplacePart = 8;

		// Token: 0x040005FD RID: 1533
		public static byte ReplaceTex = 16;

		// Token: 0x040005FE RID: 1534
		public static byte ShareTex = 32;

		// Token: 0x040005FF RID: 1535
		public string[] partSuffix = new string[9];

		// Token: 0x04000600 RID: 1536
		public string[] proPrefix = null;

		// Token: 0x04000601 RID: 1537
		public string sharedPrefix = null;

		// Token: 0x04000602 RID: 1538
		public Dictionary<uint, byte> meshPartsInfo = null;

		// Token: 0x04000603 RID: 1539
		public Dictionary<uint, string> replaceMeshPartsInfo = null;

		// Token: 0x04000604 RID: 1540
		public Dictionary<uint, string> replaceTexPartsInfo = null;
	}
}
