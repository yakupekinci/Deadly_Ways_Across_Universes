using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Teleport : Loading
{
    public int SceneNum;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("DÜŞTÜK");
            ActivePanel();
            other.gameObject.SetActive(false);
            LoadScene(SceneNum);
        }
    }



}
