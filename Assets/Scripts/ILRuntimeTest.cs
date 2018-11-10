using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using ILRuntime.Runtime.Enviorment;
using UnityEngine;

public class ILRuntimeTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LoadHotFixAssembly();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //全局唯一
    AppDomain appdomain;
    async void LoadHotFixAssembly(){
        appdomain = new ILRuntime.Runtime.Enviorment.AppDomain();
        //pc路径
        WWW www = new WWW("file:///" + Application.streamingAssetsPath + "/ILRuntimeDLL/Hotfix_Project.dll");
        //WWW www = new WWW("file:///" + Application.streamingAssetsPath + "/HotFix_Project.dll");
        while (!www.isDone){
            await Task.Yield();
        }
        if (!string.IsNullOrEmpty(www.error))
            UnityEngine.Debug.LogError(www.error);
        byte[] dll = www.bytes;
        www.Dispose();

        //pc路径
        www = new WWW("file:///" + Application.streamingAssetsPath + "/ILRuntimeDLL/Hotfix_Project.pdb");
        //www = new WWW("file:///" + Application.streamingAssetsPath + "/HotFix_Project.pdb");
        while (!www.isDone)
        {
            await Task.Yield();
        }
        if (!string.IsNullOrEmpty(www.error))
            UnityEngine.Debug.LogError(www.error);
        byte[] pdb = www.bytes;
        using (System.IO.MemoryStream fs = new MemoryStream(dll))
        {
            //mac上生成的pdb有问题,先传null，
            //pdb文件只是调试用
            using (System.IO.MemoryStream p = new MemoryStream(pdb))
            {
                appdomain.LoadAssembly(fs, null, new Mono.Cecil.Pdb.PdbReaderProvider());
            }
        }

        InitializeILRuntime();
        OnHotFixLoaded();
    }

    void InitializeILRuntime()
    {
        //这里做一些ILRuntime的注册，测试示例暂时没有需要注册的
    }

    void OnHotFixLoaded()
    {
        //测试方法
        object v = appdomain.Invoke("Hotfix_Project.TestClass", "TestMethod1", null, null);
        Debug.Log(v);
        //appdomain.Invoke("HotFix_Project.InstanceClass", "StaticFunTest", null, null);
    }
}
