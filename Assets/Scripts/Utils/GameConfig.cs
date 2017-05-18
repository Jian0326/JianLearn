using System.Collections.Generic;

namespace Assets.Scripts.Utils
{
    public class GameConfig
    {
        private static Dictionary<string, string> SCENE_NAMES = new Dictionary<string, string> {
            { "load","LoadScene"},
            { "login","LoginScene"},
            { "main","MainScene"},
        };
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
        }
    }
}

