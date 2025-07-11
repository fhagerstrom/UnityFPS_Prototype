using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Transform hitTransform = collision.transform;
        if(hitTransform.CompareTag("Player"))
        {
            Debug.Log("Hit Player!");
            hitTransform.GetComponent<PlayerHealth>().TakeDamage(15);
        }

        // Regardless of collision type, return bullet to object pool
        ReturnToPool();
    }

    public void ReturnToPool()
    {
        // Deactivate bullet and return to object pool
        gameObject.SetActive(false);
    }
}
