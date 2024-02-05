using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponManager : MonoBehaviour
{
    public List<BaseWeapon> weapons;
    public BaseWeapon equippedWeapon;

    public MagSecPistol magSecPistolObject;
    public PdShotgun shotgunObject;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize all weapons
        weapons = new List<BaseWeapon>
        {
           magSecPistolObject,
           shotgunObject,
        };

        equippedWeapon = weapons[0]; // Start with MagSec pistol
        Debug.Log(equippedWeapon.ToString());
    }

    // Update is called once per frame
    void Update()
    {

    }
   
}