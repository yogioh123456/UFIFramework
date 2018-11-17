using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUI02_Control : BaseUICtrl
{
    //面板
    public TestUI02_View myPanel;

    /// <summary>
    /// 初始化
    /// </summary>
    public void Init()
    {
        myPanel.btn_back.onClick.Add(BackPanel);
        myPanel.btn_pop.onClick.Add(delegate()
        {
            Debug.Log("弹窗");
            Debug.Log(UnityScenesManager.Instance.curSceneName);
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
    /// 面板打开
    /// </summary>
    /// <param name="panelName">Panel name.</param>
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
