using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    [Header("Панели")]
    public GameObject pausePanel;
    public GameObject settingsPanel;

    public static bool isPaused = false; // Теперь переменная статическая

    void Start()
    {
        // --- ГЛАВНОЕ ИСПРАВЛЕНИЕ ---
        // При загрузке сцены всегда сбрасываем время и статус паузы
        Time.timeScale = 1f;
        isPaused = false;
        // ---------------------------

        if (pausePanel != null) pausePanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (settingsPanel != null && settingsPanel.activeSelf)
            {
                CloseSettings();
            }
            else
            {
                TogglePause();
            }
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;
        if (pausePanel != null) pausePanel.SetActive(isPaused);

        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void OpenSettings()
    {
        if (pausePanel != null) pausePanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (pausePanel != null) pausePanel.SetActive(true);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // Обязательно возвращаем время
        SceneManager.LoadScene("MainPanel");
    }
}