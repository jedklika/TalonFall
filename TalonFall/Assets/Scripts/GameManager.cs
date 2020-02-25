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
            Debug.Log(SprintTime);
        }
        if( sprint == false)
        {
            player.Speed = 4;
            player.jumpHeight = 5;
        }
        if(sprint == false && SprintTime < 30)
        {
            SprintTime += Time.deltaTime;
        }
        if (TimeToDie)
        {
            timer = 30;
            timer -= Time.deltaTime;
            Mathf.Round(1);
        }
        if(TimeToDie == false)
        {
            timer = 100;
        }
    }
}
