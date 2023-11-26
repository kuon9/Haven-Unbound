using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class room : MonoBehaviour
{
    public GameObject virtualCamera;
    public Rigidbody2D rb;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            virtualCamera.SetActive(true);
            //StartCoroutine(freeze());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            virtualCamera.SetActive(false); 
        }
    }

    IEnumerator freeze()
    {
        rb.constraints |= RigidbodyConstraints2D.FreezePosition;
        yield return new WaitForSeconds(0.6f);
        rb.constraints &= ~RigidbodyConstraints2D.FreezePosition;
    }
}
