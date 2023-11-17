using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Converts Player input into Player Shoot Action 
/// </summary> <summary>
/// 
/// </summary>
public class PlayerShoot : MonoBehaviour
{
    private const string PLAYER_TAG = "Player";

    public static Action shootInput;
    public static Action reloadInput;

    [SerializeField] private KeyCode reloadKey;

    [SerializeField] private Camera firstPersonCamera;
    [SerializeField] private LayerMask layerMask;

    private void Update() {
        if (Input.GetMouseButtonDown(0))
        {
            shootInput?.Invoke();
        }
        if(Input.GetKeyDown(KeyCode.R)){
            reloadInput?.Invoke();
        }

        // if (Input.GetMouseButtonDown(0))
        // {
        //     var fireRate = 1f / 1f;
        //     InvokeRepeating(nameof(Shoot), 0f, fireRate);
        // }
        // else if (Input.GetMouseButtonUp(0))
        // {
        //     // Cancel shoot
        //     CancelInvoke(nameof(Shoot));
        // }
    }
    // [Client]
    // void Shoot()
    // {
    //     if (!isLocalPlayer)
    //     {
    //         return;
    //     }

    //     RaycastHit _hit;
    //     var weaponDistance = 200f;
    //     if (Physics.Raycast(firstPersonCamera.transform.position, firstPersonCamera.transform.forward, out _hit, weaponDistance))
    //     {
    //         if (_hit.collider.CompareTag(PLAYER_TAG))
    //         {
    //             var damage = 10;
    //             var shooterConnectionID = GetComponent<NetworkIdentity>().netId.ToString();
    //             var target = _hit.collider.gameObject.GetComponent<NetworkIdentity>().netId.ToString();
    //             CmdPlayerShot(target, damage, shooterConnectionID);
    //         }
    //     }
    // }

    // [Command]
    // void CmdPlayerShot(string target, int damage, string shooterConnectionID)
    // {
    //     var player = GameManager.GameManager.GetPlayerByConnectionID(new ConnectionID(target));
    //     player.RpcTakeDamage(damage, shooterConnectionID);
    // }

}
