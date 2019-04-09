using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    private int amount_of_enemies;
    public GameObject[] Foes;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        amount_of_enemies = GM.instance.Wave;

        if (transform.childCount < amount_of_enemies && !GM.instance.PlayerSpawning)
        {

            GameObject Enemy = Instantiate(Foes[Random.Range(0, Foes.Length)], GM.instance.ChooseLocation(GM.instance.SpaceShip.transform.position), transform.rotation);
            Enemy.transform.parent = transform;
        }
    }
}
