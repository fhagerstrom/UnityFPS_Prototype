using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    public IWeapon currentWeaponInterface;

    public GameObject startWeapon = null;

    GameObject magSecPistolObject;
    GameObject pdShotgunObject;
    public GameObject equippedWeapon;


    public void EquipWeapon(GameObject weapon)
    {
        // Remove UnityEvents listener from previous weapon
        if (currentWeaponInterface != null)
        {
            currentWeaponInterface.OnShoot.RemoveListener(OnShootHandler);
            currentWeaponInterface.OnReload.RemoveListener(OnReloadHandler);
            currentWeaponInterface.OnEnemyHit.RemoveListener(OnEnemyHitHandler);
        }

        // Instantiate "new" weapon, bad code i know
        currentWeaponInterface = weapon.GetComponent<IWeapon>();
        equippedWeapon = weapon;

        if(currentWeaponInterface != null)
        { 
            currentWeaponInterface.OnShoot.AddListener(OnShootHandler);
            currentWeaponInterface.OnReload.AddListener(OnReloadHandler);
            currentWeaponInterface.OnEnemyHit.AddListener(OnEnemyHitHandler);
        }
        Debug.Log("Equipped weapon: " + currentWeaponInterface.GetType().Name);
    }

    private void OnShootHandler()
    {
        Debug.Log("PlayerWeapon shoot event is handling!");

    }

    private void OnReloadHandler()
    {
        Debug.Log("PlayerWeapon reload event is handling!");
    }

    private void OnEnemyHitHandler(float damage)
    {
        Debug.Log("PlayerWeapon enemy hit event is handling!");
    }


    // Start is called before the first frame update
    void Start()
    {
        // Check for null weapon
        if (startWeapon == null)
            startWeapon = magSecPistolObject;

        // Initialise all weapons
        magSecPistolObject = new GameObject();
        magSecPistolObject.AddComponent<MagSecPistol>();
        pdShotgunObject = new GameObject();
        pdShotgunObject.AddComponent<PdShotgun>();

        equippedWeapon = Instantiate(startWeapon);
        currentWeaponInterface = startWeapon.GetComponent<IWeapon>();

        if (currentWeaponInterface == null)
        {
            Debug.LogError("Failed to get IWeapon component from the starting weapon.");
            return;
        }

        // Equip starting weapon
        EquipWeapon(startWeapon);

        // Sets correct interface
        currentWeaponInterface.OnShoot.AddListener(OnShootHandler);
        currentWeaponInterface.OnReload.AddListener(OnReloadHandler);
        currentWeaponInterface.OnEnemyHit.AddListener(OnEnemyHitHandler);
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