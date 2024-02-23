using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerAmmo : MonoBehaviour
{
    private WeaponManager weaponManager;
    public TextMeshProUGUI ammoText;

    // Start is called before the first frame update
    void Start()
    {
        weaponManager = GetComponent<WeaponManager>();

        // If the player doesn't have a weapon equipped. Set text to 0 ammo.
        if(weaponManager.equippedWeapon == null)
            ammoText.text = "0 / 0";
    }

    // Update is called once per frame
    void Update()
    {
        if (weaponManager.equippedWeapon != null)
        {
            weaponManager.equippedWeapon.currentBulletsLeft = Mathf.Clamp(weaponManager.equippedWeapon.currentBulletsLeft, 0, weaponManager.equippedWeapon.maxBullets);
            weaponManager.equippedWeapon.currentReserveAmmo = Mathf.Clamp(weaponManager.equippedWeapon.currentReserveAmmo, 0, weaponManager.equippedWeapon.maxReserveAmmo);
            UpdateAmmoUI();
        }
    }

    public void UpdateAmmoUI()
    {
        if (weaponManager.equippedWeapon != null)
        {
            int bulletsLeft = weaponManager.equippedWeapon.currentBulletsLeft;
            int reserveAmmo = weaponManager.equippedWeapon.currentReserveAmmo;
            string ammoString = bulletsLeft + " / " + reserveAmmo;

            ammoText.text = ammoString;
        }
    }
}
