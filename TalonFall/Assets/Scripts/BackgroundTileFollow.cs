using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundTileFollow : MonoBehaviour
{
	public bool followActive;

	public Transform camera_position;
	
	public float x_crawl;
	public float y_crawl;
	
	public float x_move;
	public float y_move;
	
	GameManager gm;
	
	//For changing level image
	private int currentLevelImage = -1;
	private bool levelChanged;
	
    // Start is called before the first frame update
    void Start()
    {
		gm = FindObjectOfType<GameManager>();
		levelChanged = false;
    }

    // Update is called once per frame
    void Update()
    {
		levelChanged = (currentLevelImage != gm.playerStage);
		
		if (levelChanged)
		{
			Debug.Log("New level is: " + gm.playerStage);
			
			switch (gm.playerStage){
				case 2:				//DINER
					
				break;
				case 1:					//OFFICE OUTDOOR
					
				break;
				case 0:					//OFFICE INDOOR
					
				break;
			}
			
			currentLevelImage = gm.playerStage;
		}
		else
		{
			if (followActive)
			{
				if (gm.playerState == 0)
				{
					Vector3 new_position = camera_position.position;
					transform.position = new_position;
				}
			}
		}
    }
}
