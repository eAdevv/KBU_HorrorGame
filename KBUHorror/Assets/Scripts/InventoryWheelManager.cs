using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class InventoryWheelManager : MonoBehaviour
{
    private Animator anim;
    public GameObject holdWeapon;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void HoverTrue()
    {
        anim.SetBool("Hover", true);
    }

    public void HoverFalse()
    {
        anim.SetBool("Hover", false);
    }



    // We change current weapon after button click.
    public void GetWeapon()
    {
        foreach (GameObject obj in GunManager.Instance.weapons)
        {
            int i = GunManager.Instance.weapons.FindIndex(x => x == obj);
            GunManager.Instance.weapons[i].SetActive(false);
        }
       
        int weaponIndex = GunManager.Instance.weapons.IndexOf(holdWeapon);      
        GunManager.Instance.weapons[weaponIndex].SetActive(true);
        GunManager.Instance.currentWeapon = GunManager.Instance.weapons[weaponIndex];
    }



}
