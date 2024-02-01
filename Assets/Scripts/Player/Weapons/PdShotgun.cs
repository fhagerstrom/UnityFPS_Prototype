using UnityEngine;
using UnityEngine.Events;

public class PdShotgun : BaseWeapon
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