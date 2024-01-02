using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFOV : MonoBehaviour
{
    public LayerMask targetMask;
    public LayerMask obstacleMask;
    public float FOVRadius;
    public float AttackDistance;
    [Range(0, 360)]
    public float FOVAngle;
    public EnemyMovement enemMove;
    public bool CanSeePlayer;

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
                        }
                    }
                }
            }
            enemMove.patrol = !CanSeePlayer;
            yield return new WaitForSeconds(.3f);
        }
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
