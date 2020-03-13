using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
	public bool elevatorActive;
	public bool playerInElevator = false;
	
	//Elevator function vars
    public bool IsMoving = false;
    public bool TopFloor;
    public bool BottomFloor;
    public GameObject Door;
	
    //PlayerMovement Player;
    public GameObject pos1, pos2;
    public float speed;
    public Transform startPos;
    Vector3 Nextpos;
	
    // Start is called before the first frame update
    void Start()
    {
        TopFloor = true;
        //Player = FindObjectOfType<PlayerMovement>();
        Door.SetActive(false);
        Nextpos = startPos.position;

    }

    // Update is called once per frame
    void Update()
    {
		if (elevatorActive && playerInElevator)
		{
			if(BottomFloor || TopFloor)
			{
				Door.SetActive(false);
				IsMoving = false;
			}
			
			if (Input.GetKeyDown(KeyCode.E))
			{
				if (TopFloor)
				{
					Door.SetActive(true);
					IsMoving = true;
					TopFloor = false;
					pos1.SetActive(false);
					pos2.SetActive(true);
				}
				else if (BottomFloor)
				{
					Door.SetActive(true);
					IsMoving = true;
					BottomFloor = false;
					pos2.SetActive(false);
					pos1.SetActive(true);
				}
			}
			
			if (IsMoving)
			{
				if (transform.position == pos1.transform.position)
				{
					Nextpos = pos2.transform.position;
				}
				else if (transform.position == pos2.transform.position)
				{
					Nextpos = pos1.transform.position;
				}
				
				transform.position = Vector3.MoveTowards(transform.position, Nextpos, speed * Time.deltaTime);
			}
		}
    }
	
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.transform.position, pos2.transform.position);
    }
	
    private void OnTriggerEnter2D(Collider2D collision)
    {
		if (elevatorActive)
		{
			if (collision.gameObject.CompareTag("Top"))
			{
				TopFloor = true;
				pos2.SetActive(true);
			} 
			else if (collision.gameObject.CompareTag("Bottom"))
			{
				BottomFloor = true;
				pos1.SetActive(true);
			}
			
			if (collision.gameObject.CompareTag("Player"))
			{
				playerInElevator = true;
			}
		}
    }
	
	void OnTriggerExit2D(Collider2D collision)
	{
		if (elevatorActive)
		{
			if (collision.gameObject.CompareTag("Player"))
				playerInElevator = false;
		}
	}
}
