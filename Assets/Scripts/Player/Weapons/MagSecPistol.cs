using UnityEngine;
using UnityEngine.Events;

public class MagSecPistol : MonoBehaviour, IWeapon
{
    [SerializeField]
    private UnityEvent onShoot = new UnityEvent();
    public UnityEvent OnShoot { get => onShoot; set => onShoot = value; }

    [SerializeField]
    private UnityEvent onReload = new UnityEvent();
    public UnityEvent OnReload { get => onReload; set => onReload = value; }

    public float fireCooldown;
    private float currentCooldown;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        Debug.Log("Firing pistol!");

        // Logic

        OnShoot.Invoke();
    }

    public void Reload()
    {
        Debug.Log("Reloading pistol!");

        // Logic

        OnReload.Invoke();
    }
}
