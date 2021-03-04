using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    GameObject cat;

    // Start is called before the first frame update
    void Start()
    {
        cat = GameObject.Find("Cat");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 catPos = transform.position;
        catPos.y = cat.transform.position.y; // only take y position of cat

        if(catPos.y > transform.position.y) transform.position = catPos; // only move camera upwards
    }
}
