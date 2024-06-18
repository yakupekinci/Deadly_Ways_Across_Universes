using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityTutorial.PlayerControl;

public class ZombieMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 15f;
    [SerializeField] private float attackDistance = 5f;
    [SerializeField] private float followDistance = 200f;
    [SerializeField] private float speedDistance = 20f;
    [SerializeField] private float idleDuration = 2f; // Bekleme süresi
    [SerializeField] private float randomAreaRadius = 10f; // Rastgele alan yarıçapı

    private GameObject player;
    private NavMeshAgent navMeshAgent;
    private Animator anim;
    private bool isActive;
    private bool isIdle;
    private float idleTimer;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<PlayerController>().gameObject;
    }

    private void Start()
    {
        isActive = true;
        navMeshAgent.SetDestination(player.transform.position);
        navMeshAgent.speed = movementSpeed;

        isIdle = false;
        idleTimer = 0f;
    }

    private void Update()
    {
        if (!isActive)
            return;

        float distance = Vector3.Distance(transform.position, player.transform.position);

        if (distance < followDistance)
        {
            navMeshAgent.SetDestination(player.transform.position);

            if (distance > speedDistance)
            {
                anim.SetBool("isWalking", true);
                navMeshAgent.speed = movementSpeed * 2f;
            }
            else if (distance < speedDistance)
            {
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
                anim.SetBool("isAttacking", false);
                navMeshAgent.isStopped = false;
            }

        }
        else
        {
            if (!isIdle)
            {
                SetRandomDestination();
            }
            else
            {
                idleTimer += Time.deltaTime;
                if (idleTimer >= idleDuration)
                {
                    isIdle = false;
                    idleTimer = 0f;
                }
            }
        }

        // Check if the zombie is actually moving
        if (navMeshAgent.velocity.sqrMagnitude > 0.1f)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
    }

    private void SetRandomDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * randomAreaRadius;
        randomDirection += transform.position;

        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, randomAreaRadius, 1);

        if (hit.position != null)
        {
            navMeshAgent.SetDestination(hit.position);  
            isIdle = true;
        }
    }
}
