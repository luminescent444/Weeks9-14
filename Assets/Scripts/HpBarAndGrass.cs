using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class HpBarAndGrass : MonoBehaviour
{

    public List<GameObject> grassObjs;
    public GameObject grass;
    public GameObject player; 
    public Vector3 spawn;
    public float HP = 1500 ;
    public bool poison = true;
    public bool ateGrass = false;
    public SpriteRenderer grassSR;
    public SpriteRenderer playerSR;
    public Slider HPBar;

    //timer variables
    public float timerCount;
    public float timerLength = 3;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            spawn = new Vector3 (Random.Range(-8.5f, 9), Random.Range(-2.5f, 2.5f));
            grassObjs.Add(Instantiate(grass, spawn, transform.rotation));
            playerSR=player.GetComponent<SpriteRenderer>();
        }
        HPBar.value = HP;
        HPBar.maxValue = HP;
    }

    // Update is called once per frame
    void Update()
    {
        if (poison==true)
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
        HPBar.value = HP;
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

}
