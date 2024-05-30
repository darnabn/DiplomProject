using UnityEngine;

public class TeleportController : MonoBehaviour
{
    public Transform teleportPoint; // Точка, куда будет телепортироваться объект
    public KeyCode teleportKey = KeyCode.E; // Клавиша для телепортации

    private bool isPlayerNearby = false; // Флаг для проверки нахождения объекта рядом
    private Transform playerTransform; // Ссылка на трансформ объекта

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(teleportKey))
        {
            TeleportPlayer(); // Телепортация объекта
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null) // Проверка на наличие компонента PlayerController
        {
            isPlayerNearby = true; // Объект рядом с точкой телепортации
            playerTransform = other.transform; // Сохранение ссылки на трансформ объекта
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null) // Проверка на наличие компонента PlayerController
        {
            isPlayerNearby = false; // Объект ушел от точки телепортации
            playerTransform = null; // Очистка ссылки на трансформ объекта
        }
    }

    void TeleportPlayer()
    {
        if (playerTransform != null)
        {
            playerTransform.position = teleportPoint.position; // Перемещение объекта в точку телепортации
        }
    }
}
