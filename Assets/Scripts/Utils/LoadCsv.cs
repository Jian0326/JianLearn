using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    class LoadCsv
    {
        private string csvUrl = PlatformConfig.WwwURL + "/csv/";
        //返回每一行的数据
        public string[] StartLoadCsv(string csv)
        {
            WWW www = new WWW(csvUrl + csv);
            string text;
            while (!www.isDone)
            {
            }
            text = www.text;
            string[] res = text.Split(new string[] { "\r\n" }, System.StringSplitOptions.RemoveEmptyEntries);
            return res;
        }
        //返回每一行的数据，每一行都是一个数组
        public string[][] StartLoadCsvTwo(string csv)
        {
            string[] csvs = StartLoadCsv(csv);
            int len = csvs.Length;
            string[][] res = new string[len][];
            string[] strs;
            for (int i = 0;i<len;i++)
            {
                strs = csvs[i].Split(new string[] { ","}, System.StringSplitOptions.RemoveEmptyEntries);
                res[i] = strs;
            }
            return res;
        }
    }
}
