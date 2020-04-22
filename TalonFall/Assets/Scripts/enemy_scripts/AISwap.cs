using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISwap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInParent<WayPointAI>().enabled = true;
        GetComponentInParent<Attack>().enabled = false;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Kill");
            GetComponentInParent<Attack>().enabled = true;
            GetComponentInParent<WayPointAI>().enabled = false;

        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Kill");
            GetComponentInParent<Attack>().enabled = true;
            GetComponentInParent<WayPointAI>().enabled = false;

        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Partol");
            GetComponentInParent<WayPointAI>().enabled = true;
            GetComponentInParent<Attack>().enabled = false;
        }
    }
}
