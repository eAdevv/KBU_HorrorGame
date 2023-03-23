using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PickUpController : MonoBehaviour
{
    // public ProjectileGun gunScript;
    public Rigidbody rb;
    public BoxCollider coll;
    public Transform player, gunContainer, fpsCam;

    public float pickUpRange;
    public float dropForwardForce, dropUpwardForce;

    public bool equipped;
    public static bool slotFull;

    private void Start()
    {
        //Setup
        if (!equipped)
        {
            //gunScript.enabled = false;
            rb.isKinematic = false;
            coll.isTrigger = false;
            this.gameObject.GetComponent<GunScript>().enabled = false;
        }
        if (equipped)
        {
            // gunScript.enabled = true;
            rb.isKinematic = true;
            coll.isTrigger = true;
            slotFull = true;
            this.gameObject.GetComponent<GunScript>().enabled = true;
        }
    }

    private void Update()
    {
        // We pick up max 3 gun.
        if(GunManager.Instance.weapons.Count <= 3) slotFull = false;
        
        else slotFull = true;
        

        //Check if player is in range and "E" is pressed
        Vector3 distanceToPlayer = player.position - transform.position;
        if (!equipped && distanceToPlayer.magnitude <= pickUpRange && Input.GetKeyDown(KeyCode.E) && !slotFull) PickUp();

        //Drop if equipped and "G" is pressed
        if (equipped && Input.GetKeyDown(KeyCode.G)) Drop();
    }

    private void PickUp()
    {
        equipped = true;
      

        //Make weapon a child of the camera and move it to default position
        transform.SetParent(gunContainer);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.Euler(Vector3.zero);
        transform.localScale = Vector3.one;

        //Make Rigidbody kinematic and BoxCollider a trigger
        rb.isKinematic = true;
        coll.isTrigger = true;

        // Add Weapon Inventory Holder
        GunManager.Instance.weapons.Add(gameObject);

        //Enable script
        this.gameObject.GetComponent<GunScript>().enabled = true;

        if(GunManager.Instance.currentWeapon != null)
        {
            gameObject.SetActive(false);
        }

        // We add the icon of the weapon we have taken to the inventory and keep the weapon in the background.
        GunManager.Instance.SetWeaponToInventory();

    }

    private void Drop()
    {
        equipped = false;

        //Set parent to null
        transform.SetParent(null);

        //Make Rigidbody not kinematic and BoxCollider normal
        rb.isKinematic = false;
        coll.isTrigger = false;

        //Gun carries momentum of player
        rb.velocity = player.GetComponent<Rigidbody>().velocity;

        //AddForce
        rb.AddForce(fpsCam.forward * dropForwardForce, ForceMode.Impulse);
        rb.AddForce(fpsCam.up * dropUpwardForce, ForceMode.Impulse);

        //Add random rotation
        float random = Random.Range(-1f, 1f);
        rb.AddTorque(new Vector3(random, random, random) * 10);

        // Remove Weapon Inventory Holder
        GunManager.Instance.weapons.Remove(gameObject);


        //Disable script
        this.gameObject.GetComponent<GunScript>().enabled = false;


        //After drop current weapon = element[0]
        GunManager.Instance.SetWeaponIndex();

        // EventManager._WeaponRemove();

        GunManager.Instance.RemoveWeapon();

    }
}
