using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpiderController : MonoBehaviour
{
    public enum SceneToLoad
    {
        NextScene,
        PreviousScene
    }

    private int currentSpiderScene = 0;
    private const int firstScene = 5;
    private const int lastScene = 5;
    private const string baseSceneString = "Spider - ";

    // Object in front of camera to fade in/out view.
    [SerializeField]
    private Material fadeMaterial;

    public void LoadNewScene(SceneToLoad sceneToLoad)
    {
        int spiderSceneToLoad = currentSpiderScene;

        if (sceneToLoad == SceneToLoad.NextScene)
        {
            spiderSceneToLoad += 1;
        }
        else if (sceneToLoad == SceneToLoad.PreviousScene)
        {
            spiderSceneToLoad -= 1;
        }
        Mathf.Clamp(spiderSceneToLoad, firstScene, lastScene);
        StartCoroutine(StartLoad(spiderSceneToLoad));
    }

    private IEnumerator StartLoad(int spiderSceneToLoad)
    {
        yield return StartCoroutine(FadeLoadingScreen(1, 1.5f));
        AsyncOperation UnloadOperation = SceneManager.UnloadSceneAsync(baseSceneString + currentSpiderScene);
        AsyncOperation LoadOperation = SceneManager.LoadSceneAsync(baseSceneString + spiderSceneToLoad, LoadSceneMode.Additive);
        while (!UnloadOperation.isDone && !LoadOperation.isDone)
        {
            yield return null;
        }
        yield return StartCoroutine(FadeLoadingScreen(0, 1.5f));
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
