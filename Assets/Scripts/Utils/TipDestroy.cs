using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Assets.Scripts.Utils
{
    public class TipDestroy : MonoBehaviour
    {

        // Use this for initialization

        public void Start()
        {
        }

        public void RunDestroy(float value)
        {
            Destroy(gameObject);
        }
    }
}
