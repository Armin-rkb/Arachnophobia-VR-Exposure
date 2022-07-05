using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections;

public class SpiderController : MonoBehaviour
{
    private int currentSpiderScene = 0;
    private const int firstScene = 0;
    private const int lastScene = 5;
    private const string baseSceneString = "Spider - ";
    
    [SerializeField]
    private UnityEvent OnLevelLoadStart;
    [SerializeField]
    private UnityEvent<int> OnLevelLoadEnd;

    // Object in front of camera to fade in/out view.
    [SerializeField]
    private Material fadeMaterial;

    public void LoadNewScene(bool isNextScene)
    {
        int spiderSceneToLoad = currentSpiderScene;

        spiderSceneToLoad += isNextScene ? 1 : -1;
        spiderSceneToLoad = Mathf.Clamp(spiderSceneToLoad, firstScene, lastScene);
        StartCoroutine(StartLoad(spiderSceneToLoad));
    }

    private IEnumerator StartLoad(int spiderSceneToLoad)
    {
        yield return StartCoroutine(FadeLoadingScreen(1, 1.5f));
        OnLevelLoadStart?.Invoke();
        AsyncOperation UnloadOperation = SceneManager.UnloadSceneAsync(baseSceneString + currentSpiderScene);
        AsyncOperation LoadOperation = SceneManager.LoadSceneAsync(baseSceneString + spiderSceneToLoad, LoadSceneMode.Additive);
        while (!UnloadOperation.isDone && !LoadOperation.isDone)
        {
            yield return null;
        }
        currentSpiderScene = spiderSceneToLoad;

        yield return StartCoroutine(FadeLoadingScreen(0, 1.5f));
        OnLevelLoadEnd?.Invoke(currentSpiderScene);
    }

    IEnumerator FadeLoadingScreen(float targetAlpha, float duration)
    {
        Color c = fadeMaterial.color;
        float startAlpha = c.a;
        float time = 0;

        while (time < duration)
        {
            c.a = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            fadeMaterial.color = c;
            time += Time.deltaTime;
            yield return null;
        }
        c.a = targetAlpha;
    }
}
