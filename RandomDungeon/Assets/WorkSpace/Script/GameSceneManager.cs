using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameSceneManager : MonoBehaviour
{
    [SerializeField] private string titleSceneName = "Title";
    [SerializeField] private string mainSceneName = "Main";
    [SerializeField] private string gameOverSceneName = "GameOver";
    [SerializeField] private string clearSceneName = "Clear";
    [SerializeField] private FadeManager fadeManager = null;

    public void LoadTitleScene()
    {
        StartCoroutine(LoadScene(titleSceneName));
    }

    public void LoadMainScene()
    {
        StartCoroutine(LoadScene(mainSceneName));
    }

    public void LoadGameOverScene()
    {
        StartCoroutine(LoadScene(gameOverSceneName));
    }

    public void LoadClearScene()
    {
        StartCoroutine(LoadScene(clearSceneName));
    }

    private IEnumerator LoadScene(string sceneName)
    {
        //フェードアウト完了まで待つ
        yield return StartCoroutine(fadeManager.FadeOut());
        SceneManager.LoadScene(sceneName);
    }
}