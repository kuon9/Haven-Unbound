using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    public TextMeshProUGUI textComponent;
    public GameObject DialogueUI;
    public string[] lines;
    public float textSpeed;
    public static bool isTriggered;

    private int index;

    
     //Start is called before the first frame update
    void Start()
    {
        //DialogueUI.SetActive(false);
        // if (!isTriggered) {return;}
        // DialogueUI.SetActive(true);
        textComponent.text = string.Empty;
        StartDialogue();            
    }

    // Update is called once per frame
    void Update()
    {
        if(textComponent.text == lines[index])
        {
            NextLine();
        }    
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
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
            gameObject.SetActive(false);    
        }
    }

}
