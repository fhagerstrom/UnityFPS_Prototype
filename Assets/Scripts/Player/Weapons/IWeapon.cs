using UnityEngine;
using UnityEngine.Events;

public interface IWeapon
{
    UnityEvent OnShoot { get; }
    UnityEvent OnReload { get; }
    UnityEvent<float> OnEnemyHit { get; }
    void Shoot();
    void Reload();

}
