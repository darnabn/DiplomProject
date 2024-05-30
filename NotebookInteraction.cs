using UnityEngine;
using UnityEngine.UI;

public class NotebookInteraction : MonoBehaviour
{
    public RawImage notebookRawImage; // ������ �� ������� RawImage � Canvas

    private bool nearNotebook = false; // ����, �����������, ��������� �� ������ ����� � ���������
    private bool isImageShown = false; // ����, �����������, ������������ �� ����������� ��������
    private Vector2 originalSize; // �������� ������ �����������

    private void Start()
    {
        // �������� ����������� �������� ��� ������
        notebookRawImage.gameObject.SetActive(false);

        // ��������� �������� ������ �����������
        if (notebookRawImage != null)
        {
            originalSize = notebookRawImage.rectTransform.sizeDelta;
        }
        else
        {
            Debug.LogWarning("������ �� RawImage �� �����������.");
        }
    }

    private void Update()
    {
        if (nearNotebook && Input.GetKeyDown(KeyCode.E))
        {
            if (isImageShown)
            {
                HideNotebookImage(); // �������� ����������� �������� ��� ��������� ������� ������ "E"
            }
            else
            {
                ShowNotebookImage(); // ���������� ����������� �������� ��� ������� ������ "E"
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        nearNotebook = true; // ������ ��������� ����� � ���������
    }

    private void OnTriggerExit(Collider other)
    {
        nearNotebook = false; // ������ ������� ���� ��������
        if (isImageShown)
        {
            HideNotebookImage(); // �������� ����������� ��������, ���� ������ ������ �� ����
        }
    }

    private void ShowNotebookImage()
    {
        // ���������, ���� �� ������ �� RawImage
        if (notebookRawImage != null)
        {
            notebookRawImage.gameObject.SetActive(true); // ������ RawImage ��������

            // ����������� ����������� � 3 ����
            notebookRawImage.rectTransform.sizeDelta = originalSize * 3;

            isImageShown = true; // ������������� ���� ����������� �����������
        }
        else
        {
            Debug.LogWarning("������ �� RawImage �� �����������.");
        }
    }

    private void HideNotebookImage()
    {
        // ���������, ���� �� ������ �� RawImage
        if (notebookRawImage != null)
        {
            notebookRawImage.gameObject.SetActive(false); // ������ RawImage ����������

            // ���������� �������� ������ �����������
            notebookRawImage.rectTransform.sizeDelta = originalSize;

            isImageShown = false; // ���������� ���� ����������� �����������
        }
        else
        {
            Debug.LogWarning("������ �� RawImage �� �����������.");
        }
    }
}
