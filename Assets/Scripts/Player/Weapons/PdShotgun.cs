using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PdShotgun : MonoBehaviour, IWeapon
{
    [SerializeField]
    private UnityEvent onShoot = new UnityEvent();

    [SerializeField]
    private UnityEvent onReload = new UnityEvent();

    [SerializeField]
    private UnityEvent<float> onEnemyHit = new UnityEvent<float>();

    [Header("Weapon Properties")]
    [SerializeField]
    private float raycastRange = 15f;
    public float fireRate = 1.25f;
    private float cooldown;
    public int maxShellsCapacity = 9;
    [SerializeField]
    private int currentShellsCapacity;
    public int maxAmmo = 45;
    [SerializeField]
    private int currentAmmo;
    public float damage = 35f;
    [SerializeField]
    private float reloadTimer = 2.2f;
    private bool isReloading = false;
    private float reloadTimeRemaining = 0f;

    // Use player camera for the raycasting
    public Camera playerCam;

    public UnityEvent OnShoot
    {
        get { return onShoot; }
    }

    public UnityEvent OnReload
    {
        get { return onReload; }
    }

    public UnityEvent<float> OnEnemyHit
    {
        get { return onEnemyHit; }
    }

    public GameObject GetWeaponObject()
    {
        return gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentShellsCapacity = maxShellsCapacity;
        currentAmmo = maxAmmo;
        maxShellsCapacity = Mathf.Clamp(currentShellsCapacity, 0, maxShellsCapacity);
        // CalcAmmoLeft();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {

    }

    public void Reload()
    {

    }

}
