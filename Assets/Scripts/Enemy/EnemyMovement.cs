using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform Target;
    public float Speed;
    public int Damage;
    public float knockbackForce;

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
    }
}
