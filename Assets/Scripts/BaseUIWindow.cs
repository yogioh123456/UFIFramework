using System.Collections;
using System.Collections.Generic;
using FairyGUI;
using UnityEngine;

/// <summary>
/// 窗口系统
/// </summary>
public class BaseUIWindow
{
    /*常用API
     *Show 显示
     *Hide 隐藏
     *isShowing 窗口是否显示的
     *modal 设置窗口是否模态窗口。模态窗口将阻止用户点击任何模态窗口后面的内容。当模态窗口显示时，模态窗口背后可以自动覆盖一层灰色的颜色，这个颜色可以自定义：
            UIConfig.modalLayerColor = new Color(0f, 0f, 0f, 0.4f);
            如果你不需要这个灰色效果，那么把透明度设置为0即可。
     *ShowModalWait   锁定窗口，不允许任何操作。锁定时可以显示一个提示
     *CloseModalWait  取消窗口的锁定。
     * 
     * 点击空白处关闭，fgui的Popup功能
     *     - 弹出在自定义的位置
     *     - GRoot.inst.ShowPopup(aComponent);
     *     - aComponent.SetXY(100, 100);
     * 
     * 弹出窗口，播放动画：重写父类的doShowAnimation方法
     * 关闭窗口，隐藏动画：重写父类的doHideAnimation方法
     * 新建类，继承window，重写动画方法，建窗口直接new 这个类 就行
     * 创建新类用来表示新的窗口，现在只有2种窗口，panel, window
    */

    public Window window;
    public GComponent mainView;

    public void OnCreate(string packName, string compName, bool isModal, bool isPopup = false){
        window = new Window();
        window.contentPane = UIPackage.CreateObject(packName, compName).asCom;
        mainView = window.contentPane;
        window.modal = isModal;//模态窗口，背后不可点击
        window.Center();
        if (isPopup)
        {
            GRoot.inst.ShowPopup(window);
        }
        else
        {
            window.Show();
        }
        
    }
    
    /*
    protected virtual void GetFGUIComp()
    {

    }
    */
}
