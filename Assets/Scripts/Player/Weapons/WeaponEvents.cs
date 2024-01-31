using UnityEngine;
using UnityEngine.Events;

public class WeaponEvents : MonoBehaviour
{
    public UnityEvent OnShoot;
    public UnityEvent OnReload;
    public UnityEvent<float> OnEnemyHit;
}
