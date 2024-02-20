using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : BaseState
{
    GameObject agentObject;
    private float destructionTimer = 1.25f; // Adjust timer to anmimation

    public override void Enter()
    {
        agentObject = GameObject.FindGameObjectWithTag("Agent");

        if (agentObject != null)
            agentObject.GetComponent<Animator>().SetTrigger("DeathTrigger");

        // Start destruction timer
        destructionTimer = 1.25f;
    }

    public override void Exit()
    {
        
    }

    public override void Perform()
    {
        // Update the destruction timer
        destructionTimer -= Time.deltaTime;

        if (destructionTimer <= 0)
        {
            if (enemy != null)
                // Set enemy gameobject to inactive
                enemy.gameObject.SetActive(false);

            else
                Debug.LogError("Enemy is null in DeathState");
        }
    }
}
