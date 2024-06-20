using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private GameObject settingsPanelUI;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        AudioManager.instance.PlayEffect("Click");
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked; // Kursörü serbest bırak
        Cursor.visible = false; // Kursörü görünür yap


    }
    public void Clicks()
    {
        AudioManager.instance.PlayEffect("Click");
    }
    public void Pause()
    {
        AudioManager.instance.PlayEffect("Click");
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        Cursor.lockState = CursorLockMode.None; // Kursörü serbest bırak
        Cursor.visible = true; // Kursörü görünür yap
    }

    public void OpenSettings()
    {
        settingsPanelUI.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanelUI.SetActive(false);
    }
    public void MainMenu()
    {
        AudioManager.instance.PlayEffect("Click");
        SceneManager.LoadScene(0);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
