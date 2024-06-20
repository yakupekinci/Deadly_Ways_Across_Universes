using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityTutorial.PlayerControl;

public class EnemeyAttack : MonoBehaviour
{
    [SerializeField] private float attackDamage = 1f;
    public PlayerController playerController;
    private void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
    }
    public void AttackToPlayer()
    {
        AudioManager.instance.PlayEffect("Damage");
        playerController.TakeDamage(attackDamage);
        playerController.TriggerBloodEffect();

    }
}
