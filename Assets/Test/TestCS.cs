using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using ExcelDataReader;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System;
using System.Threading.Tasks;
using System.Reflection;


public class TestCS : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //读取excel
        //GameReadExcel("/Test/Excel/testExcel.xlsx");

        //测试异步编程
        //TestAsync();//建立异步任务
        //tcs.SetResult(true);//执行异步任务

        TestFS();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// 读取excel表
    /// </summary>
    /// <param name="path">Path.</param>
    void GameReadExcel(string path){
        FileStream stream = File.Open(Application.dataPath + path, FileMode.Open, FileAccess.Read);
        IExcelDataReader excelDataReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
        DataSet result = excelDataReader.AsDataSet();
        int cols = result.Tables[0].Columns.Count;
        int rows = result.Tables[0].Rows.Count;
        //一行一行地读取
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                string v = result.Tables[0].Rows[i][j].ToString();
                Debug.Log(v);
            }
        }
    }

    async void TestAsync()
    {
        await MyTestTask();
        Debug.Log("xxxx");
    }
    private TaskCompletionSource<bool> tcs;
    Task<bool> MyTestTask()
    {
        tcs = new TaskCompletionSource<bool>();
        Debug.Log("测试task方法");
        return tcs.Task;
    }

    #region 反射
    void TestFS()
    {
        Type type =  Type.GetType("UI_FriendView");
        PropertyInfo[] proInfo = type.GetProperties();
        foreach (var field in proInfo)
        {
            Debug.Log(field);
        }
    }
    #endregion
}
