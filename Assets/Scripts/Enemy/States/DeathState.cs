using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : BaseState
{
    private float destructionTimer; // Adjust timer to anmimation

    public override void Enter()
    {
        // Set and start destruction timer
        destructionTimer = 0.75f;
    }

    public override void Exit()
    {
        
    }

    public override void Perform()
    {
        // Update destruction timer
        destructionTimer -= Time.deltaTime;

        if (destructionTimer <= 0)
        {
            if (enemy != null)
            {
                // Set enemy gameobject to inactive
                enemy.gameObject.SetActive(false);
                GameManager.instance.DecreaseEnemiesRemaining();
            }

            else
                Debug.LogError("Enemy is null in DeathState");
        }
    }
}
