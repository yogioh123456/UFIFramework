You can get introduction and new release from:
https://github.com/hiramtan/HiDebug_unity/releases


Support multiple platform(unity editor, exe, Android, iOS, WP...).
Enable or disable all logs in one switch(debug mode set true let logs on, release mode set false disable all logs out put).
Whether enable logs on screen(so that you can still check logs if you don't want to connect Android Studio or xcode)
Whether enable write logs into a text(default path is persistent folder, when crash can check logs in text)
Adding data and time append to you logs.
Display stack on screen or record stack in text.
There is only a DLL, you can copy this to your project to use whole functionality



//将会记录日志和堆栈信息到text,默认路径是Application.persistentDataPath.
HiDebug.EnableOnText(true);
//将会显示一个按钮,可以拖拽到任何地方(不遮挡你的游戏按钮的地方)  当点击这个按钮,将会弹出一个面板展示日志和堆栈.
HiDebug.EnableOnScreen(true);
//HiDebug的控制台日志.如果使用Debuger.Log or Debuger.LogWarnning or Debuger.LogError打印日志, 你可以一键关闭这些日志Hidebug.EnableDebuger(false).
HiDebug.EnableDebuger(true);