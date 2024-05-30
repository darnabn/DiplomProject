using UnityEngine;
using UnityEngine.UI;

public class NoiseIndicator : MonoBehaviour
{
    public MicrophoneInput microphoneInput;
    public Slider noiseSlider; // Или другой UI элемент

    void Update()
    {
        float noiseLevel = microphoneInput.GetMicrophoneLevel();
        noiseSlider.value = noiseLevel; // Обновить значение слайдера
    }
}
