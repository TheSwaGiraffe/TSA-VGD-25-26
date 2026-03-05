using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PostProcessingVolume : MonoBehaviour
{
    public static PostProcessingVolume Instance;
    public float BloomIntensity;
    public float DistortionIntensity;
    public Volume volume;
    Bloom bloom;
    LensDistortion distortion;
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        Instance = this;
        Bloom tempB;
        LensDistortion tempD;
        if(volume.profile.TryGet(out tempB))
        {
            bloom = tempB;
        }
        if(volume.profile.TryGet(out tempD))
        {
            distortion = tempD;
        }
        BloomIntensity = bloom.intensity.value;
        DistortionIntensity = distortion.intensity.value;
    }
    public void SetBloomIntensity(float value)
    {
        BloomIntensity = value;
        bloom.intensity.value = value;
    }
    public void SetDistortionIntensity(float value)
    {
        DistortionIntensity = value;
        distortion.intensity.value = value;
    }
}
