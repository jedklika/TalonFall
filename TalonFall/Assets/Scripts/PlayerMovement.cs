﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
	
	//Speed and movement
    public float Speed;
    public float RunSpeed;
	private float axis;				//STORED FOR MOVEMENT INPUT USE
	
	//Jump
    public float jumpHeight;
	public float defaultGravity;
	public float shortJumpGravity;
    public bool isJumping = false;
	
	//Attack
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
	
    GameManager gm;
	
	//Animation
	private Animator PlayerAnimator;
	private int animationState;
	
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gm = FindObjectOfType<GameManager>();
		PlayerAnimator = GetComponent<Animator>();
		
		Gun.SetActive(false);
        ShotGun.SetActive(false);
        holstered = true;
        ShotGunholstered = true;
		
		rb.gravityScale = defaultGravity;
		
		animationState = 0;
    }

    // Update is called once per frame
    void Update()
    {
		if (gm.playerState == 0)
		{
			//Equipping or unequipping handgun
			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				if (holstered)
				{
					Gun.SetActive(true);
					ShotGun.SetActive(false);
					holstered = false;
					ShotGunholstered = true;
				}
				else
				{
					Gun.SetActive(false);
					holstered = true;
					ShotGunholstered = false;
				}
			}
			
			
			//Equipping or unequipping shotgun
			if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				if (ShotGunholstered)
				{
					ShotGun.SetActive(true);
					Gun.SetActive(false);
					ShotGunholstered = false;
					holstered = true;
				} 
				else 
				{
					ShotGun.SetActive(false);
					ShotGunholstered = true;
					holstered = true;
				}
			}
			
			
			//Sprinting
			gm.sprint = ((Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift)) && gm.SprintTime > 0 && !isJumping);
			
			
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
			axis = Input.GetAxisRaw("Horizontal");
			
			if (axis < 0f)
			{
				rb.velocity = new Vector3(-Speed, rb.velocity.y, 0f);
				transform.localScale = new Vector3(-1f, 1f, 1f);
				
				if (!isJumping){
					if (!gm.sprint)
						SetAnimation(1);
					else
						SetAnimation(2);
				}
			}
			else if (axis > 0f)
			{
				rb.velocity = new Vector3(Speed, rb.velocity.y, 0f);
				transform.localScale = new Vector3(1f, 1f, 1f);
				
				if (!isJumping)
					if (!gm.sprint)
						SetAnimation(1);
					else
						SetAnimation(2);
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
		else if (gm.playerState == 10)
		{
			SetAnimation(0);
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
			isJumping = false;
			rb.gravityScale = defaultGravity;
        }
		
        if (col.gameObject.CompareTag("Ground") && isJumping)
        {
            isJumping = false;
			rb.gravityScale = defaultGravity;									//This line added to set gravity back to normal after shortening jumps		--David P
            //Debug.Log("Check");
            this.gameObject.transform.parent = null;
        }
		
		
		//Elevator
        if (col.gameObject.CompareTag("ElevatorFloor"))
        {
			isJumping = false;
			rb.gravityScale = defaultGravity;
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
