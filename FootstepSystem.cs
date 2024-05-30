// FootstepSystem.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootstepSystem : MonoBehaviour
{
    public AudioClip[] stone;
    public AudioClip[] grass;

    private PlayerController playerController;

    void Start()
    {
        // Получаем ссылку на компонент PlayerController внутри объекта FootstepSystem
        playerController = GetComponentInChildren<PlayerController>();

        if (playerController == null)
        {
            // Если ссылка не найдена, выдаем предупреждение
            Debug.LogWarning("PlayerController component not found in FootstepSystem object.");
        }
    }

    void Update()
    {
        // Проверяем, был ли инициализирован объект playerController
        if (playerController != null)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, -transform.up, out hit, 2))
            {
                if (hit.collider.CompareTag("Grass"))
                {
                    // Передаем массив звуков шагов по траве в PlayerController
                    playerController.SetFootstepSounds(grass);
                }
                else if (hit.collider.CompareTag("Stone"))
                {
                    // Передаем массив звуков шагов по камню в PlayerController
                    playerController.SetFootstepSounds(stone);
                }
            }
        }
    }
}
