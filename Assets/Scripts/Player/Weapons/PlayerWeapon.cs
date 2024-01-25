using UnityEngine;
using UnityEngine.Events;
public class PlayerWeapon : MonoBehaviour
{
    [SerializeField]
    public IWeapon currentWeapon;

    [SerializeField]
    private GameObject magSecPistol;

    public void EquipWeapon(IWeapon weapon)
    {
        // Remove UnityEvents listener from previous weapon
        if(currentWeapon != null)
        {
            currentWeapon.OnShoot.RemoveListener(OnShootHandler);
            currentWeapon.OnReload.RemoveListener(OnReloadHandler);
            currentWeapon.OnEnemyHit.RemoveListener(OnEnemyHitHandler);
        }

        // Set new weapon
        currentWeapon = weapon;
        Debug.Log("Equipped weapon: " + currentWeapon);

        if (currentWeapon != null)
        {
            currentWeapon.OnShoot.AddListener(OnShootHandler);
            currentWeapon.OnReload.AddListener(OnReloadHandler);
            currentWeapon.OnEnemyHit.AddListener(OnEnemyHitHandler);
        }
    }

    private void OnShootHandler()
    {
        Debug.Log("PlayerWeapon shoot event is handling!");
        // Generic handling logic here. Play sounds, UI updating...
    }

    private void OnReloadHandler()
    {
        Debug.Log("PlayerWeapon reload event is handling!");
        // Generic handling logic here. Play sounds, UI updating...
    }

    private void OnEnemyHitHandler(float damage)
    {
        Debug.Log("PlayerWeapon enemy hit event is handling!");
        // Generic handling logic here. Play sounds, UI updating...
    }

    // Start is called before the first frame update
    void Start()
    {
        // Start with MagSec pistol
        EquipWeapon(magSecPistol.GetComponent<IWeapon>());


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
