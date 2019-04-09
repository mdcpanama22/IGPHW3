using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    private int amount_of_asteroids;
    public GameObject[] Asteroids;

    public float speed = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GM.instance.Wave < 10)
            amount_of_asteroids = (GM.instance.Wave - 1) + 3;
        else
            amount_of_asteroids = 10;
        if(transform.childCount < amount_of_asteroids && !GM.instance.PlayerSpawning)
        {
            
            GameObject Aster = Instantiate(Asteroids[Random.Range(0, Asteroids.Length)], GM.instance.ChooseLocation(GM.instance.SpaceShip.transform.position), transform.rotation);
            Aster.transform.parent = transform;
        }
        
    }
}
