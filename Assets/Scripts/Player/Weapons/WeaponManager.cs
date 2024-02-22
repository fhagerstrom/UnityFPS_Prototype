using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public BaseWeapon equippedWeapon;

    private MagSecPistol magSecPistol;
    private PdShotgun pdShotgun;

    public GameObject magSecPistolObject;
    public GameObject pdShotgunObject;

    public bool pickedUpPistol = false;
    public bool pickedUpShotgun = false;

    // Start is called before the first frame update
    void Start()
    {
        // Get weapon components
        magSecPistol = magSecPistolObject.GetComponent<MagSecPistol>();
        pdShotgun = pdShotgunObject.GetComponent<PdShotgun>();

        // Hide the player's weapons since they aren't initially "picked up"
        if (magSecPistol != null)
            magSecPistol.gameObject.SetActive(false);

        if (pdShotgun != null)
            pdShotgun.gameObject.SetActive(false);

        // Activate the initially equipped weapon
        if(equippedWeapon != null)
            equippedWeapon.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void PickupWeapon(Collider weaponCollider)
    {

        if(weaponCollider.CompareTag("Pistol"))
        {
            if(pickedUpPistol == false)
                pickedUpPistol = true;
        }

        if (weaponCollider.CompareTag("Shotgun"))
        {
            if (pickedUpShotgun == false)
                pickedUpShotgun = true;
        }

        BaseWeapon pickedUpWeapon = weaponCollider.GetComponent<BaseWeapon>();

        if(pickedUpWeapon != null)
        {
            weaponCollider.gameObject.SetActive(false);

            Debug.Log("Picked up weapon: " + pickedUpWeapon);
        }
    }

}