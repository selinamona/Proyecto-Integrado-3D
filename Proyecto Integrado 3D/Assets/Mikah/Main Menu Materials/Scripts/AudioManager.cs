using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("Audio")]
    public AudioMixer audioMixer;
    public AudioSource musicSource;

    [Header("UI")]
    public Slider volumeSlider;

    [Header("Configuración")]
    [Range(0f, 1f)]
    public float defaultVolume = 0.75f;
    public string volumeParameter = "MasterVolume";

    void Start()
    {
        // Cargar volumen guardado o usar valor por defecto
        float savedVolume = PlayerPrefs.GetFloat("MusicVolume", defaultVolume);

        // Configurar slider (FORZAR rango 0-1)
        volumeSlider.minValue = 0f;
        volumeSlider.maxValue = 1f;
        volumeSlider.wholeNumbers = false;
        volumeSlider.value = savedVolume;

        // Aplicar volumen
        SetVolume(savedVolume);

        // Suscribir evento
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    public void SetVolume(float volume)
    {
        // Para AudioSource directo
        if (musicSource != null)
            musicSource.volume = volume;

        // Para AudioMixer (mejor)
        if (audioMixer != null)
        {
            float dB = volume <= 0.0001f ? -80f : Mathf.Log10(volume) * 20f;
            audioMixer.SetFloat(volumeParameter, dB);
        }

        // Guardar
        PlayerPrefs.SetFloat("MusicVolume", volume);

        Debug.Log($"Volumen cambiado a: {volume}");
    }
}