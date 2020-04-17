using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    Vector2 Direction;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
        Direction = FindObjectOfType<PlayerMovement>().Flipped ? Vector2.left : Vector2.right; 
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Direction * speed * Time.deltaTime);
    }

}
