using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Test user interface 01 ctrl.
/// Ctrl层
/// </summary>
public class TestUI01_Ctrl : BaseUICtrl
{
    //面板
    public TestUI01 myPanel;

    /// <summary>
    /// 构造函数 初始化
    /// </summary>
    public TestUI01_Ctrl(){
        myPanel = new TestUI01();
        myPanel.OnCreatePanel();
        UIManager.Instance.uiPanelList.Add(myPanel.GetType().ToString(), myPanel);
        UIManager.Instance.uiPanelCtrl.Add(this.GetType().ToString(), this);
        
        myPanel.btn_login.onClick.Add(delegate ()
        {
            Debug.Log("haha!!!");
            Debug.Log(myPanel.GetType());
            OpenPanel("TestUI02_View");
        });
    }

    /// <summary>
    /// 面板打开
    /// </summary>
    /// <param name="panelName">Panel name.</param>
    void OpenPanel(string panelName){
        UIManager.Instance.OpenUIPanel(panelName);
    }

    /// <summary>
    /// 面板回退
    /// </summary>
    void BackPanel()
    {
        UIManager.Instance.BackUIPanel();
    }
}
