using UnityEngine;
using System.Collections;

public class ObjectDisplayer : MonoBehaviour
{
    public GameObject gameObjectToShow; // ���������� ��� ������� ������ ���� ����� �������� Unity

    void Start()
    {
        gameObjectToShow.SetActive(true); // �������� ����������� ������� ��� ������ ����
        StartCoroutine(HideObjectAfterTime(10)); // ����� ��������, ������� �������� ������ ����� 20 ������
    }

    IEnumerator HideObjectAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay); // ������� ��������� ���������� ������
        gameObjectToShow.SetActive(false); // ��������� ������
    }
}
