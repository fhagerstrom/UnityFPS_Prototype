using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float gameTimer = 150; // Set between 120-240 seconds
    private float currentTime;

    private int totalEnemies;
    private int enemiesRemaining;

    private float healthPoints;
    private float timePoints;
    private float ammoPoints;

    public enum GameState
    {
        RUNNING,
        PAUSED,
        WON,
        LOST
    }

    public GameState currentState;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI objectiveText;
    public TextMeshProUGUI pointsText;

    public WeaponManager weaponManager;

    public GameObject player;
    public GameObject playerUI;
    public GameObject winScreen;
    public GameObject loseScreen;
    public GameObject pauseScreen;

    // Start is called before the first frame update
    void Start()
    {
        currentState = GameState.RUNNING;
        playerUI.SetActive(true);

        if (player != null)
        {
            weaponManager = player.GetComponent<WeaponManager>();

            // Get player weapon for calculating points
            if (weaponManager == null)
            {
                Debug.Log("Cant find weaponmanager on player.");
            }
        }

        if(instance == null)
        {
            instance = this;
            // DontDestroyOnLoad(gameObject);
        }

        CountTotalEnemies();

        currentTime = gameTimer;
        enemiesRemaining = totalEnemies;

        StartCoroutine(UpdateTimer());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator UpdateTimer()
    {
        while (currentTime > 0 && currentState == GameState.RUNNING)
        {
            yield return new WaitForSeconds(1f);
            currentTime -= 1f;
            UpdateUI();

            if(currentTime <= 0f)
            {
                // GAME OVER
                Debug.Log("TIME'S UP!");
                Time.timeScale = 0;
                playerUI.SetActive(false);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                SetGameState(GameState.LOST);
                
            }
        }
    }

    public void UpdateUI()
    {
        timerText.text = "Time: " + Mathf.Floor(currentTime / 60).ToString("00") + ":" + (currentTime % 60).ToString("00");
        objectiveText.text = "Enemies remaining: " + enemiesRemaining.ToString();
    }
    
    private void CountTotalEnemies()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        totalEnemies = enemies.Length;
    }

    public void DecreaseEnemiesRemaining()
    {
        enemiesRemaining--;

        if(enemiesRemaining <= 0)
        {
            // WIN CONDITION
            healthPoints = player.GetComponent<PlayerHealth>().GetPlayerHealth();
            timePoints = currentTime;
            if (weaponManager != null)
            {
                ammoPoints = weaponManager.magSecPistolObject.GetComponent<MagSecPistol>().GetTotalAmmoLeft() + weaponManager.pdShotgunObject.GetComponent<PdShotgun>().GetTotalAmmoLeft();
            }

            float totalPoints = (healthPoints * timePoints) + ammoPoints;

            Debug.Log("MISSION COMPLETE");
            pointsText.text = "Points: " + totalPoints.ToString("0"); // 0 is used to ensure not displaying decimals
            Time.timeScale = 0;
            playerUI.SetActive(false);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SetGameState(GameState.WON);
        }

        UpdateUI();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        playerUI.SetActive(false);
        player.GetComponent<PlayerManager>().onFoot.Disable();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SetGameState(GameState.PAUSED);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        playerUI.SetActive(true);
        player.GetComponent<PlayerManager>().onFoot.Enable();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SetGameState(GameState.RUNNING);
    }

    public void ChangeGameState()
    {
        if (currentState == GameState.RUNNING)
        {
            PauseGame();
        }

        else
        {
            ResumeGame();
        }
    }

    public void SetGameState(GameState newState)
    {
        currentState = newState;

        // Set UI visibility depending on game state
        winScreen.SetActive(currentState == GameState.WON);
        loseScreen.SetActive(currentState == GameState.LOST);
        pauseScreen.SetActive(currentState == GameState.PAUSED);
    }
}
