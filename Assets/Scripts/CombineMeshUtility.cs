using System;
using System.Collections.Generic;
using UnityEngine;
using XUtliPoolLib;

namespace XMainClient
{
	// Token: 0x02000FD6 RID: 4054
	internal class CombineMeshUtility
	{
		// Token: 0x0600D268 RID: 53864 RVA: 0x00311A40 File Offset: 0x0030FC40
		public CombineMeshUtility()
		{
			for (int i = 0; i < CombineMeshUtility.MaxPartCount; i++)
			{
				this.matCombineInstanceArrayCache.Add(new CombineInstance[i + 1]);
			}
		}

		// Token: 0x0600D269 RID: 53865 RVA: 0x00311A8C File Offset: 0x0030FC8C
		private CombineInstance[] GetMatCombineInstanceArray(int partCount)
		{
			int num = partCount - 1;
			bool flag = num >= 0;
			CombineInstance[] result;
			if (flag)
			{
				bool flag2 = num >= this.matCombineInstanceArrayCache.Count;
				if (flag2)
				{
					result = new CombineInstance[partCount];
				}
				else
				{
					result = this.matCombineInstanceArrayCache[num];
				}
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600D26A RID: 53866 RVA: 0x00311AE0 File Offset: 0x0030FCE0
		//public bool Combine(CombineMeshTask combineTask)
		//{
		//	int num = 0;
		//	for (int i = 0; i < 8; i++)
		//	{
		//		PartLoadTask partLoadTask = combineTask.parts[i] as PartLoadTask;
		//		bool flag = partLoadTask.HasMesh();
		//		if (flag)
		//		{
		//			num++;
		//		}
		//	}
		//	combineTask.isOnepart = false;
		//	CombineInstance[] matCombineInstanceArray = this.GetMatCombineInstanceArray(num);
		//	bool flag2 = matCombineInstanceArray != null && combineTask.skin != null;
		//	bool result;
		//	if (flag2)
		//	{
		//		bool flag3 = false;
		//		PartLoadTask partLoadTask2 = null;
		//		int num2 = 0;
		//		for (int j = 0; j < 8; j++)
		//		{
		//			PartLoadTask partLoadTask3 = combineTask.parts[j] as PartLoadTask;
		//			bool flag4 = partLoadTask3.HasMesh();
		//			if (flag4)
		//			{
		//				bool isReadable = partLoadTask3.mesh.isReadable;
		//				if (isReadable)
		//				{
		//					Debug.LogError(string.Format("Combine    j = {0}  hasMesh = {1}   mesh 's name ={2}",j, flag4, partLoadTask3.mesh.name));
		//					CombineInstance combineInstance = default(CombineInstance);
		//					combineInstance.mesh = partLoadTask3.mesh;
		//					bool flag5 = partLoadTask3.partType != XMeshPartList.NormalPart;
		//					if (flag5)
		//					{
		//						combineTask.isOnepart = true;
		//						flag3 = (partLoadTask3.partType == XMeshPartList.CutoutPart);
		//						partLoadTask2 = partLoadTask3;
		//					}
		//					combineInstance.subMeshIndex = 0;
		//					combineInstance.subMeshIndex = 0;
		//					matCombineInstanceArray[num2++] = combineInstance;
		//				}
		//				else
		//				{
		//					XSingleton<XDebug>.singleton.AddErrorLog2("Mesh not readable:{0}", new object[]
		//					{
		//						j
		//					});
		//				}
		//			}
		//		}
		//		bool flag6 = combineTask.skin.sharedMesh == null;
		//		if (flag6)
		//		{
		//			combineTask.skin.sharedMesh = new Mesh();
		//			combineTask.skin.sharedMesh.MarkDynamic();
		//		}
		//		else
		//		{
		//			combineTask.skin.sharedMesh.Clear(true);
		//		}
		//		combineTask.skin.enabled = false;
		//		combineTask.skin.gameObject.SetActive(false);
		//		combineTask.skin.sharedMesh.CombineMeshes(matCombineInstanceArray, true, false);
		//		combineTask.skin.gameObject.SetActive(true);
		//		bool flag7 = combineTask.skin.sharedMaterial != null;
		//		if (flag7)
		//		{
		//			XEquipDocument.ReturnMaterial(combineTask.skin.sharedMaterial);
		//			combineTask.skin.sharedMaterial = null;
		//		}
		//		bool flag8 = combineTask.skin.sharedMaterial == null;
		//		if (flag8)
		//		{
		//			bool flag9 = CombineMeshTask.s_CombineMatType == ECombineMatType.ECombined;
		//			if (flag9)
		//			{
		//				combineTask.skin.sharedMaterial = XEquipDocument.GetRoleMat(combineTask.isOnepart, flag3, 0);
		//			}
		//			else
		//			{
		//				combineTask.skin.sharedMaterial = XEquipDocument.GetRoleMat(combineTask.isOnepart, flag3, combineTask.roleType);
		//			}
		//		}
		//		bool flag10 = combineTask.skin.sharedMaterial != null;
		//		if (flag10)
		//		{
		//			combineTask.skin.GetPropertyBlock(combineTask.mpb);
		//			bool isOnepart = combineTask.isOnepart;
		//			if (isOnepart)
		//			{
		//				PartLoadTask partLoadTask4 = combineTask.parts[XFastEnumIntEqualityComparer<EPartType>.ToInt(EPartType.ECombinePartStart)] as PartLoadTask;
		//				PartLoadTask partLoadTask5 = combineTask.parts[XFastEnumIntEqualityComparer<EPartType>.ToInt(EPartType.EHair)] as PartLoadTask;
		//				PartLoadTask partLoadTask6 = combineTask.parts[XFastEnumIntEqualityComparer<EPartType>.ToInt(EPartType.EHeadgear)] as PartLoadTask;
		//				Texture texture = partLoadTask5.GetTexture();
		//				ShaderManager.SetTexture(combineTask.mpb, partLoadTask4.GetTexture(), ShaderManager._ShaderKeyIDFace);
		//				bool flag11 = texture == null;
		//				if (flag11)
		//				{
		//					ShaderManager.SetTexture(combineTask.mpb, partLoadTask6.GetTexture(), ShaderManager._ShaderKeyIDHair);
		//				}
		//				else
		//				{
		//					ShaderManager.SetTexture(combineTask.mpb, texture, ShaderManager._ShaderKeyIDHair);
		//				}
		//				bool flag12 = partLoadTask2 != null;
		//				if (flag12)
		//				{
		//					ShaderManager.SetTexture(combineTask.mpb, partLoadTask2.GetTexture(), ShaderManager._ShaderKeyIDBody);
		//					bool flag13 = flag3;
		//					if (flag13)
		//					{
		//						ShaderManager.SetTexture(combineTask.mpb, partLoadTask2.GetAlpha(), ShaderManager._ShaderKeyIDAlpha);
		//					}
		//				}
		//			}
		//			else
		//			{
		//				int k = 0;
		//				int num3 = XFastEnumIntEqualityComparer<EPartType>.ToInt(EPartType.ECombinePartEnd);
		//				while (k < num3)
		//				{
		//					PartLoadTask partLoadTask7 = combineTask.parts[k] as PartLoadTask;
		//					bool flag14 = partLoadTask7.HasMesh();
		//					if (flag14)
		//					{
		//						ShaderManager.SetTexture(combineTask.mpb, partLoadTask7.GetTexture(), ShaderManager._ShaderKeyIDSkin[k]);
		//					}
		//					k++;
		//				}
		//			}
		//		}
		//		result = true;
		//	}
		//	else
		//	{
		//		result = false;
		//	}
		//	return result;
		//}

		// Token: 0x04005F97 RID: 24471
		public static int MaxPartCount = 8;

		// Token: 0x04005F98 RID: 24472
		private List<CombineInstance[]> matCombineInstanceArrayCache = new List<CombineInstance[]>();
	}
}
