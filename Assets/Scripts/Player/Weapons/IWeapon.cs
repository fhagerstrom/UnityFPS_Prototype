using UnityEngine;
using UnityEngine.Events;

public interface IWeapon
{
    UnityEvent OnShoot { get; set; }
    UnityEvent OnReload { get; set; }
    void Shoot();
    void Reload();
}
