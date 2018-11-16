using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class WebRequest : MonoBehaviour
{
    public void CDKeyRequest(){
        StartCoroutine(WEBRequestxxx());
    }

    IEnumerator WEBRequestxxx()
    {
        print("携程");
        Dictionary<string, string> dic = new Dictionary<string, string>();
        dic.Add("gameid", "12");
        UnityWebRequest webRequest = UnityWebRequest.Post("https://cdkey.longtubas.com/Cdk/shiyong", dic);
        webRequest.SetRequestHeader("apikey", "geMLnco7TiehcfddrB16NuSV3lUBn1Yz");
        yield return webRequest.Send();
        if (webRequest.error != null)
        {
            print("error");
            print(webRequest.error);
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
