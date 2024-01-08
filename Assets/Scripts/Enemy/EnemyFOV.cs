using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFOV : MonoBehaviour
{
    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public float FOVRadius;
    [Range(0, 360)]
    public float FOVAngle;
    public EnemyMovement enemMove;
    public bool CanSeePlayer;

    private GameObject player;
    public int damage;

    private bool canHit = true;
    public float AttackDistance;

    private void Start()
    {
        StartCoroutine(FindVisibleTargets());
        enemMove = GetComponentInParent<EnemyMovement>();
    }

    private IEnumerator FindVisibleTargets()
    {
        while (true)
        {
            CanSeePlayer = false;
            Collider2D[] targetsInViewRadius = Physics2D.OverlapCircleAll(transform.position, FOVRadius, targetMask);
            for (int i = 0; i < targetsInViewRadius.Length; i++)
            {
                Transform target = targetsInViewRadius[i].transform;

                Vector2 dirToTarget = (target.position - transform.position).normalized;
                if (Vector3.Angle(transform.up, dirToTarget) < FOVAngle / 2)
                {
                    float disToTarget = Vector2.Distance(transform.position, target.position);
                    if (!Physics2D.Raycast(transform.position, dirToTarget, disToTarget, obstacleMask))
                    {
                        if (target.tag == "Player")
                        {
                            CanSeePlayer = true;
                            player = target.gameObject;
                            FindDistance(target);
                        }
                    }
                }
            }
            enemMove.patrol = !CanSeePlayer;
            yield return new WaitForSeconds(.3f);
        }
    }
    void FindDistance(Transform target)
    {
        Vector3 direction = target.position - transform.position;
        float distance = direction.magnitude;
        if (distance <= AttackDistance && CanSeePlayer && canHit)
        {
            Attack();
        }
    }
    void Attack()
    {
        player.GetComponent<PlayerHealth>().TakeDamage(damage);
        StartCoroutine(AttackCoolDown());
    }

    private IEnumerator AttackCoolDown()
    {
        canHit= false;
        yield return new WaitForSeconds(1f);
        canHit = true;
    }

    public Vector2 DirFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees -= transform.eulerAngles.z;
        }
        return new Vector2(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
