using UnityEngine;
using UnityEngine.UI;

public class NotebookInteraction : MonoBehaviour
{
    public RawImage notebookRawImage; // Ссылка на элемент RawImage в Canvas

    private bool nearNotebook = false; // Флаг, указывающий, находится ли объект рядом с блокнотом
    private bool isImageShown = false; // Флаг, указывающий, отображается ли изображение блокнота
    private Vector2 originalSize; // Исходный размер изображения

    private void Start()
    {
        // Скрываем изображение блокнота при старте
        notebookRawImage.gameObject.SetActive(false);

        // Сохраняем исходный размер изображения
        if (notebookRawImage != null)
        {
            originalSize = notebookRawImage.rectTransform.sizeDelta;
        }
        else
        {
            Debug.LogWarning("Ссылка на RawImage не установлена.");
        }
    }

    private void Update()
    {
        if (nearNotebook && Input.GetKeyDown(KeyCode.E))
        {
            if (isImageShown)
            {
                HideNotebookImage(); // Скрываем изображение блокнота при повторном нажатии кнопки "E"
            }
            else
            {
                ShowNotebookImage(); // Отображаем изображение блокнота при нажатии кнопки "E"
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        nearNotebook = true; // Объект находится рядом с блокнотом
    }

    private void OnTriggerExit(Collider other)
    {
        nearNotebook = false; // Объект покинул зону блокнота
        if (isImageShown)
        {
            HideNotebookImage(); // Скрываем изображение блокнота, если объект уходит от него
        }
    }

    private void ShowNotebookImage()
    {
        // Проверяем, есть ли ссылка на RawImage
        if (notebookRawImage != null)
        {
            notebookRawImage.gameObject.SetActive(true); // Делаем RawImage активным

            // Увеличиваем изображение в 3 раза
            notebookRawImage.rectTransform.sizeDelta = originalSize * 3;

            isImageShown = true; // Устанавливаем флаг отображения изображения
        }
        else
        {
            Debug.LogWarning("Ссылка на RawImage не установлена.");
        }
    }

    private void HideNotebookImage()
    {
        // Проверяем, есть ли ссылка на RawImage
        if (notebookRawImage != null)
        {
            notebookRawImage.gameObject.SetActive(false); // Делаем RawImage неактивным

            // Возвращаем исходный размер изображения
            notebookRawImage.rectTransform.sizeDelta = originalSize;

            isImageShown = false; // Сбрасываем флаг отображения изображения
        }
        else
        {
            Debug.LogWarning("Ссылка на RawImage не установлена.");
        }
    }
}
