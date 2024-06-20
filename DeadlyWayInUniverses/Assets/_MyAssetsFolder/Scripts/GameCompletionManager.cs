using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections; // TextMeshPro kullanımı için

public class GameCompletionManager : MonoBehaviour
{
    [SerializeField] private GameObject completionPanel; // Bitiş ekranı paneli
    [SerializeField] private GameObject MainCam;
    [SerializeField] private GameObject UICam;
    [SerializeField] private TMP_Text completionText; // Bitiş mesajı metni
    [SerializeField] private float textDisplayInterval = 2.0f; // Her bir metin bloğunun gösterim süresi
    private string[] storyTexts =
    {
        "Congratulations! You Have Successfully Completed the Game!",
        "As you escape the eerie forest, the secrets of the cave remain with you.",
        "Your journey was perilous, but your courage and determination saw you through.",
        "Thank you for playing!",
        "Made by Yakup EKINCI",
        "Deadly Ways Across Universes..."
    };

    private int currentTextIndex = 0;

    private void Start()
    {
        completionPanel.SetActive(false); // Başlangıçta kapalı tut
    }

    // Oyunun tamamlandığı bir metod
    public void CompleteGame()
    {
        MainCam.SetActive(false);
        UICam.SetActive(true);
        completionPanel.SetActive(true); // Bitiş ekranını göster
        Time.timeScale = 0f; // Oyun zamanını durdur
        StartCoroutine(DisplayStory());
    }

    // Ana menüye dönme veya sahneyi yeniden yükleme butonu için bir metod
    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f; // Oyun zamanını tekrar başlat
        SceneManager.LoadScene("MainMenu"); // Ana menü sahnesini yükle
    }

    private IEnumerator DisplayStory()
    {
        while (currentTextIndex < storyTexts.Length)
        {
            completionText.text = storyTexts[currentTextIndex];
            currentTextIndex++;
            yield return new WaitForSecondsRealtime(textDisplayInterval); // Oyun zamanını durdurduğumuz için Realtime kullanıyoruz
        }
        ReturnToMainMenu();


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CompleteGame();
        }
    }
}
