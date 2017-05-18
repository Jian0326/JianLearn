using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
        yield return asyncOp;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    private void SetProgress(int pro)
    {
        progressText.text = string.Format("{0}%", pro);
        slider.value = pro;
    }
}
