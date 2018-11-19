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

        //创建第一个面板
        new TestUI01_Ctrl();

        //创建第二个面板
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
            //通过字符串 创建对象，然后再去尝试打开
            //有必要这样做吗?
            //或者是把类传过来,上面的判断还是可以把类转成字符串判断,但是这样好吗？增加了耦合？
            //优化方案：打开时候不卡顿，那就提前new就好了，因为第一次打开是创建，优化就是事先创建，就不会卡顿了
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
