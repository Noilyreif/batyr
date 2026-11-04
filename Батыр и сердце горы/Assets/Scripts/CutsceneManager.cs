using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CutsceneManager : MonoBehaviour
{
    [Header("Элементы кат-сцены")]
    public GameObject cutscenePanel; // Панель с текстом
    public GameObject player;        // Сам игрок
    public float textDuration = 5f;  // Длительность кат-сцены

    [Header("Что скрыть (если нужно)")]
    public GameObject backgroundMusic;

    void Start()
    {
        // Если катсцена уже была - сразу играем
        if (PlayerPrefs.GetInt("CutsceneShown", 0) == 1)
        {
            if (cutscenePanel != null) cutscenePanel.SetActive(false);
            if (player != null) player.SetActive(true);
            if (backgroundMusic != null) backgroundMusic.SetActive(true);
        }
        else
        {
            // Запускаем катсцену
            StartCoroutine(PlayCutscene());
        }
    }

    IEnumerator PlayCutscene()
    {
        // 1. Включаем панель с текстом
        if (cutscenePanel != null) cutscenePanel.SetActive(true);

        // 2. ВЫКЛЮЧАЕМ игрока (он не будет двигаться и исчезнет с экрана)
        if (player != null) player.SetActive(false);

        // 3. (Опционально) Выключаем музыку
        if (backgroundMusic != null) backgroundMusic.SetActive(false);

        // 4. Ждем нужное время
        yield return new WaitForSeconds(textDuration);

        // 5. Запоминаем, что катсцена была
        PlayerPrefs.SetInt("CutsceneShown", 1);
        PlayerPrefs.Save();

        // 6. Выключаем текст
        if (cutscenePanel != null) cutscenePanel.SetActive(false);

        // 7. ВКЛЮЧАЕМ игрока обратно
        if (player != null) player.SetActive(true);

        // 8. Включаем музыку обратно
        if (backgroundMusic != null) backgroundMusic.SetActive(true);
    }
}