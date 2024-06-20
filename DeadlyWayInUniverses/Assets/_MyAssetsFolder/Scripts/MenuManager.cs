using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject DemoPanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject LoadingScrren;
    [SerializeField] private Slider loaddingBarFill;
    [SerializeField] private GameObject uICamera;
    [SerializeField] private GameObject MainCamera;
    [SerializeField] private GameObject InGameUI;
    [SerializeField] private TMP_Text sceneName;

    private void Start()
    {
        settingsPanel.SetActive(false);
        DemoPanel.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None; // Kursörü serbest bırak
        Cursor.visible = true; // Kursörü görünür yap
        AudioManager.instance.PlayMusic("MenuMusic");
    }
    public void StartGame()
    {

        AudioManager.instance.PlayEffect("ButtonHit");
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {

        AudioManager.instance.PlayEffect("Click");
        Application.Quit();
        Debug.Log("Game is exiting");
    }

    public void OpenSettings()
    {

        AudioManager.instance.PlayEffect("Click");
        settingsPanel.SetActive(true);
        DemoPanel.SetActive(false);
    }


    public void OpenDemo()
    {
        AudioManager.instance.PlayEffect("Click");
        DemoPanel.SetActive(true);
        settingsPanel.SetActive(false);

    }
    public void Click()
    {
        AudioManager.instance.PlayEffect("Click");
    }
    public void LoadScene(int sceneId)
    {
        Time.timeScale = 1f;
        ActivePanel();
        switch (sceneId)
        {
            case 1:
                AudioManager.instance.PlayMusic("Forest");
                sceneName.text = "'You have left the safety of the menu and entered the dense forest. Your journey is just beginning. Ahead lies a path fraught with danger and intrigue. Gather your courage, for the real adventure starts now. Beware of the lurking shadows and keep your wits about you.";

                break;
            case 2:
                AudioManager.instance.PlayMusic("Cave");
                sceneName.text = "'You have successfully navigated through the dense forest, but your journey is far from over. Ahead lies the mysterious Cave of Dark Secrets, where many have entered but few have returned. Gather your courage, for the true challenge begins now. Beware of the lurking shadows and keep your wits about you.";

                break;
            case 3: 
                AudioManager.instance.PlayMusic("Space");
                sceneName.text = "You have successfully navigated through the treacherous cave, but your journey is far from over. Ahead lies the mysterious Lost Space Station, where many have entered but few have returned. Gather your courage, for the true challenge begins now. Beware of the lurking shadows and keep your wits about you.";

                break;
        }
        StartCoroutine(LoadSceneAsync(sceneId));
    }
    public void ActivePanel()
    {
        MainCamera.SetActive(false);
        InGameUI.SetActive(false);
        LoadingScrren.gameObject.SetActive(true);
        loaddingBarFill.gameObject.SetActive(true);
        uICamera.SetActive(true);

    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        AudioManager.instance.PlayEffect("ButtonHit");
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.01f);
            loaddingBarFill.value = Mathf.Lerp(loaddingBarFill.value, progressValue, Time.deltaTime * 5f); // Adjust the multiplier for the speed

            yield return null;
        }

    }
    public void ForestMap()
    {
        AudioManager.instance.PlayEffect("ButtonHit");
        SceneManager.LoadScene(1);
    }
    public void CaveMap()
    {
        AudioManager.instance.PlayEffect("ButtonHit");
        SceneManager.LoadScene(2);
    }
    public void SpaceMap()
    {
        AudioManager.instance.PlayEffect("ButtonHit");
        SceneManager.LoadScene(3);
    }


}
