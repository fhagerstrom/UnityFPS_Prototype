using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    private StateMachine stateMachine;
    private NavMeshAgent agent;

    public NavMeshAgent Agent { get => agent; }

    // Debugging
    [SerializeField]
    private string currentState;
    public Pathing path;
    private GameObject player;
    public float sightDistance = 20f;
    public float enemyFov = 85f;
    public float eyeHeight;

    // Start is called before the first frame update
    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        stateMachine.Init�alize();
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        PlayerInSight();
        currentState = stateMachine.activeState.ToString();
    }

    public bool PlayerInSight()
    {
        if(player != null) 
        {
            // Is player close enough to be seen
            if(Vector3.Distance(transform.position, player.transform.position) < sightDistance) 
            {
                Vector3 targetDirection = player.transform.position - transform.position - (Vector3.up * eyeHeight);
                float angleToPlayer = Vector3.Angle(targetDirection, transform.forward);

                // Check if player is within enemy field of view
                if(angleToPlayer >= -enemyFov && angleToPlayer <= enemyFov) 
                { 
                    Ray ray = new Ray(transform.position + (Vector3.up * eyeHeight), targetDirection);
                    RaycastHit hitInfo = new RaycastHit();

                    if(Physics.Raycast(ray, out hitInfo, sightDistance))
                    {
                        if(hitInfo.transform.gameObject == player)
                        {
                            Debug.DrawRay(ray.origin, ray.direction * sightDistance);
                            return true;
                        }
                    }
                }
            }
        }
        return false;
    }
}
