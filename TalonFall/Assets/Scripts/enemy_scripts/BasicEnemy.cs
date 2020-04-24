using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour
{
    //public float speed;
    //public float Distance;
    //public bool moveRight = true;
    //public Transform groundDetection;
    public int health;

    void Update()
    {
		/*
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, Distance);
        if (groundInfo.collider == false)
        {
            if (moveRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                moveRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                moveRight = true;
            }
        }
		*/
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
	
	void OnTriggerEnter2D(Collision2D col)
	{
		//I'd advise against tags, but for the sake of showing and general functionality, here you goto
		if (col.gameObject.CompareTag("shotgunBullet"))
		{
			TakeDamage(30);
			GameObject.Destroy(col.gameObject);
		}
		else if (col.gameObject.CompareTag("revolverBullet"))
		{
			TakeDamage(10);
			GameObject.Destroy(col.gameObject);
		}
	}
}
