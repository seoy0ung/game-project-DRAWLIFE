using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;

public class CSVWriter : MonoBehaviour
{

    public static void Write(List<List<string>> dataList, string fileName)
    {
        int dataCount = dataList.Count;
        List<List<string>> output = new List<List<string>>();
        for (int i = 0; i < dataCount; i++)
        {
            List<string> lines = new List<string>();
            for (int j = 0; j < dataList[i].Count; j++)
            {
                string strData = dataList[i][j];
                lines.Add(strData);
            }
            output.Add(lines);
        }

        int length = output[0].Count;
        string delimiter = ",";
        StringBuilder sb = new StringBuilder();

        for (int index = 0; index < dataCount; index++)
        {
            
            sb.AppendLine(string.Join(delimiter, output[index]));

        }
        
        string filePath = GetPath();
        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }
        fileName += ".csv";

        StreamWriter outStream = System.IO.File.CreateText(filePath + fileName);
        outStream.WriteLine(sb);
        outStream.Close();
    }

    private static string GetPath()
    {
#if UNITY_EDITOR
        return Application.dataPath + "/Resources/";
#elif UNITY_ANDROID
        return Application.persistentDataPath + "/Resources/";
#elif UNITY_IPHONE
        return Application.persistentDataPath +"/Resources/";
#else
        return Application.dataPath +"/Resources/";
#endif
    }

    //     public static void Save(List<List<string>> rowData)
    //     {

    //         // Creating First row of titles manually..
    //         List<string> rowDataTemp = new List<string>();
    //         //string[] rowDataTemp = new string[3];
    //         // rowDataTemp[0] = "Name";
    //         // rowDataTemp[1] = "ID";
    //         // rowDataTemp[2] = "Income";
    //         // rowData.Add(rowDataTemp);

    //         rowData[0][0] = "Name";
    //         rowData[0][1] = "ID";
    //         rowData[0][2] = "Income";

    //         // You can add up the values in as many cells as you want.
    //         for (int i = 0; i < 10; i++)
    //         {
    //             rowDataTemp = new string[3];
    //             rowDataTemp[0] = "Sushanta" + i; // name
    //             rowDataTemp[1] = "" + i; // ID
    //             rowDataTemp[2] = "$" + UnityEngine.Random.Range(5000, 10000); // Income
    //             rowData.Add(rowDataTemp);
    //         }

    //         string[][] output = new string[rowData.Count][];

    //         for (int i = 0; i < output.Length; i++)
    //         {
    //             output[i] = rowData[i];
    //         }

    //         int length = output.GetLength(0);
    //         string delimiter = ",";

    //         StringBuilder sb = new StringBuilder();

    //         for (int index = 0; index < length; index++)
    //             sb.AppendLine(string.Join(delimiter, output[index]));


    //         string filePath = getPath();

    //         StreamWriter outStream = System.IO.File.CreateText(filePath);
    //         outStream.WriteLine(sb);
    //         outStream.Close();
    //     }

    //     // Following method is used to retrive the relative path as device platform
    //     private string getPath()
    //     {
    // #if UNITY_EDITOR
    //         return Application.dataPath + "/Resources/" + "Saved_data.csv";
    // #elif UNITY_ANDROID
    //         return Application.persistentDataPath+ "/Resources/" +"Saved_data.csv";
    // #else
    //         return Application.dataPath +"/Resources/"+"Saved_data.csv";
    // #endif
    //     }
}