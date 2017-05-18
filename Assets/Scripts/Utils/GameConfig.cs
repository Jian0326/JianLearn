using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Utils
{
<<<<<<< HEAD
    public class GameConfig
    {
        private static Dictionary<string, string> SCENE_NAMES = new Dictionary<string, string> {
=======
    private static Dictionary<string, string> SCENE_NAMES = new Dictionary<string, string> {
>>>>>>> bb313cad1548109f2c8f9ae50416a8901c7b46c8
        { "load","LoadScene"},
        { "login","LoginScene"},
        { "main","MainScene"},
    };
<<<<<<< HEAD
        private static string sceneName = "";
        public static string SignUpSQLName
        {
            get
            {
                return PlatformConfig.SqlURL + "/signUp.db";
            }
        }
        //需要先设置 SCENE_NAMES 的key
        public static string SceneName
        {
            set
            {
                sceneName = value;
            }
            get
            {
                if (!SCENE_NAMES.ContainsKey(sceneName))
                {
                    return string.Empty;
                }
                return SCENE_NAMES[sceneName];
            }
=======
    private static string sceneName = "";
    public static string SignUpSQLName
    {
        get
        {
            return PlatformConfig.SqlURL + "/signUp.db";
        }
    }
    //需要先设置 SCENE_NAMES 的key
    public static string SceneName
    {
        set
        {
            sceneName = value;
        }
        get
        {
            if (!SCENE_NAMES.ContainsKey(sceneName))
            {
                return string.Empty;
            }
            return SCENE_NAMES[sceneName];
>>>>>>> bb313cad1548109f2c8f9ae50416a8901c7b46c8
        }
    }
}

