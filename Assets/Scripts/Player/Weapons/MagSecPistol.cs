using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class MagSecPistol : BaseWeapon, IWeapon
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Shoot()
    {
        Debug.Log("Firing MagSec Pistol.");
        base.Shoot();
    }

    public override void Reload()
    {
        base.Reload();
    }
}
