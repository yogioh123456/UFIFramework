using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

/// <summary>
/// Demo 面板01
/// View 由代码自动生成
/// </summary>
public class TestUI01 : BaseUIPanel
{
    public GButton btn_login;
    public GTextField text_title;

    public override void OnCreatePanel(){
        base.OnCreate("Package1", "Main");
    }

    protected override void GetFGUIComp(){
        btn_login = mainView.GetChild("Button_login").asButton;
        text_title = mainView.GetChild("Text_title").asTextField;
    }
}
