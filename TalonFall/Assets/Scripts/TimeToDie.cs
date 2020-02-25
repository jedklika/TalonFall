using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeToDie : MonoBehaviour
{
    GameManager gm;
    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            gm.TimeToDie = true;
            Debug.Log("Dying");
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            gm.TimeToDie = false;
            Debug.Log("freedom!!!!");
        }
    }
}
