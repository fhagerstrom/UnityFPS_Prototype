using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchState : BaseState
{
    private float searchTimer;
    private float moveTimer;

    public override void Enter()
    {
        // Head to last known position
        enemy.Agent.SetDestination(enemy.LastKnownPos);
    }
    
    public override void Perform()
    {
        // Change state to attacking if player is seen
        if (enemy.PlayerInSight())
            stateMachine.ChangeState(new AttackState());

        // If the enemy has arrived at last known position, wait, go back to patrolstate
        if(enemy.Agent.remainingDistance < enemy.Agent.stoppingDistance) 
        {
            searchTimer += Time.deltaTime;
            moveTimer += Time.deltaTime;

            if (moveTimer > Random.Range(3, 5))
            {
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 10));
                moveTimer = 0;
            }

            if (searchTimer > Random.Range(3, 8))
            {
                stateMachine.ChangeState(new PatrolState());
            }

        }
    }  
    
    public override void Exit()
    {
        
    }
}
