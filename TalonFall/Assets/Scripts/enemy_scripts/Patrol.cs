using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    public GameObject pos1, pos2;
    public float speed;
    public Transform startPos;
    Vector3 Nextpos;
    public bool canFlip;
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
        else if (transform.position == pos2.transform.position)
        {
            Nextpos = pos1.transform.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, Nextpos, speed * Time.deltaTime);
    }
	
	
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Right")&& canFlip)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
            FoeFlipped = true;
        }

        if (collision.gameObject.CompareTag("Left")&& canFlip)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            FoeFlipped = false;
        }
		
		//For bullets
		//I'd advise against tags, but for the sake of showing and general functionality, here you go
		if (collision.gameObject.CompareTag("Shotgun"))
		{
			TakeDamage(30);
			Destroy(collision.gameObject);
		}
		else if (collision.gameObject.CompareTag("Revolver"))
		{
			TakeDamage(10);
			Destroy(collision.gameObject);
		}
    }
	
    public void TakeDamage(int damage)
    {
        health -= damage;

        //Check if dead
        if (health <= 0)
        {
            GameObject.Destroy(this.gameObject);
        }
    }
}
