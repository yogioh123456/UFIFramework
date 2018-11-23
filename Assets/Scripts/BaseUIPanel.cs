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
        //先关掉
        mainView.visible = false;
        //

    }
    
    /*
    protected virtual void GetFGUIComp()
    {

    }
    */
}
