using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

public class FairyGuiTest : MonoBehaviour
{
    // Start is called before the first frame update 
    void Start()
    {
        LoadFGUIPack();
    }

    // Update is called once per frame 
    void Update()
    {

    }

    void LoadFGUIPack()
    {
        UIPackage.AddPackage("Assets/Editor Default Resources/FairyGuiPublish/Package1");
        /* 
        UIPanel panel = gameObject.AddComponent<UIPanel>(); 
        panel.packageName = "Package1"; 
        panel.componentName = "Main"; 
        panel.CreateUI(); 
        */

        /* 
        //动态创建ui 以下3种方式都可以将view显示出来：
        GComponent view = UIPackage.CreateObject("Package1", "Main").asCom; 
        //1，直接加到GRoot显示出来 
        GRoot.inst.AddChild(view); 
        //2，使用窗口方式显示 
        aWindow.contentPane = view; 
        aWindow.Show(); 
        //3，加到其他组件里 
        aComponnent.AddChild(view); 
        */
        /*
        //可用
        Window win = new Window();
        win.contentPane = UIPackage.CreateObject("Package1", "Main").asCom;
        win.Show();
        */

        //动态创建面板
        GRoot.inst.SetContentScaleFactor(1920, 1080);
        GComponent baseUI = new GComponent();//基础UI层
        GComponent popUI = new GComponent();//弹窗层
        GComponent topUI = new GComponent();//最顶层,跑马灯层
        popUI.width = 1920;
        popUI.height = 1080;
        popUI.AddRelation(GRoot.inst, RelationType.Size);
        GRoot.inst.AddChild(baseUI);
        GRoot.inst.AddChild(popUI);
        GRoot.inst.AddChild(topUI);//默认上对齐

        //Main UI
        GComponent mainView = UIPackage.CreateObject("Package1", "Main").asCom;
        //优化drawCall
        //mainView.fairyBatching = true;
        mainView.SetSize(GRoot.inst.width, GRoot.inst.height);
        mainView.AddRelation(GRoot.inst, RelationType.Size);
        //GRoot.inst.AddChild(mainView);
        baseUI.AddChild(mainView);
        GButton btn = mainView.GetChild("Button_login").asButton;
        btn.onClick.Add(delegate ()
        {
            Debug.Log("haha");
            mainView.visible = false;
        });

        //Info UI
        GComponent infoComp = UIPackage.CreateObject("Package1", "Info").asCom;
        infoComp.AddRelation(GRoot.inst, RelationType.Size);
        topUI.AddChild(infoComp);
        topUI.sortingOrder = 100;//设置层级

        //Pop UI
        /*
        GComponent bagComp = UIPackage.CreateObject("Package1", "Bag").asCom;
        bagComp.AddRelation(popUI, RelationType.Size);
        //设置背景
        GGraph bg = new GGraph();
        bg.SetSize(1920, 1080);
        bg.DrawRect(1920, 1080, 0, new Color(1, 1, 1), new Color(0, 0, 0, 0.6f));
        bg.AddRelation(popUI, RelationType.Size);
        bg.onClick.Add(delegate ()
        {
            Debug.Log("lala");
            bg.visible = false;
            bagComp.visible = false;
        });
        popUI.AddChild(bg);
        popUI.AddChild(bagComp);
        bagComp.Center();//窗口居中
        */


        //moudle UI (在任何层的上面，建议不要控制它的层级，强行控制的话，它的黑底显示有问题,除非不使用它的黑底，不打开模态开关，自己去实现个黑底)
        Window window = new Window();
        window.contentPane = UIPackage.CreateObject("Package1", "Bag").asCom;
        window.modal = true;
        window.Center();//居中
        //window.Show();//普通打开
        GRoot.inst.ShowPopup(window);//点击空白地方可关闭
        //popUI.AddChild(window);

        //2018年11月13日17:10:09 总结：
        /*
         * 我觉得还是没有必要分层
         * 处于最上层的跑马灯
         * sortingOrder的值改成100就行了，也不会被模态窗口挡住
         * 这种窗口从需求上来说并不是很多的
         * 就是HUD需要单独处理下这个样子
         * 再写个合并ui的例子就行了，就是ui上面的常见的 金币 钻石 体力
         * 把上面的这些例子做成Demo
         * 再研究下代码分层和IL的一键切换(ET)
         * 堆栈管理全屏UI
         * 
        */
    }
}
