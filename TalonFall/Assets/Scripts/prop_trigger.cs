using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prop_trigger : MonoBehaviour
{
	public bool has_image;
	public Sprite observe_image;
	public string observe_description;
	
	GameManager gm;
	
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

	/*
    // Update is called once per frame
    void Update()
    {
        
    }
	*/
	
	public void OnTriggerEnter2D(Collider2D col){
		//CHECK IF PLAYER TRIGGERED THIS
		if (col.CompareTag("Player")){
			moveOverProp();
		}
	}
	
	private void moveOverProp(){
		if(has_image)
			gm.interactionOpen(observe_image);
		gm.notifyText(observe_description);
	}
}
