using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject settingsPanel;
    [SerializeField] private GameObject LoadingScrren;
    [SerializeField] private Slider loaddingBarFill;
    [SerializeField] private GameObject uICamera;
    [SerializeField] private GameObject MainCamera;
    [SerializeField] private GameObject InGameUI;
    [SerializeField] private TMP_Text sceneName;

    [SerializeField] private Slider musicVolumeSlider;
 

    private void Start()
    {
        musicVolumeSlider.value = AudioManager.instance.musicSource.volume;


        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);

        settingsPanel.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None; // Kursörü serbest bırak
        Cursor.visible = true; // Kursörü görünür yap

    }

    public void SetMusicVolume(float volume)
    {
        AudioManager.instance.SetMusicVolume(volume);
    }






    public void StartGame()
    {
        // Burada başlangıç sahnesinin adını veya indeksini belirtmelisin
        SceneManager.LoadScene(1);
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





}
