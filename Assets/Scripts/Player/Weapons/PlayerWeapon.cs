using UnityEngine;
using UnityEngine.Events;
public class PlayerWeapon : MonoBehaviour
{
    public IWeapon currentWeapon;

    [SerializeField]
    private MagSecPistol magSecPistolMesh;

    [SerializeField]
    private PdShotgun pdShotgunMesh;

    [SerializeField]
    private AudioClip shootSound;
    private AudioSource audioSource;

    [SerializeField, Range(0.0f, 1.0f)]
    private float shootVolume = 1.0f;

    public void EquipWeapon(GameObject weapon)
    {
        // Remove UnityEvents listener from previous weapon
        if(currentWeapon != null)
        {
            currentWeapon.OnShoot.RemoveListener(OnShootHandler);
            currentWeapon.OnReload.RemoveListener(OnReloadHandler);
            currentWeapon.OnEnemyHit.RemoveListener(OnEnemyHitHandler);
        }

        // Instantiate "new" weapon
        GameObject newWeapon = Instantiate(weapon);
        currentWeapon = newWeapon.GetComponent<IWeapon>();

        if (currentWeapon != null)
        {
            currentWeapon.OnShoot.AddListener(OnShootHandler);
            currentWeapon.OnReload.AddListener(OnReloadHandler);
            currentWeapon.OnEnemyHit.AddListener(OnEnemyHitHandler);

            Debug.Log("Equipped weapon: " + currentWeapon.GetType().Name);
        }

        else
            Debug.LogError("Failed to equip weapon. Check if gameobject make use of IWeapon");
    }

    private void OnShootHandler()
    {
        Debug.Log("PlayerWeapon shoot event is handling!");
        // Generic handling logic here. Play sounds, UI updating...

        // Play shoot sound
        if (shootSound != null && audioSource != null)
        {
            audioSource.volume = shootVolume;
            audioSource.PlayOneShot(shootSound);
        }
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
        // Initialize AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Instantiate and equip the initial weapon (e.g., MagSecPistol)
        EquipWeapon(magSecPistolMesh.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
