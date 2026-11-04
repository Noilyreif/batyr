using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [Header("Настройки звука")]
    public AudioMixer mixer;
    public string parameterName = "MasterVol";

    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();

        if (slider != null && mixer != null)
        {
            // Применяем громкость при старте
            SetVolume(slider.value);
            slider.onValueChanged.AddListener(SetVolume);
        }
    }

    public void SetVolume(float value)
    {
        if (mixer == null) return;

        // ХИТРОСТЬ: Мы берем значение ползунка (от 0.0001 до 1) 
        // и переводим его в децибелы через Mathf.Log10.
        // Но чтобы ползунок был плавным, мы не даем ему упасть в абсолютный ноль, 
        // пока он не будет в самом крайнем левом положении.

        float db;
        if (value <= 0.0001f)
        {
            db = -80f; // Полная тишина
        }
        else
        {
            // Формула, которая делает шкалу более плавной (звук начинает падать ближе к концу)
            db = Mathf.Log10(value) * 20f;
        }

        mixer.SetFloat(parameterName, db);
    }
}