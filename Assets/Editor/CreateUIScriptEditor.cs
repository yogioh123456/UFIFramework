using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class CreateUIScriptEditor : MonoBehaviour
{
    [MenuItem("Assets/创建UI脚本", priority = 0)]
    static void CreateUI(){
        string[] guidArray = Selection.assetGUIDs;
        foreach (var item in guidArray)
        {
            //打印路径
            string selecetFloder = AssetDatabase.GUIDToAssetPath(item);
            Debug.Log(selecetFloder);
            //打印名字
            string selectName = Path.GetFileName(selecetFloder);
            Debug.Log(selectName);
        }
    }
}
