using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Mirror;

public class WeaponManager : MonoBehaviour {
    public Transform HipFirePos;
    public Transform ADSPos;
    public Transform gunParent;
    public float aimSpeed;
    public float aimRotateSpeed;
    public WeaponManager currentWeapon;

    [Header("Input")]
    public InputAction aimAction;
    bool isAiming;

    public InputAction reloadAction;
    public InputAction shootAction;
    bool isShooting;


    private void OnEnable() {
        aimAction.Enable();
        reloadAction.Enable();
        shootAction.Enable();
    }

    private void OnDisable() {
        aimAction.Disable();
        reloadAction.Disable();
        shootAction.Disable();
    }
    private void Awake() {
        aimAction.started += context => isAiming = true;
        aimAction.performed += context => isAiming = true;
        aimAction.canceled += context => isAiming = false;

        shootAction.started += context => isShooting = true;
        shootAction.performed += context => isShooting = true;
        shootAction.canceled += context => isShooting = false;
    }

    public void Update(){
        if(isAiming == true) {
            gunParent.position = Vector3.Lerp(gunParent.position, ADSPos.position, Time.deltaTime * aimSpeed);
            gunParent.rotation = Quaternion.Slerp(gunParent.rotation, ADSPos.rotation, Time.deltaTime * aimRotateSpeed);
        }else {
            gunParent.position = Vector3.Lerp(gunParent.position, HipFirePos.position, Time.deltaTime * aimSpeed);
            gunParent.rotation = Quaternion.Slerp(gunParent.rotation, HipFirePos.rotation, Time.deltaTime * aimRotateSpeed);
        }
    }
}