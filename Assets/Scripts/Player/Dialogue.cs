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
    public bool isTriggered;

    private int index;

    
     //Start is called before the first frame update
    void Start()
    {
        DialogueUI.SetActive(false);
        textComponent.text = string.Empty;         
    }

    // Update is called once per frame
    void Update()
    {
        if(textComponent.text == lines[index])
        {
            NextLine();
        }    
    }

    public void StartDialogue()
    {
        
        DialogueUI.SetActive(true);
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
