using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class InputTipsManager : MonoBehaviour
{
    GameObject consoleObject;
    Text textLabel;
    InputField inputField;
    GameObject gridLayoutGroup;
    ScrollRect scrollRect;
    RectTransform rectTransform;

    //下拉数据存储
    List<string> dropDownList = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        //赋值
        consoleObject = transform.Find("CanvasConsole").gameObject;
        textLabel = consoleObject.transform.Find("Scroll View/Viewport/Content").GetComponent<Text>();
        rectTransform = consoleObject.transform.Find("Scroll View/Viewport/Content").GetComponent<RectTransform>();
        textLabel.text = "~~~控制台~~~";
        scrollRect = consoleObject.transform.Find("Scroll View").GetComponent<ScrollRect>();
        inputField = consoleObject.transform.Find("InputField").GetComponent<InputField>();
        inputField.onValueChanged.AddListener(delegate {
            InputHelp();
        });
        gridLayoutGroup = consoleObject.transform.Find("GridGroup").gameObject;
        gridLayoutGroup.SetActive(false);
        consoleObject.SetActive(false);
        InnerMethod();


        //测试数据demo
        ConsoleCommand.Instance.RegisterCommand("test01", delegate
        {
            print("haha1");
            return "test01";
        });
        ConsoleCommand.Instance.RegisterCommand("test001", delegate
        {
            print("haha2");
            return "test01haha";
        });
        ConsoleCommand.Instance.RegisterCommand("test0001", delegate
        {
            print("haha3");
            return "test01xxxaaaa";
        });
        //带参数demo(参数用空格隔开)
        ConsoleCommand.Instance.RegisterCommand("test00canshu", TestMethod);
        //多播demo
        ConsoleCommand.Instance.RegisterMulCommand("qtxest0001", delegate
        {
            print("hahasas3");
            return "test01xxx11111aaaa";
        });
        ConsoleCommand.Instance.RegisterMulCommand("qtxest0001", delegate
        {
            print("hahasas311111222");
            return "test01xxx11111aaaa222222";
        });
    }
    //测试带参数方法，参数用空格隔开
    string TestMethod(params string[] args){
        string s = "带参数的方法";
        for (int i = 0; i < args.Length; i++)
        {
            s += args[i];
        }
        return s;
    }

    //按键检测
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            consoleObject.SetActive(!consoleObject.activeSelf);
        }

        if (consoleObject.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            //处理字符串
            string result = "";
            string inputText = inputField.text;
            string[] parts = inputText.Split(' ');
            string command = parts[0];
            string[] args = parts.Skip(1).ToArray();
            //执行命令
            result += ConsoleCommand.Instance.ExecuteCommand(command,args);
            textLabel.text += "\n> " + inputField.text;
            inputField.text = "";
            textLabel.text += "\n" + result;
            gridLayoutGroup.SetActive(false);
            //Content Size Fitter这个组件会慢一帧更新,所以才会导致不能彻底滚到最底端，调用rebuild即可
            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
            //滑到最底端
            scrollRect.verticalNormalizedPosition = 0;
            //设置焦点
            inputField.ActivateInputField();
        }
    }

    //内部命令
    void InnerMethod(){
        //菜单
        ConsoleCommand.Instance.RegisterCommand("-help帮助", delegate
        {
            print("全命令菜单");
            string r = "全命令菜单";
            string[] strList = ConsoleCommand.Instance.GetAllKey();
            foreach (var item in strList)
            {
                r += "\n> " + item;
            }
            return r;
        });
    }

    //检查输入，每输入一个字符就会调用一次
    public void InputHelp(){
        gridLayoutGroup.SetActive(true);
        //print(inputField.text);
        string inputContent = inputField.text;
        List<GameObject> childs = new List<GameObject>();
        int childCount = gridLayoutGroup.transform.childCount;
        for (int i = 0; i < childCount; i++)
        {
            GameObject item = gridLayoutGroup.transform.GetChild(i).gameObject;
            childs.Add(item);
            item.SetActive(false);
        }

        dropDownList.Clear();
        //避免全显示
        if (string.IsNullOrEmpty(inputField.text))
        {
            return;
        }

        //遍历
        List<string> strlist = ConsoleCommand.Instance.SearchCommands(inputContent);
        foreach (var v in strlist)
        {
            dropDownList.Add(v);
            int index = dropDownList.Count;
            if (childs.Count >= index)
            {
                childs[index - 1].SetActive(true);
                EditText(childs[index - 1], v);
            }
            else
            {
                GameObject go = Instantiate(Resources.Load<GameObject>("Option"));
                go.transform.parent = gridLayoutGroup.transform;
                EditText(go, v);
            }
        }
    }

    //cell编辑文字
    void EditText(GameObject gameObject, string content){
        gameObject.GetComponent<Button>().onClick.RemoveAllListeners();
        gameObject.GetComponent<Button>().onClick.AddListener(delegate
        {
            inputField.text = content;
            gridLayoutGroup.SetActive(false);
        });
        gameObject.GetComponentInChildren<Text>().text = content;
    }
}
