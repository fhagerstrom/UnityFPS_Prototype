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
    private BaseWeapon baseWeapon;

    // Start is called before the first frame update
    void Awake()
    {
        // Inputs
        playerInput = new PlayerInput();
        onFoot = playerInput.OnFoot;

        motor = GetComponent<PlayerMotor>();
        look = GetComponent<PlayerLook>();
        weaponManager = GetComponent<WeaponManager>();
        baseWeapon = GetComponent<BaseWeapon>();

        onFoot.Crouch.performed += ctx => motor.Crouch();
        onFoot.Jump.performed += ctx => motor.Jump();
        onFoot.Fire.performed += ctx => Shoot();
        onFoot.Reload.performed += ctx => Reload();
        onFoot.SwitchWeapon.performed += ctx => SwitchWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        motor.ProcessMove(onFoot.Movement.ReadValue<Vector2>());
        look.ProcessLook(onFoot.Look.ReadValue<Vector2>());

        weaponManager.equippedWeapon.GetComponent<BaseWeapon>().HandleReloadTimer();
    }

    private void FixedUpdate()
    {
        
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
        weaponManager.equippedWeapon.GetComponent<BaseWeapon>().Shoot();
    }

    private void Reload()
    {
        weaponManager.equippedWeapon.GetComponent<BaseWeapon>().Reload();
    }

    private void SwitchWeapon()
    {
        // TODO: Add logic when more weapons are added
    }

}
