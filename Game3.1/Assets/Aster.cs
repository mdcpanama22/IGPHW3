using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aster : MonoBehaviour
{
    private Vector3 Direction;
    private new Renderer renderer;
    public bool movementStart = false;
    // Start is called before the first frame update
    void Start()
    {
        Direction = new Vector3(Random.Range(-3f, 3f), Random.Range(-3f, 3f), 0);
        renderer = GetComponent<Renderer>();

        movementStart = false;

        StartCoroutine(FadeIn());
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = GM.Boundary(transform.position);
        
        if(movementStart)
        {
            GetComponent<Rigidbody2D>().MovePosition(transform.position + Direction * Time.deltaTime * transform.GetComponentInParent<Asteroid>().speed);
        }
    }
    public IEnumerator FadeIn()
    {
        for (float i = 0f; i <= 1; i += 0.01f)
        {
            Color c = GetComponent<Renderer>().material.color;
            c.a = i;
            GetComponent<Renderer>().material.color = c;
            yield return null;
        }

        movementStart = true;
    }

}
