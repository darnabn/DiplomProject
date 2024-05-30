using UnityEngine;

public class PickUpAll : MonoBehaviour
{
    private bool isPlayerInRange = false;
    public Transform handPosition; // Публичное поле для задания позиции руки
    public Transform cameraTransform; // Публичное поле для задания позиции камеры
    private bool isPickedUp = false; // Флаг для проверки, поднят ли ключ

    private Vector3 originalPosition; // Исходная позиция объекта
    private Quaternion originalRotation; // Исходный поворот объекта

    void Start()
    {
        // Сохранение исходных позиции и поворота объекта
        originalPosition = transform.position;
        originalRotation = transform.rotation;
    }

    void Update()
    {
        // Проверка нажатия кнопки "E" и находится ли игрок в зоне триггера
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E))
        {
            
                PickUp();
            
            
        }
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.R))
        {
            
                Drop();
            
        }

        // Если ключ поднят, следовать за позицией руки
        if (isPickedUp)
        {
            FollowHand();
        }
    }

    // Метод для поднятия ключа
    private void PickUp()
    {
        // Логика поднятия ключа
        Debug.Log("Ключ поднят!");

        // Перемещение ключа в позицию руки
        transform.position = handPosition.position;
        transform.rotation = handPosition.rotation;
        transform.parent = handPosition; // Установить объект ключа дочерним для руки

        isPickedUp = true; // Установить флаг поднятия ключа
    }

    // Метод для следования за рукой
    private void FollowHand()
    {
        transform.position = handPosition.position;
        transform.rotation = handPosition.rotation;
    }

    // Метод для сброса ключа
    private void Drop()
    {
        // Логика сброса ключа
        Debug.Log("Ключ сброшен!");

        // Отключение дочернего объекта для ключа
        transform.parent = null;

        // Установить позицию ключа под игроком (телепорт)
        Transform playerTransform = handPosition.root; // Предполагается, что handPosition является дочерним объектом игрока
        transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y - 1.0f, playerTransform.position.z); // Установить ключ под игроком

        // Восстановление исходной ориентации объекта
        transform.rotation = originalRotation;

        isPickedUp = false; // Сбросить флаг поднятия ключа
    }

    // Событие, когда объект входит в триггер
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            Debug.Log("Игрок в зоне триггера ключа");
        }
    }

    // Событие, когда объект выходит из триггера
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            Debug.Log("Игрок вышел из зоны триггера ключа");
        }
    }
}
