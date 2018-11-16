using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 弹出框window类型_control
/// </summary>
public class TestPopUI_Control : MonoBehaviour
{
    public TestPopUI_View myWindow;

    public void Init()
    {
        myWindow.btn_confirm.onClick.Add(delegate ()
        {
            Debug.Log("弹窗按钮");
        });
    }

    /// <summary>
    /// 打开
    /// </summary>
    public void OpenWindow(){
        myWindow.window.Show();
    }

    /// <summary>
    /// 关闭
    /// </summary>
    public void CloseWindow(){
        myWindow.window.Hide();
    }
}
