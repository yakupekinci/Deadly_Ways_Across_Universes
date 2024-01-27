using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityTutorial.PlayerControl;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 15f;
    [SerializeField] private float attackDistance = 5f;
    [SerializeField] private float followDistance = 200f;

    private GameObject player;
    private NavMeshAgent navMeshAgent;
    private Animator anim;
    private bool isActive;

    private void Awake()
    {

        anim = transform.GetChild(0).GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerController>().gameObject;
    }
    private void Start()
    {
        isActive = true;
        navMeshAgent.SetDestination(player.transform.position);
        navMeshAgent.speed = movementSpeed;
    }

    private void Update()
    {
        if (!isActive)
            return;
        else
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);

            if (distance < followDistance)
            {
                navMeshAgent.SetDestination(player.transform.position);
                if (distance > 20)
                {
                    anim.SetBool("isWalking", true); 
                    anim.SetBool("isRunning", true);
                    navMeshAgent.speed = movementSpeed * 1.5f;
                }
                else if (distance < 20)
                {
                    anim.SetBool("isRunning", false);
                    anim.SetBool("isWalking", true);
                    navMeshAgent.speed = movementSpeed;
                }

                if (distance < attackDistance)
                {
                    anim.SetBool("isWalking", false);
                    anim.SetBool("isAttacking", true);
                    navMeshAgent.isStopped = true;
                }
                else
                {
                    anim.SetBool("isWalking", false);
                    anim.SetBool("isAttacking", false);
                    navMeshAgent.isStopped = false;
                }
            }
            else
            {
                navMeshAgent.isStopped = true;
            }

        }
    }
}

