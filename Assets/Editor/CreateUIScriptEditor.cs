using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Xml;
using System.Text;

public class CreateUIScriptEditor : MonoBehaviour
{
    [MenuItem("Assets/创建UI脚本", priority = 0)]
    static void CreateUI(){
        //----------------------------配置fgui工程目录----------------------------
        string fguiProjectPath = Application.dataPath + @"/../FairyGuiProject/FairyGuiDemo/assets";

        print(fguiProjectPath);
        DirectoryInfo root = new DirectoryInfo(fguiProjectPath);

        xmlList.Clear();
        GetAllXML(root);

        string[] guidArray = Selection.assetGUIDs;
        foreach (var item in guidArray)
        {
            //打印路径
            string selecetFloder = AssetDatabase.GUIDToAssetPath(item);
            Debug.Log(selecetFloder);
            //打印名字
            string selectName = Path.GetFileName(selecetFloder);
            Debug.Log(selectName);
            //获取名字相同的xml文件
            FileInfo xmlFile = GetXmlByName(selectName+".xml");
            if (xmlFile != null)
            {
                XmlDocument xmlDoc = new XmlDocument();
                print(xmlFile.ToString());
                xmlDoc.Load(xmlFile.ToString());
                XmlNodeList xnlist = xmlDoc.SelectSingleNode("component").ChildNodes;
                foreach (XmlElement xe in xnlist)
                {
                    foreach (XmlElement element in xe)
                    {
                        //-----------------------获取数据-------------------------//
                        var _type = element.Name;
                        var _attr = element.GetAttribute("id");
                        var _name = element.GetAttribute("name");
                        print("类型:"+ _type + " id:" + _attr + " 名称" + _name);

                        //------------------------写文件-------------------------//
                        File.WriteAllText(selecetFloder + "/UI_" + selectName + ".txt", "xxx内容xxx", Encoding.UTF8);
                        //刷新
                        AssetDatabase.Refresh();
                    }
                }
            }
            else{
                Debug.LogError("没有这个ui文件" + selectName);
            }
        }
    }

    static List<FileInfo> xmlList = new List<FileInfo>();
    //获取所有xml文件
    static void GetAllXML(DirectoryInfo root){

        foreach (DirectoryInfo wjj in root.GetDirectories())
        {
            if (wjj.GetDirectories().Length > 0 || wjj.GetFiles().Length > 0)
            {
                GetAllXML(wjj);
            }
        }
        foreach (FileInfo file in root.GetFiles())
        {
            if (file.Name.EndsWith(".xml"))
            {
                xmlList.Add(file);
                print(file.Name);
            }
        }
    }
    //获取xml文件
    static FileInfo GetXmlByName(string xmlName){
        foreach (FileInfo item in xmlList)
        {
            if (item.Name.Equals(xmlName))
            {
                return item;
            }
        }
        return null;
    }
    //自动获取组件
    /*UI命名规则
     * Button_按钮
     * Image_图片
     * Text_文本
     * Loader_装载器
     * List_列表
     * 
     * 
     * 
     */
    static string AutoGetComp(){
        string content = "";

        return content;
    }
}
