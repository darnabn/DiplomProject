using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public CameraController cameraController; // ������ �� ������ CameraController
    private bool isPaused = false;

    void Start()
    {
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked; // ��������� ���������� �������
        Cursor.visible = false; // ��������� ������� �������
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
        Cursor.lockState = CursorLockMode.None; // ������������� �������
        Cursor.visible = true; // ���������� ������
        cameraController.enabled = false; // ��������� ���������� �������
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked; // ��������� ������
        Cursor.visible = false; // �������� ������
        cameraController.enabled = true; // �������� ���������� �������
    }
    public void GoToMain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
