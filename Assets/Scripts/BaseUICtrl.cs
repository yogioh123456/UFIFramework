using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

public class BaseUICtrl
{
    //面板是否依赖于场景
    public bool isScenePanel;
    
    public virtual void OpenPanel(){}

    public virtual void ClosePanel(){}
    
    public virtual void Dispose(){}
}
