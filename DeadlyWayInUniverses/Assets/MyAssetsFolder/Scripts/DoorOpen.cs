using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{

    [SerializeField] private GameObject actionKey;
    [SerializeField] private GameObject actionText;
    [SerializeField] private GameObject door;
    [SerializeField] private AudioSource doorSound;

    private float theDistance;
    private void Update()
    {
        theDistance = PlayerRay.distanceFromTarget;
    }
    private void OnMouseOver()
    {
        if (theDistance < 2)
        {
            actionKey.SetActive(true);
            actionText.SetActive(true);
        }


    }
    private void OnMouseExit()
    {
        actionKey.SetActive(false);
        actionText.SetActive(false);
    }
}
