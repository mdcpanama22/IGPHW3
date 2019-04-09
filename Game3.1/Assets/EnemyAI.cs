using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public enum ENEMY { BASIC };
    public float speedP = 0.5f;
    public float speedR = 0.5f;

    private float finalT;
    public ENEMY enemyT;
    // Start is called before the first frame update
    void Start()
    {
        enemyT = ENEMY.BASIC;
        finalT = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GM.instance.SpaceShip)
        {
            Vector3 tDirection = -1 * ((transform.position - GM.instance.SpaceShip.transform.position));
            GetComponent<Rigidbody2D>().MovePosition(transform.position + tDirection * Time.deltaTime * speedP);
            float angle = (Vector3.Angle(tDirection, transform.up) + 5);

            if (angle > 10 || angle < -10)
            {
                GetComponent<Rigidbody2D>().MoveRotation(GetComponent<Rigidbody2D>().rotation + (Vector3.Angle(tDirection, transform.up) + 5) * Time.deltaTime * speedR);
            }
            else
            {
                if (finalT == 0 || Time.time - finalT > 2f)
                {
                    Shoot();
                    finalT = Time.time;
                }

            }
        }


    }

    private void Shoot()
    {
        GameObject Bullet = Instantiate(GM.instance.bullet[1]);
        Bullet.transform.position = transform.position;
        Bullet.transform.rotation = transform.rotation;
        Bullet.transform.parent = transform.Find("Bullets").transform;
        Bullet.GetComponent<SpriteRenderer>().flipX = true;
        Bullet.GetComponent<SpriteRenderer>().flipY = true;
        Bullet.GetComponent<bullet>().Direction =  transform.up;
    }
}
