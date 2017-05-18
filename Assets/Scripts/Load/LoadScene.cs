using Assets.Scripts.Utils;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace Assets.Scripts.Load
{
    public class LoadScene : MonoBehaviour
    {
        // Use this for initialization
        private AsyncOperation asyncOp;
        [SerializeField]
        private Slider slider;
        [SerializeField]
        private Text progressText;
        [SerializeField]
        private string sceneName;
        [SerializeField]
        private Text poetryText;
        [SerializeField]
        private TextAsset textAsset;
        void Start()
        {
            if (GameConfig.SceneName != string.Empty)
            {
                sceneName = GameConfig.SceneName;
            }
            
            Debug.Log(sceneName);
            StartCoroutine(StartLoad());           
        }

        private IEnumerator StartLoad()
        {
            yield return new WaitForEndOfFrame();
            asyncOp = SceneManager.LoadSceneAsync(sceneName);
            asyncOp.allowSceneActivation = false;
            int totalPar = 0;
            int currentPar = 0;
            while (asyncOp.progress < 0.9f)
            {
                totalPar = (int)(asyncOp.progress * 100);
                while (currentPar < totalPar)
                {
                    currentPar++;
                    SetProgress(currentPar);
                    yield return new WaitForEndOfFrame();
                }              
            }
            totalPar = 100;
            while (currentPar < totalPar)
            {
                currentPar++;
                SetProgress(currentPar);
                yield return new WaitForEndOfFrame();
            }
            asyncOp.allowSceneActivation = true;
        }
    
        private void SetProgress(int pro)
        {
            progressText.text = string.Format("{0}%", pro);
            slider.value = pro;
            if (pro % 40 == 0)
            {
                StartCoroutine(LoadText());
            }
        }
        
        private IEnumerator LoadText()
        {
            string text = textAsset.text;
            string[] texts = text.Split('-');
            yield return new WaitForSeconds(0.3f);
            int index = UnityEngine.Random.Range(0, texts.Length);
            poetryText.text = texts[index];
        }
    }
}





