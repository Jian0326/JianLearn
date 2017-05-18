using Assets.Scripts.CommonUI;
using UnityEngine;
using UnityEngine.UI;
namespace Assets.Scripts.Login
{
    public class SelectServer : MonoBehaviour
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void OnSelectServer()
        {
            Text tx = gameObject.GetComponentInChildren<Text>();
            //拿到顶级父对象
            Transform parent = transform.parent.transform.parent.transform.parent.transform.parent;
            Popup pp = parent.GetComponent<Popup>();
            pp.Close();
            Debug.Log(tx.text);
        }
    }
}