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

    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize all weapons
        weapons = new List<BaseWeapon>();

        equippedWeapon = null; // Start with no guns

        // Activate the GameObject of the initially equipped weapon
        equippedWeapon.gameObject.SetActive(true);

        // Deactivate of all other weapons
        foreach (var weapon in weapons)
        {
            if (weapon != equippedWeapon)
            {
                weapon.gameObject.SetActive(false);
            }
        }

        playerTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Function to equip a new weapon
    public void EquipWeapon(BaseWeapon newWeapon)
    {
        if (newWeapon != null && !weapons.Contains(newWeapon))
        {
            // Deactivate current weapon
            if (equippedWeapon != null)
                equippedWeapon.gameObject.SetActive(false);

            // Add weapon to list
            if (!weapons.Contains(newWeapon))
                weapons.Add(newWeapon);

            // Set new weapon as the equipped weapon
            equippedWeapon = newWeapon;
            equippedWeapon.gameObject.SetActive(true);

            // Set the player as the parent of the weapon
            if (playerTransform != null)
                equippedWeapon.transform.SetParent(playerTransform);

            Debug.Log("Equipped: " + equippedWeapon.ToString());
        }
    }

    public void PickupWeapon(Collider weaponCollider)
    {
        BaseWeapon pickedUpWeapon = weaponCollider.GetComponent<BaseWeapon>();

        if(pickedUpWeapon != null)
        {
            EquipWeapon(pickedUpWeapon);

            weaponCollider.gameObject.SetActive(false);

            Debug.Log("Picked up weapon: " + pickedUpWeapon.ToString());
        }
    }

}