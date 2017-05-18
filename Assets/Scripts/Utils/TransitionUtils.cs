using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace Assets.Scripts.Utils
{
    public static class TransitionUtils
    {
        public static IEnumerator FadeTo(CanvasGroup group, float alpha, float duration)
        {
            var time = 0.0f;
            var originalAlpha = group.alpha;
            while (time < duration)
            {
                time += Time.deltaTime;
                group.alpha = Mathf.Lerp(originalAlpha, alpha, time / duration);
                yield return new WaitForEndOfFrame();
            }
            group.alpha = alpha;
        }        
    }
}

