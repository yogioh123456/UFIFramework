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
///
/// BaseUICtrl 里面有个isScenePanel用来标注面板是否依赖场景
/// 在切换场景时，遍历存储panel的字典，把true的面板给释放掉
///
/// ui拼接，如果2个或多个面板有同一种元素，那么这个元素可以单独拿出来写个脚本（比如：金币，钻石，体力）
/// </summary>
public class UIManager : Singleton<UIManager>
{
    public const int UIWidth = 1920;
    public const int UIHeight = 1080;
    //栈结构维护基础面板
    public Stack<BaseUICtrl> uiCtrlStack = new Stack<BaseUICtrl>();
    //创建的面板
    public Dictionary<string, BaseUICtrl> uiPanelCtrl = new Dictionary<string, BaseUICtrl>();
    //窗口字典
    public Dictionary<string, BaseUICtrl> uiWindowCtrl = new Dictionary<string, BaseUICtrl>();
    

    public UIManager(){
        GRoot.inst.SetContentScaleFactor(UIWidth, UIHeight);
    }

    public void Test(){
        UIPackage.AddPackage("Assets/Editor Default Resources/FairyGuiPublish/Package1");

        //创建第一个面板
        //new TestUI01_Ctrl();

        //创建第二个面板
        /*
        TestUI02_View t02 = new TestUI02_View();
        TestUI02_Control tc02 = new TestUI02_Control();
        t02.OnCreatePanel();
        tc02.myPanel = t02;
        tc02.Init();
        uiPanelList.Add(t02.GetType().ToString(), t02);
        uiPanelCtrl.Add(tc02.GetType().ToString(), tc02);
        */

        OpenUIPanel<TestUI01_Ctrl>();
    }

    
    
    /// <summary>
    /// 打开面板
    /// </summary>
    /// <param name="panelName">Panel name.</param>
    public void OpenUIPanel<T>() where T: new()
    {
        string panelName = typeof(T).ToString();
        /*
        if (uiPanelCtrl.ContainsKey(panelName))
        {
            //uiPanelCtrl[panelName].OpenPanel();
        }
        else{
            Debug.LogError("没有这个面板");
            //通过字符串 创建对象，然后再去尝试打开
            //有必要这样做吗?
            //或者是把类传过来,上面的判断还是可以把类转成字符串判断,但是这样好吗？增加了耦合？
            //优化方案：打开时候不卡顿，那就提前new就好了，因为第一次打开是创建，优化就是事先创建，就不会卡顿了
            new T();
        }
        */

        if (!uiPanelCtrl.ContainsKey(panelName))
        {
            new T();
        }
        
        uiPanelCtrl[panelName].OpenPanel();
        if (uiCtrlStack.Count > 0)
        {
            //上个界面关掉
            BaseUICtrl peakCtrl = uiCtrlStack.Peek();
            peakCtrl.ClosePanel();
        }
        uiCtrlStack.Push(uiPanelCtrl[panelName]);//入栈
    }

    /// <summary>
    /// 打开窗口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void OpenUIWindow<T>() where T: new()
    {
        string windowName = typeof(T).ToString();
        if (!uiWindowCtrl.ContainsKey(windowName))
        {
            new T();
        }
        uiWindowCtrl[windowName].OpenPanel();
    }

    /// <summary>
    /// 面板回退
    /// </summary>
    public void BackUIPanel()
    {
        Debug.Log(uiCtrlStack.Count);
        if (uiCtrlStack.Count > 1)
        {
            BaseUICtrl peakCtrl = uiCtrlStack.Peek();
            peakCtrl.ClosePanel();
            uiCtrlStack.Pop();
            peakCtrl = uiCtrlStack.Peek();
            peakCtrl.OpenPanel();
        }
    }
}
