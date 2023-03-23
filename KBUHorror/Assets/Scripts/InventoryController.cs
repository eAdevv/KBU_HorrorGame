using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();  
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            anim.SetBool("Open", true);
            Cursor.lockState = CursorLockMode.None;
        }


        if (Input.GetKeyUp(KeyCode.Tab))
        {
            anim.SetBool("Open", false);
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
