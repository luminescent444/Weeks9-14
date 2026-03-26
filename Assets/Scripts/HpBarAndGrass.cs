using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class HpBarAndGrass : MonoBehaviour
{

    public List<GameObject> grassObjs;
    public GameObject grass;
    public Vector3 spawn;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            spawn = new Vector3 (Random.Range(-3, 3), Random.Range(-8, 8));
            //grassObjs[i]=Instantiate(grass);
            grassObjs.Add(Instantiate(grass,transform.position));
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
