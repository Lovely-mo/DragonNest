using System;
using UnityEngine;
using XUtliPoolLib;

namespace XMainClient
{
	// Token: 0x020009CD RID: 2509
	internal class XEquipDocument:Singleton<XEquipDocument>
	{
		public void init()
        {
			_MeshPartList = new XMeshPartList();
			_MeshPartList.Load();
		}
		/// <summary>
		/// professionIndex   0--6
		/// 
		/// </summary>
		/// <param name="partIndex"></param>
		/// <param name="path"></param>
		/// <param name="professionIndex">  0--6 </param>
		/// <param name="dir"></param>
		/// <returns></returns>
		public static string GetDefaultEquipName(int partIndex, string path, int professionIndex, out string dir)
		{
			bool flag = partIndex >= 0 && partIndex < XEquipDocument._MeshPartList.partSuffix.Length && professionIndex >= 0 && professionIndex < XEquipDocument._MeshPartList.proPrefix.Length;
			
			string result;
			if (flag)
			{
				string prefix = XEquipDocument._MeshPartList.proPrefix[professionIndex];
				string partSuffi = XEquipDocument._MeshPartList.partSuffix[partIndex];
				if (string.IsNullOrEmpty(path))
				{
					dir = "Player";
					result = string.Format("Player/{0}{1}", prefix, partSuffi); ///"face"    "hair"   "body"  "leg"  "glove"  "boots" "second"  "helmet"  "weapon"
				}
				else
				{
					if (path.StartsWith("/"))
					{
						dir = path;
						result = string.Format("{0}/{1}{2}", path, prefix, partSuffi);
					}
					else
					{
						if (path == "E")
						{
							dir = "";
							result = "";
						}
						else   
						{
							dir = "Player";
							result = string.Format("Player/{0}{1}", prefix, path);
						}
					}
				}
			}
			else
			{
				dir = "";
				result = path;
			}
			return result;
		}



	 
		public static SkinnedMeshRenderer GetSmr(GameObject keyGo)
		{
			Transform transform = keyGo.transform.Find("CombinedMesh");
			bool flag = transform == null;
			if (flag)
			{
				transform = new GameObject("CombinedMesh")
				{
					transform = 
					{
						parent = keyGo.transform
					}
				}.transform;
			}
			SkinnedMeshRenderer skinnedMeshRenderer = transform.GetComponent<SkinnedMeshRenderer>();
			bool flag2 = skinnedMeshRenderer == null;
			if (flag2)
			{
				skinnedMeshRenderer = transform.gameObject.AddComponent<SkinnedMeshRenderer>();
			}
			return skinnedMeshRenderer;
		}

        public override void Dispose()
        {
            throw new NotImplementedException();
        }

      
        public static string present_prefix = "_presentid:";

		// Token: 0x04003425 RID: 13349
		public static CombineMeshUtility _CombineMeshUtility = null;

		// Token: 0x04003426 RID: 13350
		public static XMeshPartList _MeshPartList = null;



		// Token: 0x0400342A RID: 13354
		public static int CurrentVisibleRole = 0;
	}
}
