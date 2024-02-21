using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.Events;

// Base class weapon, holds all base info on bullets, sounds and reload logic
public class BaseWeapon : MonoBehaviour
{
    // DEFAULT WEAPON VALUES - CHANGE IN WEAPON-SPECIFIC CLASSES
    [Header("Weapon Properties")]
    [SerializeField]
    protected float raycastRange = 50f;
    protected float fireRateCooldown = 0f;
    protected float fireRate = 0.2f;

    protected int maxBullets = 9;
    protected int currentBulletsLeft = 9;
    protected int maxReserveAmmo = 81;
    protected int currentReserveAmmo = 81;
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

    private void Start()
    {
        // Initializing weapon properties in their own classes. E.g  currentBulletsLeft in MagSecPistol

        // Audio setup
        weaponAudioSource = gameObject.AddComponent<AudioSource>();
        weaponAudioSource.playOnAwake = false;
        weaponAudioSource.volume = 0.15f;

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
        // GetComponent<WeaponEvents>().OnShoot.Invoke();
        Shoot();
    }

    public void OnReload()
    {
        // GetComponent<WeaponEvents>().OnReload.Invoke();
        Reload();
    }

    public void OnEnemyHit(float damage)
    {
        Debug.Log("Incoming damage: " + damage);
        // GetComponent<WeaponEvents>().OnEnemyHit.Invoke(damage);
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
                Debug.Log("Current bullets left: " + currentBulletsLeft);

                if (currentBulletsLeft == 0)
                {
                    OnReload();
                    Debug.Log("YOU NEED MORE BOULETS! INVOKING RELOAD!");
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
                Debug.Log("Reload complete! Current bullets maxed, which is: " + currentBulletsLeft);
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
            Debug.Log("RELOADINGNGNGNGNG");

            // Check for ammo capacity
            if (currentReserveAmmo < maxBullets)
            {
                currentBulletsLeft = currentReserveAmmo;
            }

            currentReserveAmmo -= (maxBullets - currentBulletsLeft);
            Debug.Log("Reload update - Current reserve ammo: " + currentReserveAmmo);
            currentBulletsLeft = maxBullets;

            if (reloadSound != null)
            {
                weaponAudioSource.clip = reloadSound;
                weaponAudioSource.Play();
            }
        }
    }
}