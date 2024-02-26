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

    public bool isGameRunning = true; // Game is running

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI objectiveText;

    // Start is called before the first frame update
    void Start()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
            Destroy(gameObject);

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
        while (currentTime > 0 && isGameRunning)
        {
            yield return new WaitForSeconds(1f);
            currentTime -= 1f;
            UpdateUI();

            if(currentTime <= 0f)
            {
                // GAME OVER
                Debug.Log("TIME'S UP! GAME OVER!");
                isGameRunning = false;
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
            Debug.Log("MISSION COMPLETE");
            isGameRunning = false;
        }

        UpdateUI();
    }



    // Consider adding an increase function if the number of enemies should be handled dynamically e.g
    // public void IncreaseEnemiesRemaining();

}
