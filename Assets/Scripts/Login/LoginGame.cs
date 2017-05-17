using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginGame : MonoBehaviour
{

    private const string DB_TABLE_NAME = "signUp";
    // Use this for initialization
    [SerializeField]
    private InputField nameText;
    [SerializeField]
    private InputField passwordText;

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartGame()
    {
        bool isOk = CheckNameAndPassword();
        if (isOk)
        {

        }
    }

    private bool CheckNameAndPassword()
    {
        if (nameText.text == string.Empty)
        {
            ShowTip("用户名为空");
            return false;
        }
        if (passwordText.text == string.Empty)
        {
            ShowTip("密码名为空");
            return false;
        }
        DbAccess db = new DbAccess(GameConfig.signUpSQLName);
        SqliteDataReader sqReader = db.SelectWhere(DB_TABLE_NAME, new string[] { "name" }, new string[] { "name" }, new string[] { "=" }, new string[] { nameText.text });
        bool isSign = false;
        while (sqReader.Read())
        {
            isSign = true;
            break;
        }
        if (!isSign)
        {
            ShowTip("用户名不存在");
            return isSign;
        }
        sqReader = db.SelectWhere(DB_TABLE_NAME, new string[] {  "name","password" }, new string[] { "password" }, new string[] { "=" }, new string[] { passwordText.text });
        isSign = false;
        while (sqReader.Read())
        {
            string password = sqReader.GetString(sqReader.GetOrdinal("password"));
            string name = sqReader.GetString(sqReader.GetOrdinal("name"));
            Debug.Log(password);
            isSign = name == nameText.text;
            if (!isSign)
            {
                break;
            } 
        }
        if (!isSign)
        {
            ShowTip("密码错误。请重新输入");
            return isSign;
        }
        db.CloseDB();
        return isSign;
    }

    private void ShowTip(string text)
    {
        var tip = gameObject.GetComponent<UIToolTip>();
        tip.ShowTip(text, Color.red, 24);
    }

    public void InputText(List<string> obj)
    {
        nameText.text = obj[0];
        passwordText.text = obj[1];
    }
}
