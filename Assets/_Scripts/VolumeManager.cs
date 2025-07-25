using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
   
     public Slider volumeSlider;

    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("volume", 1f);
        AudioListener.volume = savedVolume;
        volumeSlider.value = savedVolume;

        volumeSlider.onValueChanged.AddListener(delegate
        {
            OnVolumeChange(volumeSlider.value);
        });
    }
    void OnVolumeChange(float value)
{
    AudioListener.volume = value;
    PlayerPrefs.SetFloat("volume", value);
    PlayerPrefs.Save();
}
}
