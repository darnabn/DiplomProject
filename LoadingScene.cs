using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    public int sceneId;


    public Slider loadSlider;

    private void Start()
    {
        StartCoroutine(LoadAsync());
    }


    IEnumerator LoadAsync()
    {
        AsyncOperation loadAsync = SceneManager.LoadSceneAsync(sceneId);

        while (!loadAsync.isDone)
        {
            float progress = loadAsync.progress / 0.9f;
            loadSlider.value = progress;
            yield return null;
        }
    }
}
