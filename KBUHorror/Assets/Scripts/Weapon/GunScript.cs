using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GunScript : MonoBehaviour
{

    [Header("Fire Settings")]
    [SerializeField] private float damage;
    [SerializeField] private float range;
    [SerializeField] private float fireRate;
    private float nextTimtetoFire = 0f;

    [Header("Reload Settings")]
    public float maxAmmo;
    public float currentAmmo;
    public float reloadTime;
    public float maxMagBullet;
    public float currentMagBullet;
    public Text ammoText, magAmmoText;

    private float ammoToAdd;
    private bool isReloading = false;

    [Header("Effects-Camera")]
    [SerializeField] private Camera fpsCamera;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private GameObject impactEffect;
    [SerializeField] private GameObject hitMarker;
    [SerializeField] private GameObject bulletHole;

    public Sprite gunIcon;


    


    Weapon_Recoil_System recoil;
    EnemySystem enemySystem;
    private void Start()
    {
        recoil = this.gameObject.GetComponent<Weapon_Recoil_System>();
        muzzleFlash.Stop();
        currentAmmo = maxAmmo;
        currentMagBullet = maxMagBullet;
        ammoText.text = "" + currentAmmo;
        magAmmoText.text = "" + currentMagBullet;

        #region Creating Pool
        PoolManager.instance.CreatePool("impactEffect", impactEffect, 100);
        PoolManager.instance.CreatePool("bulletHole", bulletHole, 100);
        #endregion
    }

    private void OnEnable()
    {
        isReloading = false;    
    }

    void Update()
    {
        #region Reloding

        if (isReloading)
            return;   

        if ( ( currentAmmo <= 0 || Input.GetKeyDown(KeyCode.R) ) && currentMagBullet != 0) 
        {
             StartCoroutine(Reload());
             return;                 
        }
        #endregion

        #region Shooting
        if ( Time.time >= nextTimtetoFire && currentAmmo > 0)
        {         
            if (Input.GetButton("Fire1") &&  this.gameObject.tag.Equals("Auto") && GunManager.Instance.weapons.Capacity != 0)
            {
                nextTimtetoFire = Time.time + 1f / fireRate;
                Shoot();
            }
            else if (Input.GetButtonDown("Fire1") && this.gameObject.tag.Equals("nonAuto") && GunManager.Instance.weapons.Capacity != 0)
            {
                nextTimtetoFire = Time.time + 1f / fireRate;
                Shoot();
            }
            else if(Input.GetButtonDown("Fire1") && this.gameObject.tag.Equals("Shotgun") && GunManager.Instance.weapons.Capacity != 0)
            {
                ShotgunSystem();
            }
        }
        #endregion
    
        ammoText.text = "" + currentAmmo;
        magAmmoText.text = "" + currentMagBullet;
    }

    void Shoot()
    {
        recoil.Fire();
        muzzleFlash.Play();
        --currentAmmo;

        RaycastHit hit;

        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            PoolManager.instance.GetObject("impactEffect", hit.point, Quaternion.LookRotation(hit.normal));

            if (!hit.collider.gameObject.tag.Equals("Enemy"))
                PoolManager.instance.GetObject("bulletHole", hit.point, Quaternion.LookRotation(hit.normal));

            if (hit.collider.gameObject.tag.Equals("Enemy"))
                EventManager._EnemyTakeDamage(damage, hit.point);
        }
    } 

    void ShotgunSystem()
    {
        recoil.Fire();
        muzzleFlash.Play();
        currentAmmo -= 5f;

        for (int i = 0; i<5;i++)
        {
            RaycastHit hitShotgun;
            if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward + new Vector3(Random.Range(-.2f, .2f), Random.Range(-.1f, .1f), 0), out hitShotgun, range))
            {
                PoolManager.instance.GetObject("impactEffect", hitShotgun.point, Quaternion.LookRotation(hitShotgun.normal));
            }
        }
        
    }  
    IEnumerator Reload()
    {
        isReloading = true;
        ammoToAdd = maxAmmo - currentAmmo;

        yield return new WaitForSeconds(reloadTime);

        if(currentMagBullet >= ammoToAdd)
        {
            currentAmmo += ammoToAdd;
            currentMagBullet -= ammoToAdd;
        }
        else
        {      
            currentAmmo += currentMagBullet;
            currentMagBullet -= currentMagBullet;
        }

            isReloading = false; ;
    }

}
