using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadCSV : MonoBehaviour
{
    public static int[,] Load(TextAsset text)
    {
        int[,] arrData;
        TextAsset textCSV = text;//Resources.Load("경로.csv") as TextAsset;

        string[] Lines = CSVUtil.CSVToLine(textCSV.text);
        string[] tempArr = Lines[0].Split(',');

        arrData = new int[Lines.Length - 1, tempArr.Length];
        for (int i = 0; i < Lines.Length - 1; i++)
        {
            string[] columns = Lines[i].Split(',');
            for (int j = 0; j < columns.Length; j++)
                arrData[i, j] = int.Parse(columns[j]);
        }

        return arrData;
    }
}

public class CSVUtil
{
    public static string[] CSVToLine(string str)
    {
        string[] lineArr = str.Split('\n');
        return lineArr;
    }

    public static string[] LineToColumn(string line)
    {
        string[] columnArr = line.Split(',');
        return columnArr;
    }
}
