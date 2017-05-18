using UnityEngine;
namespace Assets.Scripts.CommonUI
{
    public class PopupOpener : MonoBehaviour
    {

        // Use this for initialization
        public GameObject popupPrefab;

        protected Canvas m_canvas;

        protected void Start()
        {
            m_canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        }

        public virtual void OpenPopup()
        {
            if (popupPrefab == null)
            {
                Debug.LogError("popupPrefab is null object");
                return;
            }
            var popup = Instantiate(popupPrefab) as GameObject;
            popup.SetActive(true);
            popup.transform.localScale = Vector3.zero;
            popup.transform.SetParent(m_canvas.transform, false);
            popup.GetComponent<Popup>().Open();
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}