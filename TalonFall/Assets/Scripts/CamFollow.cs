using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;
	
	//For tracking player state for certain cutscenes or modes
	GameManager gm;
	
	// Start is called before the first frame update
    void Start()
    {
		gm = FindObjectOfType<GameManager>();
    }

    private void FixedUpdate()
    {
		//When the player is in control
		if (gm.playerState == 0)
		{
			Vector3 desiedPosition = target.position + offset;
			Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiedPosition, smoothSpeed);
			transform.position = smoothedPosition;
		} 
		else if (gm.playerState == 10)
		{
			StartCoroutine(teleportCamera(3f, 2));
		}
    }
	
	IEnumerator teleportCamera(float seconds, int level)
	{
		yield return new WaitForSeconds(seconds);
		transform.position = target.position + offset;
		gm.playerStage = level;
	}
}
