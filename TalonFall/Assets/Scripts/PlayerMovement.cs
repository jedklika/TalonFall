using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    public float Speed;
    public float RunSpeed;
    public float jumpHeight;
    public bool isJumping = false;
    private float timeBtwAttack;
    public float startTimeBtwAttack;
    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;
    public int damage;
    public GameObject Gun;
    public GameObject ShotGun;
    public bool holstered;
    public bool ShotGunholstered;
    public bool OnElevator = false;
    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Gun.SetActive(false);
        ShotGun.SetActive(false);
        holstered = true;
        ShotGunholstered = true;
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && holstered)
        {
            Gun.SetActive(true);
            ShotGun.SetActive(false);
            holstered = false;
            ShotGunholstered = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1)&& holstered == false)
        {
            Gun.SetActive(false);
            holstered = true;
            ShotGunholstered = false;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && ShotGunholstered)
        {
            ShotGun.SetActive(true);
            Gun.SetActive(false);
            ShotGunholstered = false;
            holstered = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && ShotGunholstered == false)
        {
            ShotGun.SetActive(false);
            ShotGunholstered = true;
            holstered = true;
        }
            if (Input.GetKeyDown(KeyCode.LeftShift) && gm.SprintTime > 0 || Input.GetKeyDown(KeyCode.RightShift) && gm.SprintTime > 0)
        {
            gm.sprint = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift)|| gm.SprintTime <= 0)
        {
            gm.sprint = false;
        }
            if (Input.GetKeyDown(KeyCode.F) && timeBtwAttack <= 0)
        {
            Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
            for (int i = 0; i < enemiesToDamage.Length; i++)
            {
                Debug.Log("Enemy damage" + enemiesToDamage[i]);
                enemiesToDamage[i].GetComponent<BasicEnemy>().health -= damage;

            }
            timeBtwAttack = startTimeBtwAttack;
        }
        else
        {
            timeBtwAttack -= Time.deltaTime;
        }
        if (Input.GetAxisRaw("Horizontal") < 0f)
        {
            rb.velocity = new Vector3(-Speed, rb.velocity.y, 0f);
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        else if (Input.GetAxisRaw("Horizontal") > 0f)
        {
            rb.velocity = new Vector3(Speed, rb.velocity.y, 0f);
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
        }
        if (Input.GetKeyDown(KeyCode.Space) && isJumping == false)
        {
            rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            isJumping = true;
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
        if (isJumping == true)
        {
            this.gameObject.transform.parent = null;
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("key")){
            gm.keys++;
            Destroy(col.gameObject);
        }
        if(col.gameObject.CompareTag("Door") && gm.keys <= 0)
        {
            Debug.Log("locked");
        }
        if (col.gameObject.CompareTag("Door") && gm.keys >= 1)
        {
            gm.keys--;
            Destroy(col.gameObject);
        }
        if (col.gameObject.CompareTag("Falling") && isJumping)
        {
            isJumping = false;
        }
        if (col.gameObject.CompareTag("Ground") && isJumping)
        {
            isJumping = false;
            Debug.Log("Check");
            this.gameObject.transform.parent = null;
            if (isJumping == true)
            {
                this.gameObject.transform.parent = null;
            }
        }
        if (col.gameObject.CompareTag("ElevatorFloor"))
        {
            isJumping = false;
        }
        if (col.gameObject.CompareTag("Danger"))
        {
            SceneManager.LoadScene(0);
        }
        if (col.gameObject.CompareTag("End"))
        {
            SceneManager.LoadScene(0);
        }
        if (col.gameObject.CompareTag("Health"))
        {
            gm.playerHealth += 10;
            Destroy(col.gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Elevator"))
        {
            OnElevator = true;
        }
    }
}
