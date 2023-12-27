using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform Target;
    public float Speed;
    public int Damage;
    public float knockbackForce;
    public bool patrol;

    public Transform pointA;
    public Transform pointB;

    private bool movingTowardsA = true;

    // Update is called once per frame
    void Update()
    {
        if (patrol)
        {
            Patrol();
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
        }
    }

    void Patrol()
    {
        if (movingTowardsA)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointA.position, Speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, pointB.position, Speed * Time.deltaTime);
        }

        if (transform.position == pointA.position)
        {
            movingTowardsA = false;
        }
        else if (transform.position == pointB.position)
        {
            movingTowardsA = true;
        }
    }
}
