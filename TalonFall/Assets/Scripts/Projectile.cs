using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    PlayerMovement Player;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
        Player = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.Flipped == false)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        if (Player.Flipped)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
    }
    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
