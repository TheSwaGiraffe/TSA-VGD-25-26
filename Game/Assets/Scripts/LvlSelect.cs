using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LvlSelect : MonoBehaviour
{
    [SerializeField] UnityEngine.UI.Button template;
    [SerializeField] TextMeshProUGUI templateText;
    void Start()
    {
        for(int world = 1; world <= 4; world++)
        {
            for(int stage = 1; stage <= 8; stage++)
            {
                templateText.text = $"{world}-{stage}";
                template.gameObject.SetActive(true);
                int sceneIndex = (world - 1) * 8 + stage;
                GameObject button = Instantiate(template.gameObject, transform);
                button.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() =>
                {
                    SoundPlayer.MusicPlayer.StartPlayingSongs();
                    SceneManager.LoadScene(sceneIndex);
                });
                template.gameObject.SetActive(false);
            }
        }
    }
}
