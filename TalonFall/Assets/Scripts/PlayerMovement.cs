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
	public float shortJumpGravity;
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
	
	private Animator PlayerAnimator;
	private int animationState;
	
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Gun.SetActive(false);
        ShotGun.SetActive(false);
        holstered = true;
        ShotGunholstered = true;
        gm = FindObjectOfType<GameManager>();
		PlayerAnimator = GetComponent<Animator>();
		animationState = 0;
    }

    // Update is called once per frame
    void Update()
    {
		//Equipping or unequipping handgun
        if (Input.GetKeyDown(KeyCode.Alpha1) && holstered)
        {
            Gun.SetActive(true);
            ShotGun.SetActive(false);
            holstered = false;
            ShotGunholstered = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1)&&	!holstered)
        {
            Gun.SetActive(false);
            holstered = true;
            ShotGunholstered = false;
        }
		
		
		//Equipping or unequipping shotgun
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
        
		
		//Sprinting
		if (Input.GetKeyDown(KeyCode.LeftShift) && gm.SprintTime > 0 || Input.GetKeyDown(KeyCode.RightShift) && gm.SprintTime > 0)
        {
            gm.sprint = true;
			
			if (!isJumping)
				SetAnimation(2);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift)|| gm.SprintTime <= 0)
        {
            gm.sprint = false;
        }
		
		
		//Using weapon
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
		
		
		//Movement
        if (Input.GetAxisRaw("Horizontal") < 0f)
        {
            rb.velocity = new Vector3(-Speed, rb.velocity.y, 0f);
            transform.localScale = new Vector3(-1f, 1f, 1f);
			
			if (!isJumping)
				SetAnimation(1);
        }
        else if (Input.GetAxisRaw("Horizontal") > 0f)
        {
            rb.velocity = new Vector3(Speed, rb.velocity.y, 0f);
            transform.localScale = new Vector3(1f, 1f, 1f);
			
			if (!isJumping)
				SetAnimation(1);
        }
        else
        {
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
			
			if (!isJumping)
				SetAnimation(0);
        }
		
		
		//Jumping
		if (isJumping)
        {
            this.gameObject.transform.parent = null;
			
			//Added this for short jumping									--David P
			if (!Input.GetKey(KeyCode.Space) && rb.velocity.y > 2)
			{
				rb.gravityScale = shortJumpGravity;
			}
			
			//Jump animationState
			SetAnimation(3);
        }
		
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            isJumping = true;
        }
		
		//Testing
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
		
    }
	
	//Checking collisions
    void OnCollisionEnter2D(Collision2D col)
    {
		//Acquiring keys
        if (col.gameObject.CompareTag("key")){
            gm.keys++;
            Destroy(col.gameObject);
        }
		
		
		//Going through doors
        if(col.gameObject.CompareTag("Door") && gm.keys <= 0)
        {
            Debug.Log("locked");
        }
		
        if (col.gameObject.CompareTag("Door") && gm.keys >= 1)
        {
            gm.keys--;
            Destroy(col.gameObject);
        }
		
		
		//Falling and jumping collisions
        if (col.gameObject.CompareTag("Falling") && isJumping)
        {
			rb.gravityScale = 1;
            isJumping = false;
        }
		
        if (col.gameObject.CompareTag("Ground") && isJumping)
        {
            isJumping = false;
			rb.gravityScale = 1;									//This line added to set gravity back to normal after shortening jumps		--David P
            Debug.Log("Check");
            this.gameObject.transform.parent = null;
			
            if (isJumping)
            {
                this.gameObject.transform.parent = null;
            }
        }
		
		
		//Elevator
        if (col.gameObject.CompareTag("ElevatorFloor"))
        {
			rb.gravityScale = 1;
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
		
		//Acquiring health kit
        if (col.gameObject.CompareTag("Health"))
        {
            gm.playerHealth += 10;
            Destroy(col.gameObject);
        }
    }
	
	//Platform triggers
    private void OnTriggerStay2D(Collider2D collision)
    {
		//Elevator use
        if (collision.gameObject.CompareTag("Elevator"))
        {
            OnElevator = true;
        }
    }
	
	//Managing animations
	private void SetAnimation(int newAnimationState)
	{
		if (newAnimationState != animationState){
			switch (newAnimationState){
			case 3:				//JUMP
				SetToJump();
			break;
			case 2:				//RUN
				SetToRun();
			break;
			case 1:				//WALK
				SetToWalk();
			break;
			case 0:				//IDLE
			default:
				SetToIdle();
			break;
			}
		}
	}
	
	void SetToIdle()
	{
		animationState = 0;
		PlayerAnimator.Play("Idle");
	}
	
	void SetToWalk()
	{
		animationState = 1;
		PlayerAnimator.Play("walk");
	}
	
	void SetToRun()
	{
		animationState = 2;
		PlayerAnimator.Play("Run");
	}
	
	void SetToJump()
	{
		animationState = 3;
		PlayerAnimator.Play("Jump");
	}
}
