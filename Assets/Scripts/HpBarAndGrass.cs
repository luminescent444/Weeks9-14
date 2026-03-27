using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HpBarAndGrass : MonoBehaviour
{

    public List<GameObject> grassObjs;
    public GameObject grass;
    public GameObject player;
    public Vector3 spawn;
    public float HP = 1500;
    public bool poison = true;
    public bool ateGrass = false;
    public SpriteRenderer grassSR;
    public SpriteRenderer playerSR;
    public Slider HPBar;
    public float playerX;

    public ProgressSensor sensor; 

    //events
    public UnityEvent loss;
    public UnityEvent win;


    //timer variables
    public float timerCount;
    public float timerLength = 3;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            spawn = new Vector3(Random.Range(-8.5f, 9), Random.Range(-2.5f, 2.5f));
            grassObjs.Add(Instantiate(grass, spawn, transform.rotation));
            playerSR = player.GetComponent<SpriteRenderer>();
        }
        HP = 1500;
        HPBar.maxValue = 1500;
    }

    // Update is called once per frame
    void Update()
    {
        HPBar.value = HP;
        playerX = player.transform.position.x;

        if (poison == true)
        {
            HP = HP - 1;
        }

        if (poison == false)
        {
            timerCount = timerCount + Time.deltaTime;
            if (timerCount > timerLength)
            {
                timerCount = 0;
                poison = true;
            }
        }

        //wins/losses
        if (HP < 0)
        {
            loss.Invoke();
        }

        if (playerX > 9)
        {
            win.Invoke();
        }



    }

    public void grassAte()
    {
        for(int i = 0;i < grassObjs.Count; i++)
        {
            grassSR = grassObjs[i].GetComponent<SpriteRenderer>(); ;
            if (grassSR.bounds.Intersects(playerSR.bounds))
            {
                poison = false;
            }
        }
        
    }

    public void startGame()
    {
        for (int i = 0; i < 10; i++)
        {
            Destroy(grassObjs[i]);
            grassObjs.Remove(grassObjs[i]);
            spawn = new Vector3(Random.Range(-8.5f, 9), Random.Range(-2.5f, 2.5f));
            grassObjs.Add(Instantiate(grass, spawn, transform.rotation));
            playerSR = player.GetComponent<SpriteRenderer>();
        }
        HP = 1500;
        HPBar.maxValue = 1500;
        sensor.resetBools();

        Debug.Log("reset");
    }

}
