using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class combination_lock : MonoBehaviour
{
	public int combo_required_1;
	public int combo_required_2;
	public int combo_required_3;
	public int combo_required_4;
	
	GameManager gm;
	
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
		//IF IN DEFAULT STATE
		if (gm.canInteract()){
			Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 mouse_point = new Vector2(mouse.x, mouse.y);
			RaycastHit2D hit = Physics2D.Raycast(mouse_point, Vector2.zero);
			
			if (hit){
				//CHECK IF OVER THAT PROP
				//Debug.Log(hit.collider.name + " " + this.name);
				
				//This is not an approach I like, but hey, overengineered problems require overengineered solution.
				if (hit.collider.name == this.name){
					gm.setCursorState(2);
					
					//Checking mouse click
					if (Input.GetMouseButtonDown(0)){
						openingComboLock();
					}
					
					return;
				}
			}
		} 
		
		gm.setCursorState(0);
    }
	
	private void openingComboLock(){
		//REQUEST OPENING COMBO STATE
		//SEND THE GAME MANAGER THE GIVEN COMBO LOCK
		gm.requestComboLock(combo_required_1, combo_required_2, combo_required_3, combo_required_4);
	}
}
