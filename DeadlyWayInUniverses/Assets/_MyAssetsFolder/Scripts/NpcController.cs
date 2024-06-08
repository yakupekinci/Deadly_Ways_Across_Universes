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
    public GameObject door; // Door GameObject
    private PlayerController playerController; // PlayerController referansı

    private void Start()
    {

        playerController = FindObjectOfType<PlayerController>(); // PlayerController örneğini bul
        canvas.SetActive(false); // Initially keep Canvas inactive
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
                canvas.SetActive(true); // Activate the canvas
            }
            else
            {
                playerController.CanMove = true;
                door.SetActive(true); // Open the door
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
    public void CollectScroll()
    {
        playerScrollCount++;
    }
}
