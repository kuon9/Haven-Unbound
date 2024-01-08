using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MenuManager : MonoBehaviour
{
    public GameObject canvas;

    void Start()
    {
        canvas.SetActive(false);

        StartCoroutine(ActivateCanvas());
    }

    //Activate Canvas after Cutscene
    IEnumerator ActivateCanvas ()
    {
        yield return new WaitForSeconds(33f);
        canvas.SetActive(true);
    }

    public void StartGame ()
    {
        SceneManager.LoadScene("Hotel");
    }

    public void QuitGame() {
        Application.Quit();
        Debug.Log("The game has quit.");
    }
}