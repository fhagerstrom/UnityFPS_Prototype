using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI promptText;
    [SerializeField]
    private TextMeshProUGUI weaponText;

    private WeaponManager weaponManager;

    // Start is called before the first frame update
    void Start()
    {
        weaponManager = GetComponent<WeaponManager>();
    }

    // Update is called once per frame
    public void UpdateText(string promptMsg)
    {
        promptText.text = promptMsg;

        if(weaponManager.equippedWeapon == null) 
            weaponText.text = string.Empty;

        else
            weaponText.text = weaponManager.equippedWeapon.name;
    }
}
