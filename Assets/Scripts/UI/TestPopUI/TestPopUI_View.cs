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

    public void OnCreatePanel()
    {
        base.OnCreate("Package1", "SelectRole");
    }

    protected override void GetFGUIComp()
    {
        btn_confirm = window.GetChild("Button_Confirm").asButton;
        loader_pic = window.GetChild("Loader_pic").asLoader;
    }
}
