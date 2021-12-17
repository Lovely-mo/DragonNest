using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;
//using XUpdater;
using Object = UnityEngine.Object;


/// <summary>
///从龙之谷项目中反编译出来的代码，拷贝过来，
///用于加载 Equipments/equipmentInfo.bytes
///
/// </summary>
namespace XUtliPoolLib
{
    public sealed class XResourceLoaderMgr : XSingleton<XResourceLoaderMgr>
    {
        public XBinaryReader ReadBinary(string location, string suffix, bool readShareResource, bool error = true)
        {
            TextAsset sharedResource = Resources.Load<TextAsset>(location);

            if (sharedResource == null)
            {
                if (!error)
                {
                    return null;
                }
            }
            XBinaryReader result = null;
            try
            {
                XBinaryReader xbinaryReader = new XBinaryReader();
                xbinaryReader.Init(sharedResource);
                result = xbinaryReader;
            }
            catch (Exception ex)
            {
                XSingleton<XDebug>.singleton.AddErrorLog(ex.Message, location, null, null, null, null);
                result = null;
            }

            return result;
        }


    }
}
