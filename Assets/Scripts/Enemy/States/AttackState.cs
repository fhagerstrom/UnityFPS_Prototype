using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private float moveTimer;
    private float losePlayerTimer;
    private float shotTimer;
    private float returnBulletTimer;
    public float bulletVelocity = 50f;

    public GameObject bulletPrefab;
    private GameObject bullet;

    public override void Enter()
    {
        
    }

    public override void Exit()
    {
        
    }

    public override void Perform()
    {
        if(enemy.PlayerInSight())
        {
            // Lock lose timer, increment other timers
            losePlayerTimer = 0;
            moveTimer += Time.deltaTime;
            shotTimer += Time.deltaTime;
            enemy.transform.LookAt(enemy.Player.transform);

            if(shotTimer > enemy.fireRate)
            {
                Shoot();
            }

            if(moveTimer > Random.Range(3, 7))
            {
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 5));
                moveTimer = 0;
            }

            // Check the returnBulletTimer
            if (returnBulletTimer > 0)
            {
                returnBulletTimer -= Time.deltaTime;

                if (returnBulletTimer <= 0)
                {
                    ReturnBulletToPool();
                }
            }
        }

        else
        {
            losePlayerTimer += Time.deltaTime;

            if(losePlayerTimer > 8) 
            {
                // Player lost, enter search state
                stateMachine.ChangeState(new PatrolState());
            }
        }
    }

    public void Shoot()
    {
        // Get bullet from object pool
        bullet = ObjectPoolManager.instance.GetBullet();

        // *PROJECTILE-BASED SHOOTING*

        // Store ref to gun barrel
        Transform gunBarrel = enemy.gunBarrel;

        bullet.transform.position = gunBarrel.position;
        bullet.transform.rotation = gunBarrel.rotation;

        // Calc direction to player
        Vector3 shootDirection = (enemy.Player.transform.position - gunBarrel.transform.position).normalized;
        // Add force to rigidbody component.
        // Random is used to add "spread" / inaccuracy to the shooting
        bullet.GetComponent<Rigidbody>().velocity = Quaternion.AngleAxis(Random.Range(-3f, 3f), Vector3.up) * shootDirection * bulletVelocity;

        Debug.Log("AI Shooting!");

        shotTimer = 0;
        returnBulletTimer = 1.0f;

    }

    private void ReturnBulletToPool()
    {
        // Return the bullet to the object pool
        bullet.GetComponent<Bullet>().ReturnToPool();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
