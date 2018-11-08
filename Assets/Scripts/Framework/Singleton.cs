using UnityEngine;
using System.Collections;


public abstract class Singleton<T> where T : class, new()
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new T();
            }
            return _instance;
        }
    }
}

//切换场景会被销毁
public class UnityNormalSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                if ((_instance = Object.FindObjectOfType<T>()) == null)
                {
                    GameObject go = new GameObject(typeof(T).ToString());
                    _instance = go.AddComponent<T>();
                }
            }

            return _instance;
        }
    }
}



//切换场景不会被销毁
public class UnitySingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                if ((_instance = Object.FindObjectOfType<T>()) == null)
                {
                    GameObject go = new GameObject(typeof(T).ToString());
                    _instance = go.AddComponent<T>();
                }

                UnityEngine.Object.DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }
}

/////////////////////////////////////////////
//public class UnityNormalBaseSingleton<T> : BaseControl where T : BaseControl
//{
//    private static T _instance;

//    public static T Instance
//    {
//        get
//        {
//            if (_instance == null)
//            {
//                _instance = Object.FindObjectOfType<T>();
//                if (_instance == null)
//                {
//                    GameObject go = new GameObject(typeof(T).ToString());
//                    _instance = go.AddComponent<T>();
//                }
//            }

//            return _instance;
//        }
//    }
//}