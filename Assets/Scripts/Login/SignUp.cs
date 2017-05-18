using Assets.Scripts.Utils;
using Mono.Data.Sqlite;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Assets.Scripts.Login
{
    public class SignUp : MonoBehaviour
    {
<<<<<<< HEAD
        // Use this for initialization
        private DbAccess db;
        [SerializeField]
        private InputField nameText;
        [SerializeField]
        private InputField emailText;
        [SerializeField]
        private InputField passworldText;
=======
        db = new DbAccess(GameConfig.SignUpSQLName);
        db.CreateTable(DB_TABLE_NAME, new string[] { "name", "email", "password" }, new string[] { "text", "text", "text" });
        toolTip = gameObject.GetComponent<UIToolTip>();
    }
>>>>>>> bb313cad1548109f2c8f9ae50416a8901c7b46c8

        private UIToolTip toolTip;
        private const string DB_TABLE_NAME = "signUp";
        void Start()
        {
            db = new DbAccess(GameConfig.SignUpSQLName);
            db.CreateTable(DB_TABLE_NAME, new string[] { "name", "email", "password" }, new string[] { "text", "text", "text" });
            toolTip = gameObject.GetComponent<UIToolTip>();
        }

        public void OnSignUp()
        {
            if (string.Empty == nameText.text || string.Empty == emailText.text
                || string.Empty == passworldText.text)
            {
                toolTip.ShowTip("账户名密码email其中一项为空！", Color.red, 24);
                return;
            }
            SqliteDataReader sqReader = null;//db.ReadFullTable(DB_TABLE_NAME);
            sqReader = db.SelectWhere(DB_TABLE_NAME, new string[] { "name" }, new string[] { "name" }, new string[] { "=" }, new string[] { nameText.text });
            while (sqReader.Read())
            {
                string name = sqReader.GetString(sqReader.GetOrdinal("name"));

                if (name == nameText.text)
                {
                    toolTip.ShowTip("用户名已经被注册了请重新输入！", Color.red, 24);
                    return;
                }
            }
            sqReader = db.SelectWhere(DB_TABLE_NAME, new string[] { "email" }, new string[] { "email" }, new string[] { "=" }, new string[] { emailText.text });
            while (sqReader.Read())
            {
                string email = sqReader.GetString(sqReader.GetOrdinal("email"));

                if (email == emailText.text)
                {
                    toolTip.ShowTip("该邮箱已经被注册了请重新输入！", Color.red, 24);
                    return;
                }
            }

            if (!StringUtil.IsEmail(emailText.text))
            {
                toolTip.ShowTip("Email 格式错误！", Color.red, 24);
                return;
            }
            db.InsertInto(DB_TABLE_NAME, new string[] { nameText.text, emailText.text, passworldText.text });
            db.CloseDB();
            gameObject.transform.parent.gameObject.SendMessage("Close");
            List<string> obj = new List<string> { nameText.text, passworldText.text };
            GameObject.Find("LoginButton").SendMessage("InputText", obj);
            toolTip.ShowTip("恭喜注册成功", Color.green, 24);
        }
        public void OnDestroy()
        {
            if (null != db)
            {
                db.CloseDB();
                db = null;
            }
        }
    }
}
