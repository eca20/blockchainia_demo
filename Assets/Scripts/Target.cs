using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Target : MonoBehaviour, IDamageable
{
    
    [Header("Stats")]
    [SerializeField]
    public float health = 100f;

    [SerializeField]
    public float defense;

    public void Damage(float Damage)
    {
        health -= Damage / defense;
        if(health <= 0 && gameObject != null) {
                Destroy(gameObject);
        }
    }

}
