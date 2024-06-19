using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject settingsPanel;

    [SerializeField] private GameObject LoadingScrren;
    [SerializeField] private Slider loaddingBarFill;
    [SerializeField] private GameObject uICamera;
    [SerializeField] private GameObject MainCamera;
    [SerializeField] private GameObject InGameUI;
    [SerializeField] private TMP_Text sceneName;

    private void Start()
    {
        settingsPanel.SetActive(false);
    }
    public void StartGame()
    {
        // Burada başlangıç sahnesinin adını veya indeksini belirtmelisin
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        // Oyun durdurulur ve uygulamadan çıkılır
        Application.Quit();
        Debug.Log("Game is exiting"); // Editörde çalışırken çıkış olmadığını belirten mesaj
    }

    public void OpenSettings()
    {
        // Ayarlar panelini açmak için gereken kod buraya eklenir
        // Örneğin, bir ayarlar panelini etkin hale getirebilirsin.
        settingsPanel.SetActive(true);
        Debug.Log("Settings opened"); // Şu anda sadece bir debug mesajı gösteriyoruz
    }
    public void LoadScene(int sceneId)
    {
        Time.timeScale = 1f;
        ActivePanel();
        if (sceneId == 0)
            sceneName.text = "Loading...'You have left the safety of the menu and entered the dense forest. Your journey is just beginning. Ahead lies a path fraught with danger and intrigue. Gather your courage, for the real adventure starts now. Beware of the lurking shadows and keep your wits about you.";
        if (sceneId == 1)
            sceneName.text = "Loading...'You have successfully navigated through the dense forest, but your journey is far from over. Ahead lies the mysterious Cave of Dark Secrets, where many have entered but few have returned. Gather your courage, for the true challenge begins now. Beware of the lurking shadows and keep your wits about you.";
        if (sceneId == 2)
            sceneName.text = "Loading...You have successfully navigated through the treacherous cave, but your journey is far from over. Ahead lies the mysterious Lost Space Station, where many have entered but few have returned. Gather your courage, for the true challenge begins now. Beware of the lurking shadows and keep your wits about you.";
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
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.01f);
            loaddingBarFill.value = Mathf.Lerp(loaddingBarFill.value, progressValue, Time.deltaTime * 5f); // Adjust the multiplier for the speed

            yield return null;
        }

    }
}
