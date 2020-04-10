using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
    public GameObject Flame;
    public bool Lit;
    // Start is called before the first frame update
    void Start()
    {
        Flame.SetActive(false);
        Lit = false;
    }
    private void Update()
    {
        if (Lit)
        {
            Flame.SetActive(true);
        }
        if(Lit == false)
        {
            Flame.SetActive(false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("DarkZone"))
        {
            Lit = true;
        }
        if (collision.gameObject.CompareTag("LightZone"))
        {
            Lit = false;
        }
    }
}
