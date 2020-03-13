using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public float keys;
    public float timer;
    public bool TimeToDie;
    public float playerHealth = 90;
    public float SprintTime = 30;
    public bool sprint;
	
    PlayerMovement player;
	
	//For player state (will make enumeration for this later) -- David P
	public int playerState = 0;
	/*For now:
		0 = player can control
		1 = player teleporting/in cutscene
		2 = combo lock						(I will make everyone eat shoes if I get this system in by Friday btw, but everything's dizzy rn so idk)
	*/
	
	//For indicating stage player is in
	public int playerStage = 0;					//Need enumeration here too
	// 0 = officenight indoor, 1 = officenight outdoor, 2 = diner
	
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerMovement>();
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
}
