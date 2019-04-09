using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_up : MonoBehaviour
{
    public GameObject[] Power_ups;
    public GameObject[] items;

    private float timeLapse;
    private float timef;

    private float timeLapseT;
    private float timeLF;
    // Start is called before the first frame update
    void Start()
    {
        timeLapse = Random.Range(10f, 25f);
        timef = Time.time;

        timeLapseT = Random.Range(1f, 10f);
        timeLF = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - timef > timeLapse)
        {
            GameObject pu = Instantiate(Power_ups[Random.Range(0, Power_ups.Length)], GM.instance.ChooseLocation(GM.instance.SpaceShip.transform.position), transform.rotation);
            timef = Time.time;
            timeLapse = Random.Range(10f, 25f);
        }

        if(Time.time - timeLF > timeLapseT)
        {
            GameObject item = Instantiate(items[Random.Range(0, items.Length)], GM.instance.ChooseLocation(GM.instance.SpaceShip.transform.position), transform.rotation);
            timeLF = Time.time;
            timeLapse = Random.Range(1f, 10f);
        }
    }
}
