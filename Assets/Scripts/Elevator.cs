using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] Transform pos1, pos2;
    [SerializeField] float speed;
    Vector3 nextPos;
    public bool IsActive;
    public bool playerInRange;

    // Start is called before the first frame update
    void Start()
    {
        nextPos = pos2.position;    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!IsActive) {return;}
        if(transform.position == pos1.position)
        {
            nextPos = pos2.position;
        }    
        // if(transform.position == pos2.position)
        // {
        //     nextPos = pos1.position;
        // }
        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }

    
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(pos1.position, pos2.position);       
    }
}
