using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

/// <summary>
/// UI面板 Friend
/// 类型 Panel
/// 注意：本段代码由系统自动生成
/// </summary>
public class UI_FriendView : BaseUIPanel
{
    //---------------字段---------------
    public GButton Button_confirm;
    public GButton Button_back;
    public GList List_friend;
    public GLoader Loader_head;
    

    public UI_FriendView()
    {
        OnCreatePanel();
        GetFGUIComp();
    }

    //创建面板
    public void OnCreatePanel(){
        base.OnCreate("Package1", "Friend");
    }

    //获取组件的方法
    protected void GetFGUIComp(){
        Button_confirm = mainView.GetChild("Button_confirm").asButton;
        Button_back = mainView.GetChild("Button_back").asButton;
        List_friend = mainView.GetChild("List_friend").asList;
        Loader_head = mainView.GetChild("Loader_head").asLoader;
        
    }
}