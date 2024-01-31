using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    public BaseWeapon currentWeapon;

    public GameObject equippedWeapon = null;

    GameObject magSecPistolObject;
    GameObject pdShotgunObject;

    public void EquipWeapon(GameObject weapon)
    {
        // Instantiate "new" weapon, bad code i know
        currentWeapon = weapon.GetComponent<BaseWeapon>();
        equippedWeapon = weapon;

        Debug.Log("Equipped weapon: " + currentWeapon.GetType().Name);
    }
    // Start is called before the first frame update
    void Start()
    {
        // Initialise all weapons
        equippedWeapon = FindObjectOfType<MagSecPistol>().gameObject; 

        currentWeapon = equippedWeapon.GetComponent<BaseWeapon>();

        if (currentWeapon == null)
        {
            Debug.LogError("Current weapon is null!");
            return;
        }
       
        Debug.Log(equippedWeapon);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Shoot()
    {
        if (equippedWeapon == null)
        {
            Debug.LogError("Equipped weapon is null.");
            return;
        }
        equippedWeapon.gameObject.GetComponent<BaseWeapon>().Shoot();
    }
    public void Reload()
    {
        equippedWeapon.gameObject.GetComponent<BaseWeapon>().Reload();
    }
}