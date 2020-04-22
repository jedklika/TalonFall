using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public float speed;
    public Transform startPos;
    Vector3 Nextpos;
    public bool FoeFlipped;
    public int health;
    Rigidbody2D Rb;
	
    // Start is called before the first frame update
    void Start()
    {
        Nextpos = startPos.position;
        Rb = GetComponent<Rigidbody2D>();
        Rb.bodyType = RigidbodyType2D.Kinematic;
    }

    // Update is called once per frame
    void Update()
    {
        Rb.bodyType = RigidbodyType2D.Kinematic;
        if (transform.position == pos1.transform.position)
        {
            Nextpos = pos2.transform.position;
        }
    }
	
    void ontriggerEnter2D()
	{
        if (collision.gameObject.CompareTag("Right"))
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            FoeFlipped = true;
        }
		
        if (collision.gameObject.CompareTag("Left"))
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            FoeFlipped = false;
        }
    }
}
