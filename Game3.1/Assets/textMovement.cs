using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeOut());
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<RectTransform>().localPosition += Vector3.up;
    }
    public IEnumerator FadeOut()
    {
        GetComponent<Text>().color = new Color( GetComponent<Text>().color.r,  GetComponent<Text>().color.g,  GetComponent<Text>().color.b, 1);
        while (GetComponent<Text>().color.a > 0.0f)
        {
            GetComponent<Text>().color = new Color( GetComponent<Text>().color.r,  GetComponent<Text>().color.g,  GetComponent<Text>().color.b,  GetComponent<Text>().color.a - (Time.deltaTime / 1f));
            yield return null;
        }
        Destroy(gameObject);
    }
}
