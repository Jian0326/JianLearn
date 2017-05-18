using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Assets.Scripts.Utils
{
    //在移动的时候会出现order改变所以需要重新设置更新一下
    public class UIOrder : MonoBehaviour
    {

        // Use this for initialization
        public int order;
        public bool isUI = true;
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetOrder()
        {
            if (isUI)
            {
                Canvas canvas = GetComponent<Canvas>();
                if (canvas == null)
                {
                    canvas = gameObject.AddComponent<Canvas>();
                }
                canvas.overrideSorting = true;   // 重载排序
                canvas.sortingOrder = order;     //整理顺序
            }
        }
    }
}