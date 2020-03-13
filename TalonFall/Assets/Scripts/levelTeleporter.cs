using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class levelTeleporter : MonoBehaviour
{
	//Delay before teleporting
	public float playerTeleportTime;
	private bool teleportDebounce;
	
	//Sequence to go into for teleporting
	public int sequenceState;
	
	//Delay before returning to player control state
	public float controlReturnTime;
	
	//Positions and other stats for teleportation
	public int levelTo;
	public Transform positionTo;
	
	//Player tracking
	public Transform playerPosition;
	
	//Teleporter activation
	public bool teleporterActive;
	public bool playerInTeleporter;
	
	//Game master (for player state)
	GameManager gm;
	
    // Start is called before the first frame update
    void Start()
    {
		teleportDebounce = true;
		gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInTeleporter && 
			teleportDebounce && teleporterActive && 
			gm.playerState == 0 && Input.GetKeyDown(KeyCode.E))
		{
			teleportDebounce = false;
			gm.playerState = sequenceState;
			StartCoroutine(teleportPlayer());
		}
    }
	
	IEnumerator teleportPlayer()
	{
		yield return new WaitForSeconds(playerTeleportTime);
		Vector3 new_position = positionTo.position;
		playerPosition.position = new_position;
		
		yield return new WaitForSeconds(controlReturnTime);
		teleportDebounce = true;
		gm.playerState = 0;
	}
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			playerInTeleporter = true;
		}
	}
	
	void OnTriggerExit2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			playerInTeleporter = false;
		}
	}
}
