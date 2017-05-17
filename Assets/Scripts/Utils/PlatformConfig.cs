using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class PlatformConfig
{
    public static string SqlURL
    {
        get
        {
            string url;
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    url = "URI=file:" + Application.persistentDataPath;
                    break;
                case RuntimePlatform.WindowsPlayer:
                    url = "data source=" + Application.dataPath;
                    break;
                case RuntimePlatform.IPhonePlayer:
                    url = "data source=" + Application.persistentDataPath;
                    break;
                case RuntimePlatform.OSXPlayer:
                    url = "data source=" + Application.dataPath;
                    break;
                default:
                    url = "data source=" + Application.dataPath;
                    break;
            }
            return url;
        }
    }
    public static string WwwURL
    {
        get
        {
            string url;
            switch (Application.platform)
            {
                case RuntimePlatform.Android:
                    url = Application.streamingAssetsPath;
                    break;
                case RuntimePlatform.WindowsPlayer:
                    url = "file:" + Application.streamingAssetsPath;
                    break;
                case RuntimePlatform.IPhonePlayer:
                    url = @"file:/" + Application.streamingAssetsPath;
                    break;
                default:
                    url = @"file:/" + Application.streamingAssetsPath;
                    break;
            }
            return url;
        }
    }
}

