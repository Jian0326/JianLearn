using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerItem : MonoBehaviour
{

    // Use this for initialization
    [SerializeField]
    private GameObject prefabs;
    void Start()
    {
        WWW www = new WWW(PlatformConfig.WwwURL + "/csv/serverName.csv");
        string text;
        while (!www.isDone)
        {
        }
        text = www.text;
        string[] res = text.Split(new string[] { "\r\n" }, System.StringSplitOptions.RemoveEmptyEntries);
        Debug.Log(res);
        int len = res.Length;
        for (uint i = 1; i < len; i++)
        {
            CreateItem(res[i],i);
        }       
    }

    private void CreateItem(string data,uint index)
    {
        string[] datas = data.Split(new char[] { ',' });
        GameObject obj = Instantiate<GameObject>(prefabs);
        obj.SetActive(true);
        obj.transform.SetParent(transform, false);
        Text name = obj.GetComponentInChildren<Text>();
        name.text = datas[0];
        
    }
    // Update is called once per frame
    void Update()
    {

    }
}
