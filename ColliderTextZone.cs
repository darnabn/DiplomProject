using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderTextZone : MonoBehaviour
{
    public GameObject textObject; // Перетащите ваш текстовый объект сюда через редактор Unity

    void Start()
    {
        textObject.SetActive(false); // Убедитесь, что текст изначально скрыт
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Предполагается, что у вашего игрока установлен тег "Player"
        {
            textObject.SetActive(true); // Показываем текст, когда игрок входит в зону коллайдера
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // Предполагается, что у вашего игрока установлен тег "Player"
        {
            textObject.SetActive(false); // Скрываем текст, когда игрок выходит из зоны коллайдера
        }
    }
}
