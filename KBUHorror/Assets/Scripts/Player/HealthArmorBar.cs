using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthArmorBar : MonoBehaviour
{
    public Image healthBar, armorBar;
    private float currentHealth, maxHealth, currentArmor, maxArmor;


    private void Start()
    {
        maxHealth = PlayerManager.Instance.Health;
        maxArmor = PlayerManager.Instance.Armor;
    }

    private void Update()
    {
        currentHealth = PlayerManager.Instance.Health;
        currentArmor = PlayerManager.Instance.Armor;

        healthBar.fillAmount = currentHealth / maxHealth;
        armorBar.fillAmount = currentArmor / maxArmor;

        if (currentHealth < 50f)
            healthBar.color = Color.red;
         
    }
}
