using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

//面板: Friend
public class UI_FriendControl : BaseUICtrl
{
    public GComponent mainView;
    //面板
    public UI_FriendView myPanel;

    public UI_FriendControl()
    {
        myPanel = new UI_FriendView();
        mainView = myPanel.mainView;
        isScenePanel = true;//是否依赖于场景
        UIManager.Instance.uiPanelCtrl.Add(this.GetType().ToString(), this);

        ButtonAddClick();
    }

    /// <summary>
    /// 按钮添加事件
    /// </summary>
    public void ButtonAddClick()
    {
        //------------------按钮添加事件-----------------
        myPanel.Button_confirm.onClick.Add(delegate(){
        
        });
        myPanel.Button_back.onClick.Add(delegate(){
        
        });
        
    }

    //打开新面板
    void OpenNewPanel<T>() where T: new()
    {
        UIManager.Instance.OpenUIPanel<T>();
    }
    //打开新窗口
    public void OpenNewWindow<T>() where T : new()
    {
        UIManager.Instance.OpenUIWindow<T>();
    }
    //面板回退
    void BackPanel()
    {
        UIManager.Instance.BackUIPanel();
    }
    //打开面板
    public override void OpenPanel()
    {
        mainView.visible = true;
    }
    //关闭面板
    public override void ClosePanel()
    {
        mainView.visible = false;
    }
    //面板销毁
    public override void Dispose()
    {
        mainView.Dispose();
    }
}