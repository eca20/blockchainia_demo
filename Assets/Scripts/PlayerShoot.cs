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
    public static Action shootInput;
    public static Action reloadInput;

    [SerializeField] private KeyCode reloadKey;

    private void Update() {
        if (Input.GetMouseButton(0))
        {
            shootInput?.Invoke();
        }
        if(Input.GetKeyDown(reloadKey)){
            reloadInput?.Invoke();
        }
    }
}
