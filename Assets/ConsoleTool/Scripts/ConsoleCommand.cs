using System.Collections;
using System.Collections.Generic;

public delegate string ConsoleCommandCallBack(params string[] args);

public class ConsoleCommand
{
    //单例模式
    static ConsoleCommand instance;
    public static ConsoleCommand Instance{
        get{
            if (instance == null)
            {
                instance = new ConsoleCommand();
            }
            return instance;
        }
    }

    Dictionary<string, ConsoleCommandCallBack> repository = new Dictionary<string, ConsoleCommandCallBack>();
    //注册
    public void RegisterCommand(string command, ConsoleCommandCallBack callback)
    {
        repository[command] = new ConsoleCommandCallBack(callback);
    }
    //注册--支持一个命令调用多条方法，但是只会输出1条命令
    public void RegisterMulCommand(string command, ConsoleCommandCallBack callback)
    {
        //支持一个命令调用多条方法，但是只会输出1条命令
        if (HasCommand(command))
        {
            repository[command] += callback;
        }
        else
        {
            repository[command] = new ConsoleCommandCallBack(callback);
        }
    }
    //检查有没有
    public bool HasCommand(string command)
    {
        return repository.ContainsKey(command);
    }
    //查找出以str开头的命令，返回list
    public List<string> SearchCommands(string str)
    {
        string[] keys = new string[repository.Count];
        //拷贝key
        repository.Keys.CopyTo(keys, 0);
        List<string> output = new List<string>();
        foreach (string key in keys)
        {
            if (key.StartsWith(str))
                output.Add(key);
        }
        return output;
    }
    public string[] GetAllKey()
    {
        string[] keys = new string[repository.Count];
        repository.Keys.CopyTo(keys, 0);
        return keys;
    }
    //执行命令
    public string ExecuteCommand(string command, string[] args)
    {
        if (HasCommand(command))
        {
            return repository[command](args);
        }
        else
        {
            return "Command not found";
        }
    }
}
