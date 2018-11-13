using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

/// <summary>
/// 基础UI面板
/// 特性：全屏UI
/// 
/// </summary>
public class BaseUIPanel
{
    public GComponent mainView;

    /// <summary>
    /// 创建面板
    /// </summary>
    /// <param name="packName">Pack name.</param>
    /// <param name="compName">Comp name.</param>
    protected void OnCreate(string packName, string compName)
    {
        mainView = UIPackage.CreateObject(packName, compName).asCom;
        mainView.SetSize(GRoot.inst.width, GRoot.inst.height);
        mainView.AddRelation(GRoot.inst, RelationType.Size);
        GRoot.inst.AddChild(mainView);
        GetFGUIComp();
        //先关掉
        mainView.visible = false;
        //

    }

    protected virtual void GetFGUIComp(){

    }

    /// <summary>
    /// 打开面板
    /// </summary>
    public void OpenPanel()
    {
        mainView.visible = true;
        //或者把oncreate写在这里，判断mainView值是否为空，空去调用oncreate，不是空直接设置visible
    }

    /// <summary>
    /// 关闭面板，一般情况不需要主动调用的，在切换界面时，框架会自动调用
    /// </summary>
    public void ClosePanel()
    {
        mainView.visible = false;
    }
}
