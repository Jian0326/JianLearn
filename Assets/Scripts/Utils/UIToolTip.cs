using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Assets.Scripts.Utils
{
    public class UIToolTip : MonoBehaviour
    {

        // Use this for initialization
        [SerializeField]
        private GameObject prefabsObj;
        [SerializeField]
        private Canvas mCanvas;
        void Start()
        {
            if (null == mCanvas)
            {
                mCanvas = GameObject.Find("Canvas").GetComponent<Canvas>();
            }
            if (null == prefabsObj)
            {
                prefabsObj = Resources.Load("Prefabs/TipsText") as GameObject;
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
        private Text GetText()
        {
            GameObject obj = Instantiate(prefabsObj);
            obj.SetActive(true);
            obj.GetComponent<UIOrder>().SetOrder();
            return obj.GetComponent<Text>();
        }
        public void ShowTip(string text)
        {
            var tip = GetText();
            tip.text = text;
            tip.transform.SetParent(mCanvas.transform, false);
            SetTipTransform(tip);
        }
        public void ShowTip(string text, Color color)
        {
            var tip = GetText();
            tip.text = text;
            tip.color = color;
            tip.transform.SetParent(mCanvas.transform, false);
            SetTipTransform(tip);
        }
        public void ShowTip(string text, Color color, Font font, int fontSize)
        {
            var tip = GetText();
            tip.text = text;
            tip.color = color;
            tip.font = font;
            tip.fontSize = fontSize;
            SetTipTransform(tip);
        }

        public void ShowTip(string text, Color color, int fontSize)
        {
            var tip = GetText();
            tip.text = text;
            tip.color = color;
            tip.fontSize = fontSize;
            SetTipTransform(tip);
        }

        private void SetTipTransform(Text text)
        {
            text.transform.SetParent(mCanvas.transform, false);
        }
    }
}