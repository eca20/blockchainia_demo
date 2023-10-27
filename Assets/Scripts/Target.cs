using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IDamageable
{
    
    [Header("Stats")]
    [SerializeField]
    public float health = 100f;

    public void Damage(float Damage)
    {
        health -= Damage;
        if(health <= 0){
            Destroy(gameObject);
        }
    }


}
