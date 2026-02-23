using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class temporarylevelselect : MonoBehaviour
{
    [SerializeField] TMP_Dropdown dropdown;

    void Start()
    {
        dropdown.options = new List<TMP_Dropdown.OptionData>()
        {
            new TMP_Dropdown.OptionData("Select")
        };
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            dropdown.options.Add(new TMP_Dropdown.OptionData(i.ToString()));
        }
    }
    public void onValueChanged()
    {
        SceneManager.LoadScene(dropdown.value-1);
    }
}
