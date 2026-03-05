using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField] Material CRTShader;
    [SerializeField] Slider MasterVolumeSlider;
    [SerializeField] Slider MusicVolumeSlider;
    [SerializeField] Slider SFXVolumeSlider;
    [SerializeField] Toggle CRTEnabledToggle;
    [SerializeField] Slider BloomIntensitySlider;
    [SerializeField] Slider DistortionIntensitySlider;
    [SerializeField] TextMeshProUGUI MasterVolumeTxt;
    [SerializeField] TextMeshProUGUI MusicVolumeTxt;
    [SerializeField] TextMeshProUGUI SFXVolumeTxt;
    [SerializeField] TextMeshProUGUI BloomIntensityTxt;
    [SerializeField] TextMeshProUGUI DistortionIntensityTxt;
    public float MasterVolume {get => SoundPlayer.Instance.masterVolume; set => SoundPlayer.Instance.SetMasterVolume(value);}
    public float MusicVolume {get => SoundPlayer.Instance.musicVolume; set => SoundPlayer.Instance.SetMusicVolume(value);}
    public float SFXVolume {get => SoundPlayer.Instance.SFXVolume; set => SoundPlayer.Instance.SetSFXVolume(value);}
    public bool CRTEnabled {get => CRTShader.GetFloat("_Enabled") == 1f; set => CRTShader.SetFloat("_Enabled", value? 1f : 0f);}
    public float BloomIntensity {get => PostProcessingVolume.Instance.BloomIntensity; set => PostProcessingVolume.Instance.SetBloomIntensity(value);}
    public float DistortionIntensity {get => PostProcessingVolume.Instance.DistortionIntensity; set => PostProcessingVolume.Instance.SetDistortionIntensity(value);}
    void Awake()
    {
        CRTEnabled = true;
        MasterVolumeSlider.onValueChanged.AddListener(UpdateValues);
        MusicVolumeSlider.onValueChanged.AddListener(UpdateValues);
        SFXVolumeSlider.onValueChanged.AddListener(UpdateValues);
        CRTEnabledToggle.onValueChanged.AddListener(UpdateValues);
        BloomIntensitySlider.onValueChanged.AddListener(UpdateValues);
        DistortionIntensitySlider.onValueChanged.AddListener(UpdateValues);
    }
    void OnEnable()
    {
        StartCoroutine(Enable());
    }
    bool isEnabled;
    IEnumerator Enable()
    {
        yield return null;
        MasterVolumeSlider.value = MasterVolume;
        MusicVolumeSlider.value = MusicVolume;
        SFXVolumeSlider.value = SFXVolume;
        CRTEnabledToggle.isOn = CRTEnabled;
        BloomIntensitySlider.value = BloomIntensity;
        DistortionIntensitySlider.value = DistortionIntensity;
        UpdateTxt();
        yield return null;
        isEnabled = true;
    }
    void OnDisable()
    {
        isEnabled = false;
    }
    public void UpdateValues(float a)
    {
        if(!isEnabled){return;}
        MasterVolume = MasterVolumeSlider.value;
        MusicVolume = MusicVolumeSlider.value;
        SFXVolume = SFXVolumeSlider.value;
        CRTEnabled = CRTEnabledToggle.isOn;
        BloomIntensity = BloomIntensitySlider.value;
        DistortionIntensity = DistortionIntensitySlider.value;
        UpdateTxt();
    }
    public void UpdateValues(bool a)
    {
        UpdateValues(1);
    }
    void UpdateTxt()
    {
        MasterVolumeTxt.text = Mathf.Round(MasterVolumeSlider.value*100).ToString();
        MusicVolumeTxt.text = Mathf.Round(MusicVolumeSlider.value*100).ToString();
        SFXVolumeTxt.text = Mathf.Round(SFXVolumeSlider.value*100).ToString();
        BloomIntensityTxt.text = Mathf.Round(BloomIntensitySlider.value*100).ToString();
        DistortionIntensityTxt.text = Mathf.Round(DistortionIntensitySlider.value*100).ToString();
    }
}
