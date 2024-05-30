using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public CameraController cameraController; // Ссылка на скрипт CameraController
    private bool isPaused = false;

    void Start()
    {
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked; // Начальная блокировка курсора
        Cursor.visible = false; // Начальное скрытие курсора
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None; // Разблокировка курсора
        Cursor.visible = true; // Показываем курсор
        cameraController.enabled = false; // Отключаем управление камерой
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked; // Блокируем курсор
        Cursor.visible = false; // Скрываем курсор
        cameraController.enabled = true; // Включаем управление камерой
    }
    public void GoToMain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
