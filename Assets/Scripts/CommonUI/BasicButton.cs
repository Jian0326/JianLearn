using Assets.Scripts.Utils;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
namespace Assets.Scripts.CommonUI
{
    public class BasicButton : MonoBehaviour, IPointerUpHandler, IPointerExitHandler, IPointerEnterHandler, IPointerDownHandler
    {

        public float fadeTime = 0.2f;
        public float onUpAlpha;
        public float onClickAlpha;

        [Serializable]
        public class ButtonClickedEvent : UnityEvent { }

        [SerializeField]
        private ButtonClickedEvent onClicked = new ButtonClickedEvent();

        private CanvasGroup canvasGroup;

        private void Awake()
        {
            canvasGroup = gameObject.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
            {
                return;
            }
            canvasGroup.alpha = onClickAlpha;
            onClicked.Invoke();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
            {
                return;
            }
            StopAllCoroutines();
            StartCoroutine(TransitionUtils.FadeTo(canvasGroup, 0.8f, fadeTime));
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
            {
                return;
            }

            StopAllCoroutines();
            StartCoroutine(TransitionUtils.FadeTo(canvasGroup, 1.0f, fadeTime));
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
            {
                return;
            }
            canvasGroup.alpha = onUpAlpha;
        }

        // Use this for initialization
        void Start()
        {
            //string a = new string(new char[] { 's'});
            //String S = new String('A',10);
            //a = (a.ToUpper() + "123") ;
            //string b = a.Substring(0,2);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}