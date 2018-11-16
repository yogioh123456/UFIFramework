using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

public class TestUI02_View : BaseUIPanel
{
    public GButton btn_back;
    public GButton btn_pop;

    public void OnCreatePanel()
    {
        base.OnCreate("Package1", "SelectRole");
    }

    protected override void GetFGUIComp()
    {
        btn_back = mainView.GetChild("Button_back").asButton;
        btn_pop = mainView.GetChild("Button_pop").asButton;
    }
}
