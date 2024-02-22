using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerAmmo : MonoBehaviour
{
    public BaseWeapon weapon;
    public TextMeshProUGUI ammoText;


    // Start is called before the first frame update
    void Start()
    {
        ammoText = GetComponent<TextMeshProUGUI>();
        weapon = GetComponent<BaseWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAmmoUI();
    }

    public void UpdateAmmoUI()
    {
        if (weapon != null)
        {
            int bulletsLeft = weapon.currentBulletsLeft;
            int reserveAmmo = weapon.currentReserveAmmo;
            string ammoString = bulletsLeft + " / " + reserveAmmo;

            ammoText.text = ammoString;
        }
    }
}
