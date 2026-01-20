using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void loadScene(int buildindex)
    {
        SceneManager.LoadScene(buildindex);
    }
}
