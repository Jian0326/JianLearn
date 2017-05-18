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
        public string[][] StartLoadCsvTwo(string csv)
        {
            string[] csvs = StartLoadCsv(csv);
            int len = csvs.Length;
            string[][] res = new string[len][];
            string[] strs;
            for (int i = 0;i<len;i++)
            {
                strs = csvs[i].Split(new string[] { ","}, System.StringSplitOptions.RemoveEmptyEntries);
            }
            return res;
        }
    }
}
