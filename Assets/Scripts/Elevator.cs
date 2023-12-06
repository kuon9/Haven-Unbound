using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Elevator : MonoBehaviour
{
    [SerializeField] Transform pos1, pos2;
    [SerializeField] float speed;
    [SerializeField] GameObject popUpText;
    Vector3 nextPos;
    public bool IsActive;
    public bool playerInRange;
    PlayerMovement player;

    // Start is called before the first frame update
    void Start()
    {
        nextPos = pos2.position;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!IsActive) {return;}
        player.canMove = false;
        if(transform.position == pos1.position)
        {
            nextPos = pos2.position;
        }    
        if(transform.position == pos2.position)
        {
            //player can move
            player.canMove = true;
        }
        // if(transform.position == pos2.position)
        // {
        //     nextPos = pos1.position;
        // }
        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }

    void Update()
    {
        BeginElevator();
    }

    void BeginElevator()
    {
        if(Keyboard.current.fKey.wasPressedThisFrame && playerInRange)
        {
            IsActive = true;
            popUpText.SetActive(false);
        }
    }

    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Elevator can work");
            popUpText.SetActive(true);
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            popUpText.SetActive(false);
            playerInRange = false;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(pos1.position, pos2.position);       
    }
}
