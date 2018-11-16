using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Xml;
using System.Text;

public class CreateUIScriptEditor : MonoBehaviour
{
    [MenuItem("Assets/创建UIPanel脚本", priority = 0)]
    static void CreateUI(){
        //----------------------------配置fgui工程目录----------------------------
        string fguiProjectPath = Application.dataPath + @"/../FairyGuiProject/FairyGuiDemo/assets";
        //----------------------------------------------------------------------

        print(fguiProjectPath);
        DirectoryInfo root = new DirectoryInfo(fguiProjectPath);

        xmlList.Clear();
        packageDic.Clear();
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
                string xmlContent = "";
                foreach (XmlElement xe in xnlist)
                {
                    foreach (XmlElement element in xe)
                    {
                        //-----------------------获取数据-------------------------//
                        var _type = element.Name;
                        var _attr = element.GetAttribute("id");
                        var _name = element.GetAttribute("name");
                        print("类型:"+ _type + " id:" + _attr + " 名称" + _name);
                        xmlContent += "类型:" + _type + " id:" + _attr + " 名称" + _name + "\n        ";

                    }
                }
                //------------------------写文件-------------------------//
                File.WriteAllText(selecetFloder + "/UI_" + selectName + ".txt", AutoGetPanelComp(xmlContent,selectName), Encoding.UTF8);
                //刷新
                AssetDatabase.Refresh();
            }
            else{
                Debug.LogError("没有这个ui文件" + selectName);
            }
        }
    }

    static List<FileInfo> xmlList = new List<FileInfo>();
    static Dictionary<string, string> packageDic = new Dictionary<string, string>();
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
                //查找package.xml文件
                if (file.Name.Equals("package.xml"))
                {
                    CreatePackageDic(file);
                }
            }
        }
    }
    //解析package.xml  生成 包---对应组件 的字典
    static void CreatePackageDic(FileInfo xmlFile)
    {
        XmlDocument xmlDoc = new XmlDocument();
        print(xmlFile.ToString());
        xmlDoc.Load(xmlFile.ToString());
        XmlNodeList xnlist = xmlDoc.SelectSingleNode("packageDescription").ChildNodes;

        string packName = "";
        //寻找包名
        foreach (XmlElement xe in xnlist)
        {
            if (xe.Name == "publish")
            {
                print("包名---------" + xe.GetAttribute("name"));
                packName = xe.GetAttribute("name");
                break;
            }
        }
        
        //内容
        foreach (XmlElement xe in xnlist)
        {
            foreach (XmlElement element in xe)
            {
                //-----------------------获取数据-------------------------//
                var _type = element.Name;
                if (_type == "component")
                {
                    var _name = element.GetAttribute("name");
                    if (packageDic.ContainsKey(_name))
                    {
                        //有重复的，要么这个是没用的，要么弄错了，直接覆盖吧
                        packageDic[_name] = packName;
                    }
                    else
                    {
                        packageDic.Add(_name, packName);
                    }
                }
            }
        }
    }
    //获取xml文件
    static FileInfo GetXmlByName(string xmlName){
        foreach (FileInfo item in xmlList)
        {
            print("-----------------" + item.Name);
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
     * 需要类名，包名，组件名
     */
    static string AutoGetPanelComp(string xmlContent, string className){
        string content = "";
        string packName = "";
        if (packageDic.ContainsKey(className + ".xml"))
        {
            packName = packageDic[className + ".xml"];
        }
        else
        {
            print("找不到" + className + ".xml");
            print("目前拥有===================================================");
            //遍历
            foreach (var _key in packageDic.Keys)
            {
                print(_key);
            }
            return "错误";
        }
        

        string head = string.Format(
            @"using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

/// <summary>
/// UI面板 {2}
/// 类型 Panel
/// 注意：本段代码由系统自动生成
/// </summary>
public class {1} : BaseUIPanel
{{
    public GButton btn_login;
    public GTextField text_title;
    public void OnCreatePanel(){{
        base.OnCreate(""{3}"", ""{2}"");
    }}

    protected override void GetFGUIComp(){{
        {0}
    }}
}}", xmlContent, "UI_"+className, className, packName);

        content += head;
        return content;
    }
}
