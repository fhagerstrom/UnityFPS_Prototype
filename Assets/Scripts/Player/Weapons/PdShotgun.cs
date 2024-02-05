using UnityEngine;
using UnityEngine.Events;

public class PdShotgun : BaseWeapon
{
    private Vector3[] blastCone = new Vector3[]
    {
        new Vector3(-10f, -10f, 0),
        new Vector3(-5f, -5f, 0),
        new Vector3(0, 0, 0),
        new Vector3(5f, 5f, 0),
        new Vector3(10f, 10f, 0),
    };

    // Awake is called before the first frame update
    void Awake()
    {
        raycastRange = 15f;
        fireRate = 1.1f;

        reloadTimer = 1.0f;
        reloadTimerCooldown = reloadTimer;

        maxBullets = 9;
        maxReserveAmmo = 18;
        currentBulletsLeft = maxBullets;
        currentReserveAmmo = maxReserveAmmo;
        damage = 25f;
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
                        // Apply damage
                        totalDamage += damage;
                        //
                    }

                    // Debug.DrawRay(playerCam.transform.position, shotDirection, Color.green, raycastRange);
                }
            }

            if(totalDamage > 0f)
            {
                OnEnemyHit(totalDamage);
            }

            currentBulletsLeft--;
            fireRateCooldown = fireRate;
            Debug.Log("Current bullets left: " + currentBulletsLeft);

            if (currentBulletsLeft == 0)
            {
                OnReload();
                Debug.Log("YOU NEED MORE BULLETS! INVOKING RELOAD!");
            }
        }
    }

    public override void Reload()
    {
        base.Reload();
    }

}