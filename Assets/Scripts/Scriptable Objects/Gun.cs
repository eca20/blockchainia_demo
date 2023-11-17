using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;

public class Gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] public GunData gunData;
    [SerializeField] public Transform muzzle;

    float timeSinceLastShot;

    public float currentAmmo;

    private Camera playerCam;

    public void Start() {
        //Subscribe to Shoot method of PlayerShoot script
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;
        playerCam = this.GetComponentInParent<Camera>();
        muzzle = this.GetComponentInParent<Transform>();
    }

    public void StartReload(){
        Debug.Log("StartReload() called");

        // if(!gunData.reloading ){
        if( this.gunData != null){
            if(!this.gunData.reloading && this.gameObject.activeInHierarchy){

                StartCoroutine(Reload());
            }
        }
    }    
    

    private IEnumerator Reload(){
        Debug.Log("Reload() called");

        gunData.reloading = true;

        yield return new WaitForSeconds(gunData.reloadTime);

        gunData.currentAmmo = gunData.magSize;

        gunData.reloading = false;
    }

    private bool CanShoot() => currentAmmo > 0 && !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);
    public void Shoot(){
        Debug.Log("Shoot() called");

        if(CanShoot())
        {
            Debug.Log("Ammo: " + gunData.currentAmmo);
//            Debug.Log("muzzle.position:" + muzzle.position);
            if(gunData != null){
                if (gunData.currentAmmo > 0){
                    gunData.currentAmmo--;
                    timeSinceLastShot = 0;
                    OnGunShot();
                }
            }
            Debug.Log("Player Shot!");
            Debug.Log("Ammo: " + gunData.currentAmmo);
            if(muzzle.position != null){
                Debug.Log("muzzle.position:" + muzzle.position);
                if(Physics.Raycast(muzzle.position, muzzle.forward, out RaycastHit hitInfo, gunData.maxDistance));
                {
                    if(hitInfo.transform != null){
                        IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
                        Debug.Log("hitInfo: " + hitInfo.transform.GetComponent<IDamageable>());

                        damageable?.Damage(gunData.damage);
                    }

                }
                gunData.currentAmmo--;
                timeSinceLastShot = 0;
            }
            OnGunShot();
        }
    }

    private void Update() {
        timeSinceLastShot += Time.deltaTime;
        currentAmmo = gunData.currentAmmo;
        
        Debug.DrawRay(muzzle.position, muzzle.forward * gunData.maxDistance, Color.red, .02f);
    }

    private void OnGunShot(){
        Debug.Log("OnGunShot() called");
        // Vector3 point = new Vector3 (playerCam.pixelWidth / 2, playerCam.pixelHeight / 2, 0);
        // Ray ray = playerCam.ScreenPointToRay(point);
        // RaycastHit hit;
        // if(Physics.Raycast(ray, out hit)) {
        //     GameObject hitObject = hit.transform.gameObject;
        //     StartCoroutine (ShotGen (hit.point));
        //     if(hit.transform != null){
        //         IDamageable damageable = hit.transform.GetComponent<IDamageable>();
        //         Debug.Log("hitInfo: " + damageable.GetType());

        //         damageable?.Damage(gunData.damage);
        //     }
        // }
    }

    private IEnumerator ShotGen (Vector3 pos){
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.localScale = new Vector3(0.1f, 0.1f, 0.2f);
        sphere.transform.position = pos;
        yield return new WaitForSeconds (2);

        Destroy(sphere);
    }
}
