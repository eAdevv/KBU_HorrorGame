using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class PlayerManager : MonoSingleton<PlayerManager>
{

    public float Health;
    public float Armor;
    [Header("TEXTS")]
    public TMP_Text healthText, armorText; 
    public GameObject getHitScreen;

    private void OnEnable()
    {
        EventManager.OnPlayerTakeDamage += PlayerTakeDamage;
    }
    private void OnDisable()
    {
        EventManager.OnPlayerTakeDamage -= PlayerTakeDamage;
    }
    void Update()
    {
        healthText.text = "" + Health;
        armorText.text = "" + Armor;   

        if (Health <= 0) Die();

        if (Armor < 0) Armor = 0;


        if (getHitScreen != null)
        {
            if(getHitScreen.GetComponent<Image>().color.a > 0)
            {
                var color = getHitScreen.GetComponent<Image>().color;
                color.a -= 0.01f;
                getHitScreen.GetComponent<Image>().color = color;                   
            }
        }

    }

    public void PlayerTakeDamage(float enemyDamage)
    {
        if (Armor > 0)
            Armor -= enemyDamage;
        else 
            Health -= enemyDamage;

    }

    void Die()
    {
        /// DIE SYSTEM ///
        /// Ölüm ekranı.
    }
}