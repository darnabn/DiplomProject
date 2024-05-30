using UnityEngine;

public class MicrophoneInput : MonoBehaviour
{
    private AudioClip microphoneClip;
    private const int sampleWindow = 128;
    private int microphoneSampleRate;
    public float sensitivity = 100f; // ����������� ��������

    void Start()
    {
        // ������ ������ � ���������
        microphoneClip = Microphone.Start(null, true, 10, 44100);
        microphoneSampleRate = AudioSettings.outputSampleRate;
    }

    public float GetMicrophoneLevel()
    {
        // �������� ������� ��� ������ ������������
        float[] samples = new float[sampleWindow];
        int startPosition = Microphone.GetPosition(null) - sampleWindow + 1;
        if (startPosition < 0)
            return 0;

        microphoneClip.GetData(samples, startPosition);

        // ���������� ������� ������� �����
        float sum = 0;
        for (int i = 0; i < sampleWindow; i++)
        {
            sum += Mathf.Abs(samples[i]);
        }
        // �������� ������ �����
        float level = (sum / sampleWindow) * sensitivity;
        return level;
    }
}
