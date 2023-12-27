using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    private EnemyMovement enemMove;

    private void Awake()
    {
        currentHealth = maxHealth;
        enemMove = GetComponent<EnemyMovement>();
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        StartCoroutine(StopMove());
        if (currentHealth <= 0)
        {
            Destroy(gameObject);
        }        
    }

    IEnumerator StopMove()
    {
        enemMove.enabled = false;
        yield return new WaitForSeconds(1);
        enemMove.enabled = true;
    }
}
