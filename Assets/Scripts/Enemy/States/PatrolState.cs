using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    // Which waypoint is targeted
    public int waypointIndex;
    public float waitTimer;

    public override void Enter()
    {
        
    }

    public override void Perform()
    {
        Patrolling();

        if(enemy.PlayerInSight())
        {
            stateMachine.ChangeState(new AttackState());
        }
    }

    public override void Exit() 
    {
        
    }

    public void Patrolling()
    {
        // Patrol logic
        if(enemy.Agent.remainingDistance < 0.2f)
        {
            waitTimer += Time.deltaTime;
            if (waitTimer > 2)
            {
                if (waypointIndex < enemy.path.waypoints.Count - 1)
                {
                    waypointIndex++;
                }

                else
                    waypointIndex = 0;

                enemy.Agent.SetDestination(enemy.path.waypoints[waypointIndex].position);
                waitTimer = 0;
            }
        }
    }
}
