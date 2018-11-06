//****************************************************************************
// Description:download newest version from: https://github.com/hiramtan/HiDebug_unity/releases
// Author: hiramtan@live.com
//****************************************************************************

using UnityEngine;

public class Example1 : MonoBehaviour
{
    // Use this for initialization
    void Start()
    {
        //将会记录日志和堆栈信息到text,默认路径是Application.persistentDataPath.
        HiDebug.EnableOnText(true);
        //将会显示一个按钮,可以拖拽到任何地方(不遮挡你的游戏按钮的地方)  当点击这个按钮,将会弹出一个面板展示日志和堆栈.
        HiDebug.EnableOnScreen(true);
        //HiDebug的控制台日志.如果使用Debuger.Log or Debuger.LogWarnning or Debuger.LogError打印日志, 你可以一键关闭这些日志Hidebug.EnableDebuger(false).
        HiDebug.EnableDebuger(true);

        Use_Debuger();
        Use_Debug();
    }

    /// <summary>
    /// use debuger, you can enable or disable logs just one switch
    /// and also it automatically add time to your logs 
    /// </summary>
    void Use_Debuger()
    {
        //you can set all debuger's out put logs disable just set this value false(pc,android,ios...etc)
        //it's convenient in release mode, just set this false, and in debug mode set this true.


        for (int i = 0; i < 100; i++)
        {
            Debuger.Log(i);
            Debuger.LogWarning(i);
            Debuger.LogError(i);
        }
    }

    /// <summary>
    /// if you donnt want use Debuger.Log()/Debuger.LogWarnning()/Debuger.LogError()
    /// you can still let UnityEngine's Debug on your screen or write them into text
    /// </summary>
    void Use_Debug()
    {
        for (int i = 0; i < 100; i++)
        {
            Debug.Log(i);
            Debug.LogWarning(i);
            Debug.LogError(i);
        }
    }
}
