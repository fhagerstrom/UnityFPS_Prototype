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
        // Remove UintyEvents listener from previous weapon
        if(currentWeapon != null)
        {
            currentWeapon.OnShoot.RemoveListener(OnShootHandler);
            currentWeapon.OnReload.RemoveListener(OnReloadHandler);
        }

        // Set new weapon
        currentWeapon = weapon;

        if(currentWeapon != null)
        {
            currentWeapon.OnShoot.AddListener(OnShootHandler);
            currentWeapon.OnReload.AddListener(OnReloadHandler);
        }
    }

    private void OnShootHandler()
    {
        Debug.Log("PlayerWeapon shoot event is handling!");
        // Other actions here...
    }

    private void OnReloadHandler()
    {
        Debug.Log("PlayerWeapon reload event is handling!");
        // Other actions here...
    }

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("PlayerWeapon Start is called!");
        // Start with MagSec pistol
        currentWeapon = magSecPistol.GetComponent<IWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
