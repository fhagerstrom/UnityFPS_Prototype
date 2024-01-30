using UnityEngine;
using UnityEngine.Events;


// Base class weapon, holds all base info on bullets, sounds and reload logic
public class BaseWeapon : MonoBehaviour, IWeapon
{
    public IWeapon currentWeapon;

    [SerializeField]
    protected UnityEvent onShoot = new UnityEvent();

    [SerializeField]
    protected UnityEvent onReload = new UnityEvent();

    [SerializeField]
    protected UnityEvent<float> onEnemyHit = new UnityEvent<float>();

    [SerializeField]
    protected AudioClip shootSound;
    protected AudioSource audioSource;

    [SerializeField, Range(0.0f, 1.0f)]
    protected float shootVolume = 0.1f;

    [Header("Weapon Properties")]
    [SerializeField]
    protected float raycastRange = 50f;
    public float fireRate;

    protected int maxBullets = 9;
    protected int currentBulletsLeft = 9;
    public int maxReserveAmmo = 81;
    public int currentReserveAmmo = 81;
    public float damage = 20f;

    [SerializeField]
    protected float reloadTimer = 2.2f;

    protected float reloadTimeRemaining = 0f;

    [SerializeField]
    protected float inaccuracyAngle;

    public Camera playerCam;
    public bool isReloading = false;

    protected float fireRateCooldown = 0f;
    // End base values

    // Start is called before the first frame update
    void Start()
    {
        // Initialize AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Ensure player camera is assigned
        if (playerCam == null)
        {
            Debug.LogError("Player camera not assigned to BaseWeapon.");
            return;
        }

    }

    // Update is called once per frame
    public virtual void Update()
    {
        Debug.Log("Im UPD");
        Reload();
        fireRateCooldown += Time.deltaTime;

     
    }
    public virtual void Shoot()
    {
        Debug.Log("Firing weapon!");

        // Testing random inaccuracy
        Vector3 shotDirection = playerCam.transform.forward;
        shotDirection = Quaternion.Euler(Random.Range(-inaccuracyAngle, inaccuracyAngle), Random.Range(-inaccuracyAngle, inaccuracyAngle), 0) * shotDirection;

        RaycastHit hitInfo;
        bool raycastHit = Physics.Raycast(playerCam.transform.position, shotDirection, out hitInfo, raycastRange);

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
        currentBulletsLeft--;
        fireRateCooldown = 0;
        Debug.Log(currentBulletsLeft);
    }
    
    public virtual void Reload()
    {
        // Check if conditions allow for reloading
        if (currentReserveAmmo > 0 && isReloading)
        {
            Debug.Log("RELOADINGNGNGNGNG");
            // Auto reload if empty
            if (currentBulletsLeft <= 0)
                isReloading = true;

            // If reloading, wait for reload timer to finish
            if (isReloading)
            {
                reloadTimeRemaining += Time.deltaTime;

                // If reload timer is complete, update values and end reload
                if (reloadTimeRemaining >= reloadTimer)
                {
                    // Check for ammo capacity
                    if (currentReserveAmmo < maxBullets)
                    {
                        currentBulletsLeft = currentReserveAmmo;
                    }

                    currentBulletsLeft = maxBullets;
                    Debug.Log("Reload Update, bullets: " + currentBulletsLeft);

                    // Reset reload variables
                    reloadTimeRemaining = 0;
                    isReloading = false;
                }
            }
        }
    }
    void OnShootEvent()
    {
        isReloading = true;
    }
    public UnityEvent OnShoot
    {
        get { return onShoot; }
    }

    public UnityEvent OnReload
    {
        get {return onReload; }
    }

    public UnityEvent<float> OnEnemyHit
    {
        get { return onEnemyHit; }
    }
}
