using UnityEngine;
using UnityEngine.UI;

public class NoiseIndicator : MonoBehaviour
{
    public MicrophoneInput microphoneInput;
    public Slider noiseSlider; // ��� ������ UI �������

    void Update()
    {
        float noiseLevel = microphoneInput.GetMicrophoneLevel();
        noiseSlider.value = noiseLevel; // �������� �������� ��������
    }
}
