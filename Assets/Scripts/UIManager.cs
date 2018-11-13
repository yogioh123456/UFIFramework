using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

/// <summary>
/// UI管理器
/// GRoot里提供了一些窗口管理的常用API。
/// BringToFront 把窗口提到所有窗口的最前面。
/// CloseAllWindows 隐藏所有窗口。注意不是销毁。
/// CloseAllExceptModals 隐藏所有非模态窗口。
/// GetTopWindow 返回当前显示在最上面的窗口。
/// hasModalWindow 当前是否有模态窗口在显示。
/// </summary>
public class UIManager : Singleton<UIManager>
{
    public const int UIWidth = 1920;
    public const int UIHeight = 1080;
    //栈结构维护基础面板
    public Stack<BaseUIPanel> uiPanelStack = new Stack<BaseUIPanel>();
    //创建的面板
    public Dictionary<string, BaseUIPanel> uiPanelList = new Dictionary<string, BaseUIPanel>();
    public Dictionary<string, BaseUICtrl> uiPanelCtrl = new Dictionary<string, BaseUICtrl>();

    public UIManager(){
        GRoot.inst.SetContentScaleFactor(UIWidth, UIHeight);
    }

    public void Test(){
        UIPackage.AddPackage("Assets/Editor Default Resources/FairyGuiPublish/Package1");

        TestUI01 t01 = new TestUI01();
        TestUI01_Ctrl tc01 = new TestUI01_Ctrl();
        t01.OnCreatePanel();
        tc01.myPanel = t01;
        tc01.Init();
        uiPanelList.Add(t01.GetType().ToString(), t01);
        uiPanelCtrl.Add(tc01.GetType().ToString(), tc01);

        TestUI02_View t02 = new TestUI02_View();
        TestUI02_Control tc02 = new TestUI02_Control();
        t02.OnCreatePanel();
        tc02.myPanel = t02;
        tc02.Init();
        uiPanelList.Add(t02.GetType().ToString(), t02);
        uiPanelCtrl.Add(tc02.GetType().ToString(), tc02);

        OpenUIPanel("TestUI01");
    }

    /// <summary>
    /// 打开面板
    /// </summary>
    /// <param name="panelName">Panel name.</param>
    public void OpenUIPanel(string panelName){
        if (uiPanelList.ContainsKey(panelName))
        {
            uiPanelList[panelName].OpenPanel();
            if (uiPanelStack.Count > 0)
            {
                BaseUIPanel peakPanel = uiPanelStack.Peek();
                //上个界面关掉
                peakPanel.ClosePanel();
            }
            uiPanelStack.Push(uiPanelList[panelName]);//入栈
        }
        else{
            Debug.LogError("没有这个面板");
        }
        Debug.Log(uiPanelStack.Count);
    }

    /// <summary>
    /// 面板回退
    /// </summary>
    public void BackUIPanel(){
        Debug.Log(uiPanelStack.Count);
        if (uiPanelStack.Count > 1)
        {
            BaseUIPanel peakPanel = uiPanelStack.Peek();
            peakPanel.ClosePanel();
            uiPanelStack.Pop();
            peakPanel = uiPanelStack.Peek();
            peakPanel.OpenPanel();
        }
    }
}
