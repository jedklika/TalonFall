using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISwap : MonoBehaviour
{
    public bool Right;
    public GameObject Monster;
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInParent<Patrol>().enabled = true;
        GetComponentInParent<Attack>().enabled = false;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") &&Right)
        {
            Debug.Log("Kill");
            GetComponentInParent<Attack>().enabled = true;
            GetComponentInParent<Patrol>().enabled = false;
            Monster.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        if (collision.gameObject.CompareTag("Player") && Right == false)
        {
            Debug.Log("Kill");
            GetComponentInParent<Attack>().enabled = true;
            GetComponentInParent<Patrol>().enabled = false;
            Monster.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Right)
        {
            Debug.Log("Kill");
            GetComponentInParent<Attack>().enabled = true;
            GetComponentInParent<Patrol>().enabled = false;
            Monster.transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        if (collision.gameObject.CompareTag("Player") && Right == false)
        {
            Debug.Log("Kill");
            GetComponentInParent<Attack>().enabled = true;
            GetComponentInParent<Patrol>().enabled = false;
            Monster.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Partol");
            GetComponentInParent<Patrol>().enabled = true;
            GetComponentInParent<Attack>().enabled = false;
        }
    }
}
