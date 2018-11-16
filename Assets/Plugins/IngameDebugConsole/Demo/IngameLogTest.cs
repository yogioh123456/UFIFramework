using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameLogTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("hahah");
        Debug.LogError("cuowu");
        Debug.LogWarning("111");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LogInfo(){
        Debug.Log("hahah");
    }

    public void LogWarring(){
        Debug.LogWarning("111");
    }

    public void LogErroring(){
        Debug.LogError("cuowu");
    }
}
