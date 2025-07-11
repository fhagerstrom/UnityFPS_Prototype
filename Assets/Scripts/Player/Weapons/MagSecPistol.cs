using UnityEngine;
using UnityEngine.Events;

public class MagSecPistol : BaseWeapon
{
    [Header("Audio")]
    [SerializeField]
    private AudioClip magSecShootSfx;
    [SerializeField]
    private AudioClip magSecReloadSfx;
    [SerializeField]
    private AudioClip magSecClickSfx;

    // Awake is called before the first frame update
    void Awake()
    {
        currentBulletsLeft = maxBullets;
        currentReserveAmmo = maxReserveAmmo;

        // Assign audio clips
        shootSound = magSecShootSfx;
        reloadSound = magSecReloadSfx;
        gunClickSound = magSecClickSfx;
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

    public int GetTotalAmmoLeft()
    {
        return currentBulletsLeft + currentReserveAmmo;
    }
}