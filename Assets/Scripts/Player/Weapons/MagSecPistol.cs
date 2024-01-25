using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class MagSecPistol : MonoBehaviour, IWeapon
{
    [SerializeField]
    private UnityEvent onShoot = new UnityEvent();

    [SerializeField]
    private UnityEvent onReload = new UnityEvent();

    [SerializeField]
    private UnityEvent<float> onEnemyHit = new UnityEvent<float>();

    public float fireCooldown; // Set in inspector
    private float currentCooldown;

    public float rayCastRange = 100f;

    // Use player camera
    public Camera playerCam;

    public UnityEvent OnShoot
    {
        get { return onShoot; }
    }

    public UnityEvent OnReload
    {
        get { return onReload; }
    }

    public UnityEvent<float> OnEnemyHit
    {
        get { return onEnemyHit; }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Debug.DrawRay to visualize the expected ray in the Scene view
        Debug.DrawRay(playerCam.transform.position, playerCam.transform.forward * rayCastRange, Color.blue);

        currentCooldown -= Time.deltaTime;
    }

    public void Shoot()
    {
        if (currentCooldown <= 0f)
        {
            Debug.Log("Firing pistol!");

            RaycastHit hitInfo;
            bool raycastHit = Physics.Raycast(playerCam.transform.position, playerCam.transform.forward, out hitInfo, rayCastRange);

            if (raycastHit)
            {
                Enemy enemy = hitInfo.collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    Debug.Log("Hit enemy!");
                    // Invoke the damage event
                    OnEnemyHit.Invoke(20); // Adjust the damage value as needed
                }

                Debug.DrawRay(playerCam.transform.position, playerCam.transform.forward, Color.green, rayCastRange);
            }
            else
            {
                Debug.DrawRay(playerCam.transform.position, playerCam.transform.forward, Color.red, rayCastRange);
            }

            OnShoot.Invoke();
            currentCooldown = fireCooldown;

            // Specific weapon logic here, ammo count etc.
        }
    }

    public void Reload()
    {
        // Specific weapon logic here, ammo reset, reload animation etc.
        Debug.Log("Reloading pistol!");

        OnReload.Invoke();
    }
}
