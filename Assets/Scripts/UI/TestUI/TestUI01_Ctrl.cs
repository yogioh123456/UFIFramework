using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

/// <summary>
/// Test user interface 01 ctrl.
/// Ctrl层
/// </summary>
public class TestUI01_Ctrl : BaseUICtrl
{
    public GComponent mainView;
    //面板
    public TestUI01 myPanel;

    /// <summary>
    /// 构造函数 初始化
    /// </summary>
    public TestUI01_Ctrl(){
        myPanel = new TestUI01();
        mainView = myPanel.mainView;
        
        //UIManager.Instance.uiPanelList.Add(myPanel.GetType().ToString(), myPanel);
        UIManager.Instance.uiPanelCtrl.Add(this.GetType().ToString(), this);

        ButtonAddClick();
    }

    /// <summary>
    /// 按钮添加事件
    /// </summary>
    void ButtonAddClick()
    {
        myPanel.btn_login.onClick.Add(delegate ()
        {
            Debug.Log("haha!!!");
            Debug.Log(myPanel.GetType());
            //OpenPanel("TestUI02_View");
            UIManager.Instance.OpenUIPanel<TestUI02_Control>();
        });
    }
    
    /// <summary>
    /// 打开另外一个面版
    /// </summary>
    /// <param name="panelName">Panel name.</param>
    public void OpenNewPanel<T>() where T: new()
    {
        UIManager.Instance.OpenUIPanel<T>();
    }
    
    /// <summary>
    /// 面板回退
    /// </summary>
    void BackPanel()
    {
        UIManager.Instance.BackUIPanel();
    }
    
    public override void OpenPanel()
    {
        mainView.visible = true;
    }

    public override void ClosePanel()
    {
        mainView.visible = false;
    }
    
    public override void Dispose()
    {
        mainView.Dispose();
    }
}
