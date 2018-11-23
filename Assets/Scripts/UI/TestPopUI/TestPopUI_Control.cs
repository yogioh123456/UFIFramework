using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

/// <summary>
/// 弹出框window类型_control
/// </summary>
public class TestPopUI_Control : BaseUICtrl
{
    //public GComponent mainView;
    public TestPopUI_View myWindow;

    public TestPopUI_Control()
    {
        myWindow = new TestPopUI_View();
        ButtonAddClick();
        
        UIManager.Instance.uiWindowCtrl.Add(this.GetType().ToString(), this);
    }

    public void ButtonAddClick()
    {
        myWindow.btn_confirm.onClick.Add(delegate ()
        {
            Debug.Log("弹窗按钮");
            //关闭
            ClosePanel();
            //销毁
            //myWindow.window.Dispose();
        });
    }
    
    public override void OpenPanel()
    {
        myWindow.window.Show();
    }

    public override void ClosePanel()
    {
        myWindow.window.Hide();
    }
    
    public override void Dispose()
    {
        myWindow.window.Dispose();
    }
}
