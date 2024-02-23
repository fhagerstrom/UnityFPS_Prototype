using UnityEngine;

// Base class weapon, holds all base info on bullets, sounds and reload logic
public class BaseWeapon : MonoBehaviour
{
    // DEFAULT WEAPON VALUES - CHANGE IN WEAPON-SPECIFIC CLASSES
    [Header("Weapon Properties")]
    [SerializeField]
    protected float raycastRange = 50f;
    protected float fireRateCooldown = 0f;
    protected float fireRate = 0.2f;

    public int maxBullets = 9;
    public int currentBulletsLeft = 9;
    public int maxReserveAmmo = 81;
    public int currentReserveAmmo = 81;
    protected float damage = 20f;

    [SerializeField]
    protected float reloadTimer;
    protected float reloadTimerCooldown = 1.5f;

    [SerializeField]
    protected float inaccuracyAngle = 5.0f;

    public Camera playerCam;
    public bool canShoot = true;
    public bool startReloadTimer = false;

    protected Enemy enemy;

    [Header("Audio")]
    protected AudioClip shootSound;
    protected AudioClip reloadSound;

    protected AudioSource weaponAudioSource;

    public PlayerAmmo playerAmmo;

    private void Start()
    {
        // Initializing weapon properties in their own classes. E.g  currentBulletsLeft in MagSecPistol

        // Audio setup
        weaponAudioSource = gameObject.AddComponent<AudioSource>();
        weaponAudioSource.playOnAwake = false;
        weaponAudioSource.volume = 0.15f;

        playerAmmo = GetComponent<PlayerAmmo>();

    }

    public virtual void Update()
    {
        if (fireRateCooldown > 0)
        {
            fireRateCooldown -= Time.deltaTime;
        }
    }

    public void OnShoot()
    {
        Shoot();
    }

    public void OnReload()
    {
        Reload();
    }

    public void OnEnemyHit(float damage)
    {
        enemy.TakeDamage(damage);
    }

    public virtual void Shoot()
    {
        if (canShoot && fireRateCooldown <= 0)
        {
            // Debug.Log("Firing weapon!");
            if (currentBulletsLeft > 0)
            {
                // Testing random inaccuracy
                Vector3 shotDirection = playerCam.transform.forward;
                shotDirection = Quaternion.Euler(Random.Range(-inaccuracyAngle, inaccuracyAngle), Random.Range(-inaccuracyAngle, inaccuracyAngle), 0) * shotDirection;
                RaycastHit hitInfo;
                bool raycastHit = Physics.Raycast(playerCam.transform.position, shotDirection, out hitInfo, raycastRange);

                if (raycastHit)
                {
                    enemy = hitInfo.collider.gameObject.GetComponent<Enemy>();

                    if (enemy != null)
                    {
                        Debug.Log("Hit enemy!");
                        // Apply damage
                        OnEnemyHit(damage);

                        if(enemy.GetEnemyHealth() <= 0)
                            hitInfo.collider.gameObject.GetComponent<Animation>().Play();
                    }

                    // Debug.DrawRay(playerCam.transform.position, playerCam.transform.forward, Color.green, raycastRange);

                }

                // Sounds
                if (shootSound != null)
                {
                    weaponAudioSource.clip = shootSound;
                    weaponAudioSource.Play();
                    weaponAudioSource.volume = 0.1f;
                }

                currentBulletsLeft--;
                fireRateCooldown = fireRate;

                if (currentBulletsLeft == 0)
                {
                    OnReload();
                }
            }
        }
    }

    public void HandleReloadTimer()
    {
        if (startReloadTimer == true)
        {
            reloadTimer -= Time.deltaTime;
            if (reloadTimer <= 0)
            { 
                canShoot = true;
                startReloadTimer = false;
                reloadTimer = reloadTimerCooldown;
            }
        }
    }

    public virtual void Reload()
    {
        // Only enter Reload function if we have reserve ammo left and the weapon has shot at least once
        if (currentReserveAmmo > 0 && (currentBulletsLeft < maxBullets))
        {
            canShoot = false;
            startReloadTimer = true;

            // Top up bullets
            int bulletsToReload = Mathf.Min(maxBullets - currentBulletsLeft, currentReserveAmmo);

            // Update bullets and reserve ammo
            currentBulletsLeft += bulletsToReload;
            currentReserveAmmo -= bulletsToReload;

            if (reloadSound != null)
            {
                weaponAudioSource.clip = reloadSound;
                weaponAudioSource.Play();
            }
        }
    }
}