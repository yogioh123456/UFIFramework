# UFIFramework
Unity+FairyGui+ILRuntime游戏框架

1.FairyGui作为游戏UI框架，并且提供代码自动生成工具,自动生成UI的view层脚本，和基础的control层脚本
生成之后view层代码不需要再修改了，逻辑写在control层即可
UIManager作为游戏的UI管理框架，采用堆栈结构控制UI，实现UI的自动控制

2.ILRuntime作为游戏的热更脚本（可以不用去写lua了，全C#开发）

3.采用反射，和打标签的方式实现实现消息机制的自动监听
在脚本里调用this.RegisterEvent();即可对此类所有打了[EventMsg]标签的脚本进行自动监听
this.UnregisterEvent(); 对应为移除消息监听

4.数据管理类

后续的以后再补充。。。。。。
