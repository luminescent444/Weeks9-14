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
            spawn = new Vector3 (Random.Range(-8.5f, 9), Random.Range(-2.5f, 2.5f));
            grassObjs.Add(Instantiate(grass, spawn, transform.rotation));
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
