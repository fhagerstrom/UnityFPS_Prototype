using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class MagSecPistol : MonoBehaviour, IWeapon
{
    [SerializeField]
    private UnityEvent onShoot = new UnityEvent();

    [SerializeField]
    private UnityEvent onReload = new UnityEvent();

    [SerializeField]
    private UnityEvent<float> onEnemyHit = new UnityEvent<float>();

    [Header("Weapon Properties")]
    [SerializeField]
    private float raycastRange = 50f;
    public float fireCooldown;
    private float currentCooldown;
    public int maxMagCapacity = 9;
    [SerializeField]
    private int currentMagCapacity;
    public int maxAmmo = 81;
    [SerializeField]
    private int currentAmmo;
    public float damage = 20f;
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

    // Start is called before the first frame update
    void Start()
    {
        currentMagCapacity = maxMagCapacity;
        currentAmmo = maxAmmo;
        maxMagCapacity = Mathf.Clamp(currentMagCapacity, 0, maxMagCapacity);
        CalcAmmoLeft();
    }

    // Update is called once per frame
    void Update()
    {
        // Debug.DrawRay to visualize the expected ray in the Scene view
        Debug.DrawRay(playerCam.transform.position, playerCam.transform.forward * raycastRange, Color.blue);

        currentCooldown -= Time.deltaTime;

        if(isReloading)
        {
            HandleReloadTimer();
        }

    }

    private void CalcAmmoLeft()
    {
        currentAmmo -= (maxMagCapacity - currentMagCapacity);
    }

    public void Shoot()
    {
        if (!isReloading && currentCooldown <= 0f)
        {
            if (currentMagCapacity > 0)
            {


                Debug.Log("Firing pistol!");

                RaycastHit hitInfo;
                bool raycastHit = Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hitInfo, raycastRange);

                if (raycastHit)
                {
                    Enemy enemy = hitInfo.collider.gameObject.GetComponent<Enemy>();

                    if (enemy != null)
                    {
                        Debug.Log("Hit enemy!");
                        // Invoke the damage event
                        OnEnemyHit.Invoke(damage);
                    }

                    // Debug.DrawRay(playerCam.transform.position, playerCam.transform.forward, Color.green, raycastRange);

                }

                // Specific weapon logic here, ammo count etc.
                OnShoot.Invoke();
                currentCooldown = fireCooldown;
                currentMagCapacity--;
            }
        }

        if (currentMagCapacity == 0)
        {
            Reload();
        }
    }

    private void HandleReloadTimer()
    {
        reloadTimeRemaining -= Time.deltaTime;
        if (reloadTimeRemaining <= 0f)
        {
            // Reload complete
            isReloading = false;
            Debug.Log("Reload Complete! Current Ammo left: " + currentAmmo);
        }
    }

    public void Reload()
    {
        if(!isReloading && currentMagCapacity < maxMagCapacity && currentAmmo > 0) 
        {
            OnReload.Invoke();
            isReloading = true;
            reloadTimeRemaining = reloadTimer;
            currentAmmo -= (maxMagCapacity - currentMagCapacity); // Deduct total ammo left
            currentMagCapacity = maxMagCapacity;
        }

        if(currentAmmo <= 0)
        {
            Debug.Log("No ammo left! Can't reload.");
        }
    }
}
