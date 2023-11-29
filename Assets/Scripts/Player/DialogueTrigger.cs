using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    public bool playerinRange;
    public GameObject popUpText;
    Dialogue dialogue;
    
    // Start is called before the first frame update
    void Start()
    {
        // Calling a function another gameobject. I should tag for the gameobject and the getcomponent is the script
        dialogue = GameObject.FindGameObjectWithTag("Dialogue").GetComponent<Dialogue>();  
    }

    // Update is called once per frame
    void Update()
    {
        if(!playerinRange) {return;}
        {
            BeginDialogue();    
        }    
    }

    void BeginDialogue()
    {
        if(playerinRange && Keyboard.current.fKey.wasPressedThisFrame && !dialogue.isTriggered)
        {       
            Debug.Log("Dialogue Starting");
            dialogue.StartDialogue();
            dialogue.isTriggered = true;
            popUpText.SetActive(false);    
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" && !dialogue.isTriggered)
        {
            Debug.Log("In Range");
            popUpText.SetActive(true);
            playerinRange = true;    
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            popUpText.SetActive(false);
            playerinRange = false;
            
        }
    }
}
