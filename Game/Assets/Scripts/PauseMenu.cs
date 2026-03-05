using System.Collections;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    bool paused;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !Terminal.Instance.isActive)
        {
            StopAllCoroutines();
            if (paused)
            {
                StartCoroutine(Unpause());
            }
            else
            {
                StartCoroutine(Pause());
            }
        }
    }
    public void setPaused(bool value)
    {
        if (value)
        {
            StartCoroutine(Pause());
        }
        else
        {
            StartCoroutine(Unpause());
        }
    }
    IEnumerator Pause()
    {
        paused = true;
        while(canvasGroup.alpha < 1)
        {
            canvasGroup.alpha += Time.unscaledDeltaTime*2;
            Time.timeScale = 1 - canvasGroup.alpha; 
            yield return null;
        }
        Time.timeScale = 0;
    }
    IEnumerator Unpause()
    {
        paused = false;
        while(canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= Time.unscaledDeltaTime*2;
            Time.timeScale = 1 - canvasGroup.alpha; 
            yield return null;
        }
        Time.timeScale = 1;
    }
    public void MainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}
