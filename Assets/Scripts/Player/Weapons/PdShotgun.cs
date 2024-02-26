using UnityEngine;
using UnityEngine.Events;

public class PdShotgun : BaseWeapon
{
    [Header("Audio")]
    [SerializeField]
    private AudioClip shotgunShootSfx;
    [SerializeField]
    private AudioClip shotgunReloadSfx;
    [SerializeField]
    private AudioClip shotgunClickSfx;

    // Define a blast cone by shooting x amount of rays from camera when shotgun is used
    private Vector3[] blastCone = new Vector3[]
    {
        new Vector3(-5f, -5f, 0),
        new Vector3(-2.5f, -2.5f, 0),
        new Vector3(0, 0, 0),
        new Vector3(2.5f, 2.5f, 0),
        new Vector3(5f, 5f, 0),
    };

    // Awake is called before the first frame update
    void Awake()
    {
        raycastRange = 15f;
        fireRate = 1.2f;

        reloadTimerCooldown = 1.8f;
        reloadTimer = reloadTimerCooldown;

        maxBullets = 9;
        maxReserveAmmo = 9;
        currentBulletsLeft = maxBullets;
        currentReserveAmmo = maxReserveAmmo;
        damage = 25f;

        shootSound = shotgunShootSfx;
        reloadSound = shotgunReloadSfx;
        gunClickSound = shotgunClickSfx;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        foreach (var spreadAngles in blastCone)
        {
            // Calculate shot direction with spread
            Vector3 shotDirection = playerCam.transform.forward;
            shotDirection = Quaternion.Euler(spreadAngles) * shotDirection;

            // Debug draw the constant blast cone
            Debug.DrawRay(playerCam.transform.position, shotDirection * raycastRange, Color.red);
        }
    }

    public override void Shoot()
    {
        if (canShoot && fireRateCooldown <= 0)
        {
            if (currentBulletsLeft > 0)
            {
                float totalDamage = 0f;
                foreach (var spreadAngles in blastCone)
                {
                    Vector3 shotDirection = playerCam.transform.forward;
                    shotDirection = Quaternion.Euler(spreadAngles) * shotDirection;

                    RaycastHit hitInfo;
                    bool raycastHit = Physics.Raycast(playerCam.transform.position, shotDirection, out hitInfo, raycastRange);

                    if (raycastHit)
                    {
                        enemy = hitInfo.collider.gameObject.GetComponent<Enemy>();

                        if (enemy != null)
                        {
                            Debug.Log("Hit enemy!");
                            // Add damage based on how many rays hit
                            totalDamage += damage;
                            // Apply damage
                            OnEnemyHit(totalDamage);

                            if (enemy.GetEnemyHealth() <= 0)
                            {
                                hitInfo.collider.gameObject.GetComponent<Animation>().Play();
                            }
                        }
                    }
                }

                // Sounds
                if (shootSound != null)
                {
                    weaponAudioSource.clip = shootSound;
                    weaponAudioSource.Play();
                }

                currentBulletsLeft--;
                fireRateCooldown = fireRate;

                if (currentBulletsLeft == 0)
                {
                    OnReload();
                }
            }

            // No ammo left at all
            else
            {
                weaponAudioSource.clip = gunClickSound;
                weaponAudioSource.Play();
            }
        }
    }

    public override void Reload()
    {
        base.Reload();
    }

}