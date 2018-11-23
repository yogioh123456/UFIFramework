using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

public class TestUI02_Control : BaseUICtrl
{
    public GComponent mainView;
    //面板
    public TestUI02_View myPanel;

    public TestUI02_Control(){
        myPanel = new TestUI02_View();
        mainView = myPanel.mainView;
        
        //UIManager.Instance.uiPanelList.Add(myPanel.GetType().ToString(), myPanel);
        UIManager.Instance.uiPanelCtrl.Add(this.GetType().ToString(), this);

        Init();
    }
    
    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        myPanel.btn_back.onClick.Add(BackPanel);
        myPanel.btn_pop.onClick.Add(delegate()
        {
            Debug.Log("弹窗");
            //切换场景
            //Debug.Log(UnityScenesManager.Instance.curSceneName);
            UIManager.Instance.OpenUIWindow<TestPopUI_Control>();
            //UIManager.Instance.OpenUIWindow<UI_FriendWindowControl>();
        });
        myPanel.btn_Next.onClick.Add(delegate()
        {
            //切换场景
            Debug.Log("切换场景");
            UnityScenesManager.Instance.LoadScene("Main2", delegate
            {
                Debug.Log("切换完成");
            });
        });
    }

    /// <summary>
    /// 打开新面版
    /// </summary>
    /// <param name="panelName">Panel name.</param>
    public void OpenNewPanel<T>() where T: new()
    {
        UIManager.Instance.OpenUIPanel<T>();
    }

    /// <summary>
    /// 打开新窗口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void OpenNewWindow<T>() where T : new()
    {
        UIManager.Instance.OpenUIWindow<T>();
    }
    
    /// <summary>
    /// 面板回退
    /// </summary>
    void BackPanel()
    {
        UIManager.Instance.BackUIPanel();
    }
    
    public override void OpenPanel()
    {
        mainView.visible = true;
    }

    public override void ClosePanel()
    {
        mainView.visible = false;
    }
    
    public override void Dispose()
    {
        mainView.Dispose();
    }
}
