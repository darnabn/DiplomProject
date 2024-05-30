using UnityEngine;
using System.Collections;

public class ObjectDisplayer : MonoBehaviour
{
    public GameObject gameObjectToShow; // Перетащите ваш игровой объект сюда через редактор Unity

    void Start()
    {
        gameObjectToShow.SetActive(true); // Включаем отображение объекта при старте игры
        StartCoroutine(HideObjectAfterTime(10)); // Вызов корутины, которая отключит объект через 20 секунд
    }

    IEnumerator HideObjectAfterTime(float delay)
    {
        yield return new WaitForSeconds(delay); // Ожидаем указанное количество секунд
        gameObjectToShow.SetActive(false); // Отключаем объект
    }
}
