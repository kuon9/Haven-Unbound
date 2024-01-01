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

    public Transform[] waypoints;
    int waypointIndex = 0;

    private void Start()
    {
        transform.position = waypoints[waypointIndex].transform.position;
    }

    void Update()
    {
        if (patrol)
        {
            Patrol();
        }
        else
        {
            Vector3 direction = Target.transform.position - transform.position;
            direction.Normalize();

            if (direction.x >= 0.01f)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (direction.x <= -0.01f)
            {
                transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            transform.position = Vector3.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
        }
    }

    void Patrol()
    {
        Vector3 direction = waypoints[waypointIndex].transform.position - transform.position;
        direction.Normalize();

        if (direction.x >= 0.01f)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (direction.x <= -0.01f)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        transform.position = Vector3.MoveTowards(transform.position, waypoints[waypointIndex].transform.position, Speed * Time.deltaTime);

        if (transform.position == waypoints[waypointIndex].transform.position)
        {
            waypointIndex += 1;
        }

        if (waypointIndex == waypoints.Length)
        {
            waypointIndex = 0;
        }
    }
}
