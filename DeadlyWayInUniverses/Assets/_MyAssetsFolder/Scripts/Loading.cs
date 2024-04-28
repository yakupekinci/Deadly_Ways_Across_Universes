using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [SerializeField] private GameObject LoadingScrren;
    [SerializeField] private Slider loaddingBarFill;
    [SerializeField] private GameObject uICamera;

    public void LoadScene(int sceneId)
    {
        StartCoroutine(LoadSceneAsync(sceneId));
    }
    public void ActivePanel()
    {
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
