using UnityEngine;

public class HideInCloset : MonoBehaviour
{
    public Transform hidePosition; // Позиция, куда переместится игрок, когда спрячется
    private bool isHiding = false;
    private Transform player;
    private CharacterController playerController;
    private Vector3 initialPosition; // Начальная позиция игрока перед пряткой

    public Renderer closetRenderer; // Renderer шкафа
    public Material normalMaterial; // Нормальный материал (непрозрачный)
    public Material transparentMaterial; // Прозрачный материал

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerController = player.GetComponent<CharacterController>();

        // Установить нормальный материал по умолчанию
        closetRenderer.material = normalMaterial;
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
            if (!isHiding)
            {
                Hide();
            }
            else
            {
                Unhide();
            }
        }
    }

    void Hide()
    {
        Debug.Log("Player is hiding");
        // Сохранить начальную позицию игрока
        initialPosition = player.position;

        // Отключить управление игроком
        playerController.enabled = false;
        player.position = hidePosition.position;
        isHiding = true;

        // Сделать шкаф прозрачным
        closetRenderer.material = transparentMaterial;
    }

    void Unhide()
    {
        Debug.Log("Player is unhiding");
        // Включить управление игроком
        playerController.enabled = true;
        // Вернуть игрока на сохранённую позицию
        player.position = initialPosition;
        isHiding = false;

        // Сделать шкаф снова нормальным
        closetRenderer.material = normalMaterial;
    }
}
