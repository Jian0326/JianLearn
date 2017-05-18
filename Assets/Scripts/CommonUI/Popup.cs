using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace Assets.Scripts.CommonUI
{
    public class Popup : MonoBehaviour
    {

        // Use this for initialization
        public Color backgroundColor = new Color(0.04f, 0.04f, 0.04f, 0.78f);
        public float destroyTime = 0.5f;

        private GameObject background;

        public void Open()
        {
            AddBackground();
        }

        public void Close()
        {
            var animator = GetComponent<Animator>();
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Open"))
            {
                animator.Play("Close");
            }

            RemoveBackground();
            StartCoroutine(RunPopupDestroy());
        }

        private IEnumerator RunPopupDestroy()
        {
            yield return new WaitForSeconds(destroyTime);
            Debug.LogFormat("RunPopupDestroy background = {0}", background);
            Destroy(background);
            Destroy(gameObject);
        }

        private void AddBackground()
        {
            var bgTex = new Texture2D(1, 1);
            bgTex.SetPixel(0, 0, backgroundColor);
            bgTex.Apply();

            background = new GameObject("PopupBackground");
            var image = background.AddComponent<Image>();
            var rect = new Rect(0, 0, bgTex.width, bgTex.height);
            var sprite = Sprite.Create(bgTex, rect, new Vector2(0.5f, 0.5f), 1);
            image.material.mainTexture = bgTex;
            image.sprite = sprite;
            var newColor = image.color;
            image.color = newColor;
            image.canvasRenderer.SetAlpha(0.0f);
            image.CrossFadeAlpha(1.0f, 0.4f, false);

            var canvas = GameObject.Find("Canvas");
            background.transform.localScale = new Vector3(1, 1, 1);
            background.GetComponent<RectTransform>().sizeDelta = canvas.GetComponent<RectTransform>().sizeDelta;
            background.transform.SetParent(canvas.transform, false);
            background.transform.SetSiblingIndex(transform.GetSiblingIndex());
            Debug.LogFormat("AddBackground background = {0} ", background.name);
        }

        private void RemoveBackground()
        {
            Debug.LogFormat("RemoveBackground background = {0}", background);
            var image = background.GetComponent<Image>();
            if (image != null)
                image.CrossFadeAlpha(0.0f, 0.2f, false);
        }
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}