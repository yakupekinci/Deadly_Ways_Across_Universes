using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTutorial.PlayerControl;

public class CollectParts : MonoBehaviour
{
    [SerializeField] private GameObject actionKey;
    [SerializeField] private GameObject actionText;
    [SerializeField] private GameObject collectPos;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private AudioSource collectSound;
    [SerializeField] private GameObject ParchamentPanel;
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

                /*  playerController.ResumeAnimations(); */

                ParchamentPanel.SetActive(false);
                playerController.CanMove = true;

            }
        }
    }

    private void OnMouseOver()
    {
        if (theDistance < 2)
        {
            actionKey.SetActive(true);
            actionText.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (gameObject.CompareTag("Parchment"))
                {
                    transform.gameObject.SetActive(false);
                    playerController.CanMove = false;
                    isCollect = true;
                    ParchamentPanel.SetActive(true);
                    NPCController.CollectScroll();

                    /*   playerController.PauseAnimations();
                      transform.SetParent(collectPos.transform);
                      transform.position = collectPos.transform.position;
                      transform.localRotation = Quaternion.Euler(0f, -90f, 71f);
                      mainCamera.transform.localRotation = Quaternion.Euler(0f, 0f, 0f); */


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
}
