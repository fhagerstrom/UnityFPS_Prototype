using UnityEngine;
using UnityEngine.Events;

public class PdShotgun : BaseWeapon
{

    // Start is called before the first frame update
    void Start()
    {
        raycastRange = 15f;
        fireRate = 1.2f;

        reloadTimer = 1.0f;
        reloadTimerCooldown = reloadTimer;

        maxBullets = 9;
        maxReserveAmmo = 18;
        currentBulletsLeft = maxBullets;
        currentReserveAmmo = maxReserveAmmo;
    }

    // Update is called once per frame
    public override void Update()
    {
        Debug.DrawRay(playerCam.transform.position, playerCam.transform.forward * raycastRange, Color.green);
        base.Update();
    }

    public override void Shoot()
    {
        base.Shoot();
    }

    public override void Reload()
    {
        base.Reload();
    }

}