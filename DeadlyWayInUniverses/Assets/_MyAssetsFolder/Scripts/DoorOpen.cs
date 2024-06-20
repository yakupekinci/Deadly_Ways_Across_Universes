using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{

    [SerializeField] private GameObject actionKey;
    [SerializeField] private GameObject actionText;
    [SerializeField] private AudioSource doorSound;
    [SerializeField] private Animation anim;
    private float theDistance;
    private bool isOpen = false;
    private void Start()
    {
        anim = GetComponent<Animation>();
    }
    private void Update()
    {
        theDistance = PlayerRay.distanceFromTarget;
    }
    private void OnMouseOver()
    {
        if (Vector3.Distance(transform.position, Camera.main.transform.position) <= 3f && !isOpen)
        {
            actionKey.SetActive(true);
            actionText.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (gameObject.tag == "HingeDoor")
                {
                    if (isOpen)
                    {
                        anim.Play("GlassDoorClose");
                        isOpen = false;
                    }
                    else if (!isOpen)
                    {
                        anim.Play("GlassDoor");
                        isOpen = true;
                    }
                }
                else
                {
                    anim.Play();
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
