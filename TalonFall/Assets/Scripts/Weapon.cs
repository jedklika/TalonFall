using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float offset;
    public GameObject projecticle;
    public Transform ShotPos;
    private float timeBtwShots;
    public float startTimeBtwShots;
	
	//This is for melee weapons only							--David P
	public bool isMelee;
	private bool isBroken = false;
	public int maxDurability;			//guns don't have to worry about the durability stat
	private int currentDurability = 0;
	
    // Start is called before the first frame update
    void Start()
    {
        //In case of loaded file, load that durability, else just set current durability to maxDurability
		currentDurability = maxDurability;
		//for future, else just do something like setBroken(loadedDurability);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
        if(timeBtwShots <= 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Instantiate(projecticle, ShotPos.position, transform.rotation);
                timeBtwShots = startTimeBtwShots;
            }
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }
	
	//For durability (melee only)				--David P
	private void setBroken(bool newBroken)
	{
		isBroken = newBroken;
	}
	
	bool getBroken()
	{
		return isBroken;
	}
	
		//Used by both repair and break
	void setDurability(int amount)
	{
		currentDurability-= amount;
		
		//Determining if broken or not
		if (currentDurability < 1){
			currentDurability = 0;
			setBroken(true);
		} else 
		{
			if (isBroken)
					setBroken(false);
				
			if (currentDurability > maxDurability)
				currentDurability = maxDurability;
		}
	}
	
	int getDurability()
	{
		return currentDurability;
	}
}
