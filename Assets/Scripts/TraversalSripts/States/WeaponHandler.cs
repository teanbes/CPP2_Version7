using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField] private GameObject weaponLogic;
    [SerializeField] private GameObject sword;
    [SerializeField] private GameObject powerUpEffect;

    public void EnableWeapon()
    {
        weaponLogic.SetActive(true);
    } 

    public void DisableWeapon()
    {
        weaponLogic.SetActive(false);
    }

    public void EnableSword()
    {
        sword.SetActive(true);
    }

    public void DisableSword()
    {
        sword.SetActive(false);
    }

    public void PowerUpEffectEnable()
    {
        powerUpEffect.SetActive(true);
    }

    public void PowerUpEffectDisable()
    {
        powerUpEffect.SetActive(false);
    }



}
