using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GunManager : MonoSingleton<GunManager>
{
  
    public List<GameObject> weapons = new List<GameObject>();
    public List<Image> Icons = new List<Image>();
    public GameObject currentWeapon;


    private void Start()
    {
        for (int i = 0; i < Icons.Count; i++)
        {
            Icons[i].enabled = false;
        }
    }
    private void Update()
    {      

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }  
        
        // Active gun in the Inventory is currentWeapon and Set Inventory icons.
        for(int i = 0; i<weapons.Count; i++)
        {
            if(weapons[i].activeSelf == true)
            {
                currentWeapon = weapons[i];
            }
        }
            
    }

    // We add the icon of the weapon we have taken to the inventory and keep the weapon in the background.
    public void SetWeaponToInventory()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            Icons[i].GetComponent<Image>().sprite = weapons[i].GetComponent<GunScript>().gunIcon;
            Icons[i].enabled = true;

            if (Icons[i].GetComponent<Image>().sprite != null)
            {
                Icons[i].GetComponentInParent<InventoryWheelManager>().holdWeapon = weapons[i];
            }
        }
    }

    public void RemoveWeapon()
    {

        for(int i = 0; i<Icons.Count; i++)
        {
            if(currentWeapon == Icons[i].GetComponentInParent<InventoryWheelManager>().holdWeapon)
            {
                Debug.Log(i);
                Icons[i].GetComponent<Image>().sprite = null;
                Icons[i].enabled = false;
                Icons[i].GetComponentInParent<InventoryWheelManager>().holdWeapon = null;
            }       
        }
    }

    

    //After drop current weapon set to 0 index.
    public void SetWeaponIndex()
    {
        if (weapons.Count != 0)
        {
            currentWeapon = Instance.weapons[0];
            Instance.currentWeapon.SetActive(true);
        }
        else
        {
            Instance.currentWeapon = null;
        }
    }






}
