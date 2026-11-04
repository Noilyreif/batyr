using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class CutsceneManager : MonoBehaviour
{
    [Header("Элементы кат-сцены")]
    public GameObject cutscenePanel; // Панель с картинками/текстом
    public GameObject player;        // Игрок
    public float textDuration = 5f;  // Время показа

    void Start()
    {
        // Убрали проверку PlayerPrefs! Теперь катсцена запускается ВСЕГДА.
        StartCoroutine(PlayCutscene());
    }

    IEnumerator PlayCutscene()
    {
        // 1. Показываем панель
        if (cutscenePanel != null) cutscenePanel.SetActive(true);

        // 2. Выключаем игрока (чтобы он не ходил)
        if (player != null) player.SetActive(false);

        // 3. Ждем, пока игрок посмотрит катсцену
        yield return new WaitForSeconds(textDuration);

        // 4. Выключаем катсцену и включаем игрока
        if (cutscenePanel != null) cutscenePanel.SetActive(false);
        if (player != null) player.SetActive(true);
    }
}