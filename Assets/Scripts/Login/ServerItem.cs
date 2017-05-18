using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Utils;
namespace Assets.Scripts.Login
{
    public class ServerItem : MonoBehaviour
    {

        // Use this for initialization
        [SerializeField]
        private GameObject prefabs;
        void Start()
        {
            LoadCsv csv = new LoadCsv();
            string[] res = csv.StartLoadCsv("serverName.csv");
            int len = res.Length;
            for (uint i = 1; i < len; i++)
            {
                CreateItem(res[i], i);
            }
        }

        private void CreateItem(string data, uint index)
        {
            string[] datas = data.Split(new char[] { ',' });
            GameObject obj = Instantiate<GameObject>(prefabs);
            obj.SetActive(true);
            obj.transform.SetParent(transform, false);
            Text name = obj.GetComponentInChildren<Text>();
            name.text = datas[0];

        }
    }
}