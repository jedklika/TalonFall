using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<KeyEnums> keys;
    public float timer;
    public bool TimeToDie;
	
    public float playerHealth = 90;
	public float playerMaxHealth = 90;
	
    public float SprintTime = 30;
    public bool sprint;
	
	public int revolverMaxAmmo;
	private int revolverAmmo;
	
	public int shotgunMaxAmmo;
	private int shotgunAmmo;
	
	public bool canBeDamaged;
	
	//For player state (will make enumeration for this later) -- David P
	public int playerState = 0;
	/*For now:
		0 = player can control
		1 = player teleporting/in cutscene
		2 = combo lock
	*/
	
	//For indicating stage player is in
	public int playerStage = 0;					//Need enumeration here too
	// 0 = officenight indoor, 1 = officenight outdoor, 2 = diner
	
	PlayerMovement player;
	PlayerSound player_sound;
	
	UI_changer ui_manager;
	
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
		player_sound = FindObjectOfType<PlayerSound>();
		ui_manager = GameObject.Find("ui_canvas").GetComponent<UI_changer>();
		
		revolverAmmo = revolverMaxAmmo;
		shotgunAmmo = shotgunMaxAmmo;
		
		canBeDamaged = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (sprint)
        {
            SprintTime -= Time.deltaTime;
            player.Speed = player.RunSpeed;
            player.jumpHeight = 7;
            //Debug.Log(SprintTime);
        } 
		else 
		{
            player.Speed = 4;
            player.jumpHeight = 5;
			
			if(SprintTime < 30)
				SprintTime += Time.deltaTime;
        }
		
        if (TimeToDie)
        {
            timer = 30;
            timer -= Time.deltaTime;
            Mathf.Round(1);
        } 
		else 
		{
            timer = 100;
        }
		
		
    }
	
	//Managing health
	public void setDamage(float new_damage)
	{
		playerHealth-=new_damage;
		
		//Checking bounds
		//Max
		if (playerHealth > playerMaxHealth)
		{
			playerHealth = playerMaxHealth;
		//Min									--The player dies if this reaches 0
		} 
		else if (playerHealth <= 0)
		{
			playerHealth = 0;
			SceneManager.LoadScene(0);
		}
		
		//Flashing and sounds
		//Damage	(make sure not invincible)
		if (new_damage > 0 && canBeDamaged)
		{
			StartCoroutine(ui_manager.displayDamageFlash());
			player.SetAnimation(6);
			player_sound.PlayHurt();
			setInvincible();
		//Heal
		} 
		else if (new_damage < 0)
		{
			StartCoroutine(ui_manager.displayHealFlash());
		}
		
		//Setting face
		ui_manager.setFace(playerHealth);
	}
	
	IEnumerator setInvincible()
	{
		canBeDamaged = false;
		
		yield return new WaitForSeconds(1.5f);
		
		canBeDamaged = true;
	}
	
	//Managing ammo
	public bool remainingRevolverAmmo()
	{
		return revolverAmmo > 0;
	}
	
	public bool atRevolverMaxAmmo()
	{
		return revolverAmmo == revolverMaxAmmo;
	}
	
	public void useRevolverAmmo(int used_ammo)
	{
		revolverAmmo-=used_ammo;
		
		if (revolverAmmo < 0)
		{
			revolverAmmo = 0;
		}
		else if (revolverAmmo > revolverMaxAmmo)
		{
			revolverAmmo = revolverMaxAmmo;
		}
		
		//Check if using revolver ammo
		if (used_ammo > 0)
			player_sound.PlayGun();
		
		
		//Display ammo capacity
		ui_manager.setGunAmmoDisplay(revolverAmmo.ToString()+"/"+revolverMaxAmmo.ToString());
	}
	
	public bool remainingShotgunAmmo()
	{
		return shotgunAmmo > 0;
	}
	
	public bool atMaxShotgunAmmo()
	{
		return shotgunAmmo == shotgunMaxAmmo;
	}
	
	public void useShotgunAmmo(int used_ammo)
	{
		shotgunAmmo-=used_ammo;
		
		if (shotgunAmmo < 0)
		{
			shotgunAmmo = 0;
		}
		else if (shotgunAmmo > shotgunMaxAmmo)
		{
			shotgunAmmo = shotgunMaxAmmo;
		}
		
		//Check if using shotgun ammo
		if (used_ammo > 0)
			player_sound.PlayGun();
		
		//Display ammo capacity
		ui_manager.setGunAmmoDisplay(shotgunAmmo.ToString()+"/"+shotgunMaxAmmo.ToString());
	}
}
