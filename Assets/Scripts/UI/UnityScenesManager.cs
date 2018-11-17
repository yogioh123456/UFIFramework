using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// unity场景管理器
/// 我觉得场景也可以用栈管理，像ui那样，返回上一场景
/// </summary>
public class UnityScenesManager:Singleton<UnityScenesManager>
{
    //当前场景名称
    public string curSceneName = "";
    public AsyncOperation loadMapOperation;
    public TaskCompletionSource<bool> tcs;

    /// <summary>
    /// 如果你想知道场景什么时候切换完成，那么下面的这部分代码，需要在你那部分逻辑地方写
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadScene(string sceneName, Action ac)
    {
        LoadAsync(sceneName, ac);//建立异步任务
        tcs.SetResult(true);//执行任务
    }
    
    async void LoadAsync(string sceneName, Action ac)
    {
        try
        {
            await ChangeSceneAsync(sceneName);
            curSceneName = sceneName;
            Debug.Log("切换至场景" + curSceneName);
            ac();
        }
        catch (Exception e)
        {
            Debug.Log(e);
            throw;
        }
    }
    
    //切换场景
    public Task<bool> ChangeSceneAsync(string sceneName)
    {
        tcs = new TaskCompletionSource<bool>();
        loadMapOperation = SceneManager.LoadSceneAsync(sceneName);
        return tcs.Task;
    }
    
    //获取进度，需要优化下
    public int Process
    {
        get
        {
            if (this.loadMapOperation == null)
            {
                return 0;
            }
            return (int)(this.loadMapOperation.progress * 100);
        }
    }
}
