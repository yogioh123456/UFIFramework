using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml;
using System.Text;
using TMPro;

public class CreateUIScriptEditor : MonoBehaviour
{
    //----------------------------配置fgui工程目录----------------------------
    //fgui版本3.9.7
    static string fguiProjectPath = Application.dataPath + @"/../FairyGuiProject/FairyGuiDemo/assets";
    static string buttonNamed = "Button";
    //注意点：按钮的命名必须带 buttonNamed这个字符串的内容 ，因这个类型在xml里面显示的是component，无法识别出 button ，所以需要在命名是给以规范
    //但是我也并不知道GCompontnt能不能直接当按钮使用
    //命名开头带"n" 的不会生成字段和方法，因为fgui里面命名开头带n就是未命名
    //----------------------------------------------------------------------


    [MenuItem("Assets/创建UI View脚本", priority = 0)]
    static void CreateUIView()
    {
        CreateUIScript("View.cs", AutoGetPanelComp);
    }
    [MenuItem("Assets/创建UI Control脚本", priority = 0)]
    static void CreateUIControl()
    {
        CreateUIScript("Control.cs", AutoGenControlScript);
    }
    
    delegate string AutonGenScript(string selectName);
    static void CreateUIScript(string csSuffix, AutonGenScript ac){
        print(fguiProjectPath);
        DirectoryInfo root = new DirectoryInfo(fguiProjectPath);

        xmlList.Clear();
        packageDic.Clear();
        compList.Clear();
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
                        Dictionary<string,string> mDic = new Dictionary<string, string>();
                        var _type = element.Name;
                        var _attr = element.GetAttribute("id");
                        var _name = element.GetAttribute("name");
                        //筛选未手动命名的，就是命名开头带'n'的
                        if (_name[0].Equals('n'))
                        {
                            continue;
                        }
                        mDic.Add("type",_type);
                        mDic.Add("id",_attr);
                        mDic.Add("name",_name);
                        print("类型:"+ _type + " id:" + _attr + " 名称" + _name);
                        xmlContent += "类型:" + _type + " id:" + _attr + " 名称" + _name + "\n        ";
                        compList.Add(mDic);
                    }
                }
                //------------------------写文件View-------------------------//
                File.WriteAllText(selecetFloder + "/UI_" + selectName + csSuffix, ac(selectName), Encoding.UTF8);
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
    static List<Dictionary<string,string>> compList = new List<Dictionary<string,string>>();
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
    static string AutoGetPanelComp(string className){
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
    //---------------字段---------------
    {4}
    //创建面板
    public void OnCreatePanel(){{
        base.OnCreate(""{3}"", ""{2}"");
    }}

    //获取组件的方法
    protected override void GetFGUIComp(){{
        {0}
    }}
}}", GenMethod(), "UI_"+className+"View", className, packName, GenField());

        content += head;
        return content;
    }
    //生成字段
    static string GenField()
    {
        string content = "";
        foreach (Dictionary<string,string> comp in compList)
        {
            content += "public G" + GetHeadUpString(comp["type"],comp["name"]) + " " + comp["name"] + ";\n    ";
        }
        return content;
    }
    //生成获取组件的方法
    static string GenMethod()
    {
        string content = "";
        foreach (Dictionary<string,string> comp in compList)
        {
            content += comp["name"] + " = " + "mainView.GetChild(" + "\"" + comp["name"] + "\"" + ").as" + GetHeadUpString(comp["type"],comp["name"]) + ";\n        ";
        }
        return content;
    }
    //生成对应的类型
    static string GetHeadUpString(string content, string name)
    {
        //对Button命名特殊处理，因为xml读取的button类型是GCComponent类型
        if (name.Contains(buttonNamed))
        {
            content = "button";
        }
        //text特殊处理
        if (content.Equals("text"))
        {
            content = "TextField";
        }
        char[] s = content.ToCharArray();
        s[0] = char.ToUpper(s[0]);
        return s.ArrayToString();
    }

    //-------------------生成ctrl层脚本------------------------
    static string AutoGenControlScript(string className)
    {
        string content = "";
        string head = string.Format(@"using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//面板: {3}
public class {0} : BaseUICtrl
{{
    //面板
    public {1} myPanel;

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {{
        //------------------按钮添加事件-----------------
        {2}
    }}

    /// 面板打开
    void OpenPanel(string panelName)
    {{
        UIManager.Instance.OpenUIPanel(panelName);
    }}

    /// <summary>
    /// 面板回退
    /// </summary>
    void BackPanel()
    {{
        UIManager.Instance.BackUIPanel();
    }}
}}", "UI_"+className+"Control", "UI_"+className+"View", GenBtnEvent(), className);
        content += head;
        return content;
    }
    //自动生成按钮事件
    static string GenBtnEvent()
    {
        string content = "";
        foreach (Dictionary<string,string> comp in compList)
        {
            //对按钮处理
            if (comp["name"].Contains(buttonNamed))
            {
                content += "myPanel." + comp["name"] + ".onClick.Add(delegate(){\n        \n        });\n         ";
            }
        }
        return content;
    }
    
    //这个没有必要
    #region 创建View 和 Control文件夹
    //有必要吗？文件夹只有一个文件
    [MenuItem("Assets/创建VC文件夹", priority = 0)]
    static void CreateViewAndControl()
    {
        string[] guidArray = Selection.assetGUIDs;
        foreach (var item in guidArray)
        {
            string selecetFloder = AssetDatabase.GUIDToAssetPath(item);
            Directory.CreateDirectory(selecetFloder + "/View");
            Directory.CreateDirectory(selecetFloder + "/Control");
            AssetDatabase.Refresh();
        }
    }
    #endregion

}
