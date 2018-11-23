using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

/// <summary>
/// 弹出框window类型_view
/// </summary>
public class TestPopUI_View : BaseUIWindow
{
    
    public GButton btn_confirm;
    public GLoader loader_pic;

    public TestPopUI_View()
    {
        OnCreatePanel();
        GetFGUIComp();
    }
    
    public void OnCreatePanel()
    {
        //包名，组件名，模态窗口，点击空白处关闭（可选）
        base.OnCreate("Package1", "Bag", true, true);
    }

    protected void GetFGUIComp()
    {
        btn_confirm = mainView.GetChild("Button_Confirm").asButton;
        loader_pic = mainView.GetChild("Loader_pic").asLoader;
    }
}
