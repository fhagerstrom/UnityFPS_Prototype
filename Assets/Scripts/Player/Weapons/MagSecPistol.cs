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
    public override void Update()
    {

        Debug.DrawRay(playerCam.transform.position, playerCam.transform.forward * raycastRange, Color.blue);
        base.Update();
    }

    public override void Shoot()
    {
        Debug.Log("Firing MagSec Pistol.");
        base.Shoot();
    }

    
}
