using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GunData gunData;
    [SerializeField] private Transform muzzle;

    float timeSinceLastShot;

    public void Start() {
        //Subscribe to Shoot method of PlayerShoot script
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;

    }

    public void StartReload(){
        if(!gunData.reloading && this.gameObject.activeSelf){
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload(){
        gunData.reloading = true;

        yield return new WaitForSeconds(gunData.reloadTime);

        gunData.currentAmmo = gunData.magSize;

        gunData.reloading = false;
    }

    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);
    public void Shoot(){
     

        if(gunData.currentAmmo > 0)
        {
            if (CanShoot()){
                   Debug.Log("Player Shot!");
                   Debug.Log("Ammo: " + gunData.currentAmmo);

                if(Physics.Raycast(muzzle.position, muzzle.forward, out RaycastHit hitInfo, gunData.maxDistance));
                {
                    IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
                    damageable?.Damage(gunData.damage);
                }

                gunData.currentAmmo--;
                timeSinceLastShot = 0;
                OnGunShot();
            }
        }
    }

    private void Update() {
        timeSinceLastShot += Time.deltaTime;
        
        Debug.DrawRay(muzzle.position, muzzle.forward * gunData.maxDistance, Color.red, .02f);
    }

    private void OnGunShot(){
        Debug.Log("OnGunShot() called");
    }
}
