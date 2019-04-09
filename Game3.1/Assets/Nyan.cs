using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nyan : MonoBehaviour
{
    public float speed = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position +  transform.right * Time.deltaTime * speed;
        GetComponent<Rigidbody2D>().MovePosition(pos);
        if(transform.position.x > 12f)
        {
            Destroy(gameObject);
        }
    }
}
