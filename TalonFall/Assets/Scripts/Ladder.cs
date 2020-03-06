using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public float speed;
    PlayerMovement player;
    private void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player" && Input.GetKey(KeyCode.UpArrow))
        {
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
            player.isJumping = false;
        }
        else if (other.tag == "Player" && Input.GetKey(KeyCode.DownArrow))
        {
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -speed);
            player.isJumping = false;
        }
        else
        {
            other.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0.2f);
            player.isJumping = false;
        }
    }
}
