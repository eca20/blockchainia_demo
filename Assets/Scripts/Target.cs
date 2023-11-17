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
       this.health -= Damage;
        if(this.health <= 0 && gameObject != null) {
            Elimination();
        }
    }

    public void Elimination(){
        // add experiece
        // when all enemies eliminated, end round
        //
        Debug.Log("Eliminating GameObject: " + gameObject);
        NetworkServer.Destroy(gameObject);
    }

}
