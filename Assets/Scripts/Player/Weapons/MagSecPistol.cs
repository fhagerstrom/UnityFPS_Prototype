using UnityEngine;
using UnityEngine.Events;

public class MagSecPistol : BaseWeapon
{
    // Start is called before the first frame update
    void Start()
    {
        currentBulletsLeft = maxBullets;
        currentReserveAmmo = maxReserveAmmo;
    }

    // Update is called once per frame
    public override void Update()
    {
        Debug.DrawRay(playerCam.transform.position, playerCam.transform.forward * raycastRange, Color.blue);
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