using UnityEngine;
using UnityEngine.Events;

// Base class weapon, holds all base info on bullets, sounds and reload logic
public class BaseWeapon : MonoBehaviour
{
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

    private Enemy enemy;

    private void Start()
    {
        
    }

    public virtual void Update()
    {
        Reload();
    }

    public void OnShoot()
    {
        GetComponent<WeaponEvents>().OnShoot.Invoke();
        Shoot();
    }

    public void OnReload()
    {
        GetComponent<WeaponEvents>().OnReload.Invoke();
        Reload();
    }

    public void OnEnemyHit(float damage)
    {
        GetComponent<WeaponEvents>().OnEnemyHit.Invoke(damage);
    }

    public virtual void Shoot()
    {
        Debug.Log("Firing weapon!");

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

            currentBulletsLeft--;
            fireRateCooldown = 0;
            Debug.Log(currentBulletsLeft);
        }

        else
            Debug.Log("YOU NEED MORE BOULETS!");
    }

    public virtual void Reload()
    {
        // Check if conditions allow for reloading
        if (!isReloading || currentReserveAmmo <= 0)
        {
            return;
        }

        Debug.Log("RELOADINGNGNGNGNG");

        // Auto reload if empty
        if (currentBulletsLeft <= 0)
        {
            isReloading = true;
        }

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