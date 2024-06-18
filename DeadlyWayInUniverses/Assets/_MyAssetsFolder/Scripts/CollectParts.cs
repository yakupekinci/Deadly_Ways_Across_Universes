using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTutorial.PlayerControl;

public class CollectParts : MonoBehaviour
{
    [SerializeField] private GameObject actionKey;
    [SerializeField] private GameObject actionText;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private AudioSource collectSound;
    public GameObject ParchamentPanel;
     [SerializeField] private float  _collectDistance;
    private float theDistance;
    private bool isCollect = false;

    private PlayerController playerController; // PlayerController referansı
    private NpcController NPCController;

    private void Start()
    {
        NPCController = FindObjectOfType<NpcController>();
        playerController = FindObjectOfType<PlayerController>(); // PlayerController örneğini bul
    }

    private void Update()
    {
        theDistance = PlayerRay.distanceFromTarget;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isCollect && ParchamentPanel.gameObject.activeSelf)
            {
                ParchamentPanel.SetActive(false);
                playerController.CanMove = true;
            }
        }
    }

    private void OnMouseOver()
    {
        // Mesafe kontrolünü buraya ekliyoruz.
        if (Vector3.Distance(transform.position, Camera.main.transform.position) <= _collectDistance && !isCollect)
        {
            actionKey.SetActive(true);
            actionText.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (gameObject.CompareTag("Parchment"))
                {
                    playerController.CanMove = false;
                    isCollect = true;
                    ParchamentPanel.SetActive(true);
                    NPCController.CollectScroll(ParchamentPanel.gameObject);
                    transform.GetChild(0).gameObject.SetActive(false);
                    transform.GetChild(1).gameObject.SetActive(false);
                    transform.GetChild(2).gameObject.SetActive(false);
                    StartCoroutine(WaitForParchament());
                }
            }
        }
        else
        {
            actionKey.SetActive(false);
            actionText.SetActive(false);
        }
    }

    private void OnMouseExit()
    {
        actionKey.SetActive(false);
        actionText.SetActive(false);
    }

    IEnumerator WaitForParchament()
    {
        yield return new WaitForSeconds(30);
        transform.gameObject.SetActive(false);
        ParchamentPanel.SetActive(false);
        playerController.CanMove = true;
    }
}
