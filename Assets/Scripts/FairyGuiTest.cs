using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

public class FairyGuiTest : MonoBehaviour
{
    // Start is called before the first frame update 
    void Start()
    {
        LoadFGUIPack();
    }

    // Update is called once per frame 
    void Update()
    {

    }

    void LoadFGUIPack()
    {
        UIPackage.AddPackage("Assets/Editor Default Resources/FairyGuiPublish/Package1");
        /* 
        UIPanel panel = gameObject.AddComponent<UIPanel>(); 
        panel.packageName = "Package1"; 
        panel.componentName = "Main"; 
        panel.CreateUI(); 
        */

        /* 
        GComponent view = UIPackage.CreateObject("Package1", "Main").asCom; 
        //1，直接加到GRoot显示出来 
        GRoot.inst.AddChild(view); 
        //2，使用窗口方式显示 
        aWindow.contentPane = view; 
        aWindow.Show(); 
        //3，加到其他组件里 
        aComponnent.AddChild(view); 
        */

        GRoot.inst.SetContentScaleFactor(1920, 1080);
        GComponent mainView = UIPackage.CreateObject("Package1", "Main").asCom;
        mainView.SetSize(GRoot.inst.width, GRoot.inst.height);
        mainView.AddRelation(GRoot.inst, RelationType.Size);
        GRoot.inst.AddChild(mainView);

        GButton btn = mainView.GetChild("Button_login").asButton;
        btn.onClick.Add(delegate ()
        {
            Debug.Log("haha");
        });
    }
}
