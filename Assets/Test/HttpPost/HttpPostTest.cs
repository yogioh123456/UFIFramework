using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HttpPostTest : MonoBehaviour
{
    // Start is called before the first frame update
    public void xStart()
    {
        /*
        WWWForm form = new WWWForm();
        form.AddField("gameid", "50001");
        form.AddField("channelid", "1002");
        form.AddField("serverid", "1");
        form.AddField("playerid", "1001");
        form.AddField("uid", "100001");
        form.AddField("cdkey", "abcd12");
        form.AddField("extension", "6");
        form.AddField("level", "6");
        form.AddField("viplevel", "6");
        StartCoroutine(SendPost("https://cdkey.longtubas.com/Cdk/shiyong", form));
        */

        //WWWForm form2 = new WWWForm();
        //form2.AddField("tel", "17621979287");
        //StartCoroutine(SendPost("https://tcc.taobao.com/cc/json/mobile_tel_segment.htm", form2));

        //StartCoroutine(WEBRequestxxx());

        WebRequest request;
        GameObject go = GameObject.Find("webRequest");
        if (go == null)
        {
            go = new GameObject();
            go.name = "webRequest";
            request = go.AddComponent<WebRequest>();
        }else
        {
            request = go.GetComponent<WebRequest>();
        }

        request.CDKeyRequest();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Test(){

    }

    IEnumerator SendPost(string _url, WWWForm _wForm)
    {
        WWW postData = new WWW(_url, _wForm);
        yield return postData;
        if (postData.error != null)
        {
            Debug.Log(postData.error);
        }
        else
        {
            Debug.Log(postData.text);
        }
    }

    static IEnumerator WEBRequestxxx(){
        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic.Add("gameid", "12");
        UnityWebRequest webRequest = UnityWebRequest.Post("https://cdkey.longtubas.com/Cdk/shiyong", dic);
        webRequest.SetRequestHeader("apikey", "geMLnco7TiehcfddrB16NuSV3lUBn1Yz");
        yield return webRequest.Send();
        print("发送王弼");
        if (webRequest.error != null)
        {
            print("error");
        }
        else
        {
            print("xxxx");
            string returnMessage = webRequest.downloadHandler.text;
            print(returnMessage);
        }
        webRequest.Abort();
    }
}
