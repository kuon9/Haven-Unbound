using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public GameObject DialogueUI;
    public string[] lines;
    public string[] endingline;
    public float textSpeed;
    public bool isTriggered;
    public bool isFinished;
    private int index;
    PlayerMovement player;

    
     //Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();  
        DialogueUI.SetActive(false);
        textComponent.text = string.Empty;
        //isTriggered = false;         
    }

    // Update is called once per frame
    void Update()
    {
        // if(textComponent.text == lines[index])
        // {
        //     NextLine();
        // }    
       if(Keyboard.current.anyKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame && !isFinished)
       {
            if(textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];

            }     
       }
    
        if(Keyboard.current.anyKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame && isFinished)
       {
            if(textComponent.text == endingline[index])
            {
                LastLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = endingline[index];

            }     
       }
    }
    public void StartDialogue()
    {
        if(!isFinished)
        {
            //dialogue is triggered.
            isTriggered = true;
            // disable playermovement when in dialogue.
            player.canMove = false;
            DialogueUI.SetActive(true);
            index = 0;
            StartCoroutine(TypeLine());      
        }
        else if(isFinished)
        {
            DialogueUI.SetActive(true);
            isTriggered = true;
            index = 0;
            StartCoroutine(TypeLastLine());
        }
    }
    
    IEnumerator TypeLine()
    {
        //Type each character 1 by 1
        foreach(char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);    
        }
    }


    IEnumerator TypeLastLine()
    {
        //Type each character 1 by 1
        foreach(char c in endingline[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);    
        }
    }



    void NextLine()
    {
        if(index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            DialogueUI.SetActive(false);
            isTriggered = false;
            isFinished = true;
            // this resets current string back to empty, allowing us to restart with same beginning dialogue.
            textComponent.text = string.Empty;
            // player can move after dialogue is over
            player.canMove = true;
        }
    }

    void LastLine()
    {
        if(index < endingline.Length -1)
        {
            StartCoroutine(TypeLastLine());
        }
        else
        {
            DialogueUI.SetActive(false);
            textComponent.text = string.Empty;
            isTriggered = false;
        }
    }
}
