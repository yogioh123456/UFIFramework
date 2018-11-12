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


public class TestCS : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //读取excel
        GameReadExcel("/Test/Excel/testExcel.xlsx");
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
}
