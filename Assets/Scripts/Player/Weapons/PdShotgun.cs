using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PdShotgun : BaseWeapon
{

    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    public override void Shoot()
    {
        Debug.Log("Firing Shotgun.");
        base.Shoot();
    }

    public override void Reload()
    {
        base.Reload();
    }

}