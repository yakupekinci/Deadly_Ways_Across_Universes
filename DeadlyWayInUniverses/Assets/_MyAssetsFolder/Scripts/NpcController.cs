using System.Collections;
using TMPro;
using UnityEngine;

using UnityEngine.UI;
using UnityTutorial.PlayerControl;

public class NpcController : MonoBehaviour
{
    public int totalScrollsNeeded = 4; // Total scrolls needed
    public int playerScrollCount = 0; // Player's collected scroll count
    public GameObject canvas; // Canvas GameObject
    public TMP_Text infoText; // Text element inside Canvas

    public TMP_Text PickedCountTxT; // Text element inside Canvas
    public TMP_Text ParchamentCountTxGUI;
    public GameObject door; // Door GameObject
    public Animator doorAnim;
    private PlayerController playerController; // PlayerController reference
    public Camera mainCamera; // Main camera reference
    public Camera doorCamera; // Door camera reference
    CollectParts collectParts;

    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>(); // Find PlayerController instance
        canvas.SetActive(false); // Initially keep Canvas inactive
        doorCamera.gameObject.SetActive(false); // Initially keep door camera inactive
        collectParts = FindObjectOfType<CollectParts>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && canvas.activeSelf) // Check if ESC is pressed and canvas is active
        {
            canvas.SetActive(false); // Deactivate the canvas
            playerController.CanMove = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // When the player enters NPC's area
        {
            if (playerScrollCount < totalScrollsNeeded)
            {
                playerController.CanMove = false;
                int remainingScrolls = totalScrollsNeeded - playerScrollCount;
                infoText.text = "You need to collect " + remainingScrolls + " more scrolls to open the door!";
                PickedCountTxT.text = playerScrollCount.ToString();
                ParchamentCountTxGUI.text = playerScrollCount.ToString();
                canvas.SetActive(true); // Activate the canvas
            }
            else
            {

            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) // When the player exits NPC's area
        {
            canvas.SetActive(false); // Deactivate the canvas
            playerController.CanMove = true;
        }
    }

    // This method can be called when the player collects a scroll
    public void CollectScroll(GameObject panel)
    {
        playerScrollCount++;
        ParchamentCountTxGUI.text = playerScrollCount.ToString();
        if (playerScrollCount >= totalScrollsNeeded)
        {
            StartCoroutine(OpenDoorSequenceIE(panel));
        }
    }

    public void OpenDoorSequence(GameObject panel)
    {
        StartCoroutine(OpenDoorSequenceIE(panel));
    }
    private IEnumerator OpenDoorSequenceIE(GameObject panel)
    {
        playerController.CanMove = false;
        mainCamera.gameObject.SetActive(false);
        doorCamera.gameObject.SetActive(true);
        panel.gameObject.SetActive(false);
        doorAnim.SetBool("isOpen", true); // Open the door animation
        yield return new WaitForSeconds(5f); // Wait for the door animation to complete (adjust time as needed)
        panel.gameObject.SetActive(true);
        doorCamera.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);
        playerController.CanMove = true;
    }
}
