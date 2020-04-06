using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using StageEnums;

public class SkyboxChanger : MonoBehaviour
{
	//THIS CHANGES THE LEVEL; THE REST IS HANDLED IN BACKGROUND SCRIPT
	public Stages newLevel;
	
	GameManager gm;
	
    // Start is called before the first frame update
    void Start()
    {
        //camera = FindObjectOfType<MainCamera>();
		gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	private void OnTriggerEnter2D(Collider2D col){
		if (gm.playerStage != newLevel)
			gm.playerStage = newLevel;
	}
}
