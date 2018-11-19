using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//面板: Friend
public class UI_FriendControl : BaseUICtrl
{
    //面板
    public UI_FriendView myPanel;

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        myPanel.Button_confirm.onClick.Add(delegate(){
        
        });
         myPanel.Button_back.onClick.Add(delegate(){
        
        });
         
    }

    /// 面板打开
    void OpenPanel(string panelName)
    {
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