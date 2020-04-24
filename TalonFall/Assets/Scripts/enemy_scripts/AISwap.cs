using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISwap : MonoBehaviour
{
    public bool Right;
    public SpriteRenderer Monster;
    public bool flipped;
    // Start is called before the first frame update
    void Start()
    {
        GetComponentInParent<Patrol>().enabled = true;
        GetComponentInParent<Attack>().enabled = false;
        Monster = GetComponentInParent<SpriteRenderer>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") &&Right)
        {
            Debug.Log("Kill");
            GetComponentInParent<Attack>().enabled = true;
            GetComponentInParent<Patrol>().enabled = false;
            Monster.flipX = true;
            flipped = true;
        }
        if (collision.gameObject.CompareTag("Player") && Right == false)
        {
            Debug.Log("Kill");
            GetComponentInParent<Attack>().enabled = true;
            GetComponentInParent<Patrol>().enabled = false;
            Monster.flipX = false;
            flipped = false;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Right)
        {
            Debug.Log("Kill");
            GetComponentInParent<Attack>().enabled = true;
            GetComponentInParent<Patrol>().enabled = false;
        }
        if (collision.gameObject.CompareTag("Player") && Right == false)
        {
            Debug.Log("Kill");
            GetComponentInParent<Attack>().enabled = true;
            GetComponentInParent<Patrol>().enabled = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")&& Right)
        {
            Debug.Log("Partol");
            GetComponentInParent<Patrol>().enabled = true;
            GetComponentInParent<Attack>().enabled = false;
            Monster.flipX = false;
        }
        if (collision.gameObject.CompareTag("Player") && Right==false)
        {
            Debug.Log("Partol");
            GetComponentInParent<Patrol>().enabled = true;
            GetComponentInParent<Attack>().enabled = false;
            Monster.flipX = true;
        }
    }
}
