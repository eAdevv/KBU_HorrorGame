using System;
using UnityEngine;

public static class EventManager
{
    public static event Action<float, Vector3> OnEnemyTakeDamage;
    public static event Action<float> OnPlayerTakeDamage;
    public static event EventHandler OnWeaponRemove;

    public static void _EnemyTakeDamage(float myDamage, Vector3 myPoint)
    {
        OnEnemyTakeDamage?.Invoke(myDamage, myPoint);
    }

    public static void _PlayerTakeDamage(float eDamage)
    {
        OnPlayerTakeDamage?.Invoke(eDamage);
    }

    public static void _WeaponRemove()
    {
        OnWeaponRemove?.Invoke(null,EventArgs.Empty);
    }




}
