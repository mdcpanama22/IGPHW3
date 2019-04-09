using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Expand());
        Invoke("Close", 5f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Close()
    {
        StartCoroutine(Unexpand());
    }
    public IEnumerator Expand()
    {
        for (float i = 0.1f; i <= 1; i += 0.01f)
        {
            transform.localScale = new Vector3(i, i, i);
            yield return null;
        }
    }
    public IEnumerator Unexpand()
    {
        for (float i = 1f; i >= 0.1f; i -= 0.01f)
        {
            transform.localScale = new Vector3(i, i, i);
            yield return null;
        }
        Destroy(gameObject);
    }
}
