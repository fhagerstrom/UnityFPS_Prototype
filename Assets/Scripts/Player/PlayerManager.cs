using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    private PlayerInput playerInput;
    public PlayerInput.OnFootActions onFoot;
 
    private PlayerMotor motor;
    private PlayerLook look;
    private WeaponManager weaponManager;

    // Start is called before the first frame update
    void Awake()
    {
        // Inputs
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        weaponManager = GetComponent<WeaponManager>();

        onFoot.Crouch.performed += ctx => motor.Crouch();
        onFoot.Fire.performed += ctx => Shoot();
        onFoot.Reload.performed += ctx => Reload();
        onFoot.SwitchWeapon.performed += ctx => SwitchWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());

        if (weaponManager.equippedWeapon != null)
        {
            weaponManager.equippedWeapon.HandleReloadTimer();
        }
    }

    private void OnEnable()
    {
        onFoot.Enable();
    }

    private void OnDisable()
    {
        onFoot.Disable();
    }

    // Handle shooting
    private void Shoot()
    {
        // Call the Shoot method of the current weapon
        if(weaponManager.equippedWeapon != null)
            weaponManager.equippedWeapon.Shoot();
    }

    private void Reload()
    {
        if (weaponManager.equippedWeapon != null)
            weaponManager.equippedWeapon.Reload();
    }

    private void SwitchWeapon()
    {
        if(!weaponManager.magSecPistolObject.activeSelf && weaponManager.pickedUpPistol == true) 
        {
            weaponManager.magSecPistolObject.SetActive(true);
            weaponManager.pdShotgunObject.SetActive(false);

            weaponManager.equippedWeapon = weaponManager.magSecPistolObject.GetComponent<MagSecPistol>();
        }

        else if (!weaponManager.pdShotgunObject.activeSelf && weaponManager.pickedUpShotgun == true)
        {
            weaponManager.pdShotgunObject.SetActive(true);
            weaponManager.magSecPistolObject.SetActive(false);

            weaponManager.equippedWeapon = weaponManager.pdShotgunObject.GetComponent<PdShotgun>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collided object has BaseWeapon component
        BaseWeapon pickedUpWeapon = other.GetComponent<BaseWeapon>();

        // If it does, equip the weapon
        if (pickedUpWeapon != null)
        {
            weaponManager.PickupWeapon(other);
        }
    }

}
