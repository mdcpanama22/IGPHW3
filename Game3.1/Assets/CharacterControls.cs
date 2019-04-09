/*
                -- Matthew Carlson --
 */ 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterControls : MonoBehaviour{

    public float speed = 2;
    public float speedAngle = 5;
    public float tiltAngle = 15;
    public float changeAngle = 2;

    public GameObject bullet;

    private Rigidbody2D rb2;

    private int moves;

    public float secondWeaponT;
    private bool sw = false;
    private float swTimer;

    public float speedUpT;
    private bool su = false;
    private float suTimer;

    // Use this for initialization

	void Start () {
        rb2 = GetComponent<Rigidbody2D>();
        GM.powerUpAbilityDelegate = Bullet;

    }
    
    // Update is called once per frame
    void Update() {


        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            Up();
            if(!GetComponent<ParticleSystem>().isPlaying)
                GetComponent<ParticleSystem>().Play();
        }
        else
        {
            if(GetComponent<ParticleSystem>().isPlaying)
                GetComponent<ParticleSystem>().Stop();
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            Left();
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            Right();
        }
        if(Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            Down();
        }
        if (Input.GetMouseButtonDown(0))
        {
            GM.instance.DelegateShot();
        }

        transform.position = GM.Boundary(transform.position);

        //Powerups
        if (sw)
        {
            if(Time.time - swTimer >= secondWeaponT)
            {
                GM.powerUpAbilityDelegate = Bullet;
                GM.instance.SecondWeaponShowUnshow();
                sw = false;
            }
            else
            {
                GM.instance.SecondWeaponTimer(secondWeaponT, Time.time - swTimer);
            }
        }
        if (su)
        {
            if(Time.time - suTimer >= speedUpT)
            {
                GM.instance.SpeedUpShowUnshow();
                su = false;
                speed /= 0.5f;
            }
            else
            {
                GM.instance.SpeedUpTimer(speedUpT, Time.time - suTimer);
            }
        }
    }
  
    public void Up()
    {
        Vector3 pos = transform.position + (-1 * transform.up) * Time.deltaTime * speed;
        rb2.MovePosition(pos);
    }
    public void Left()
    {
        tiltAngle = changeAngle;
        ChangeTilt();
    }
    public void Right()
    {
        tiltAngle = -1 * changeAngle;
        ChangeTilt();
    }
    public void Down()
    {
        Vector3 pos = transform.position +  transform.up * Time.deltaTime * speed;
        rb2.MovePosition(pos);
    }
    public void Bullet()
    {
        GameObject Bullet = Instantiate(GM.instance.bullet[0]);
        Bullet.transform.position = transform.position;
        Bullet.transform.rotation = transform.rotation;
        Bullet.transform.parent = transform.GetChild(0).Find("Bullets").transform;

        Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
        Bullet.GetComponent<bullet>().Direction = p - transform.position;
    }
    public void Bullet2()
    {
        List<GameObject> Bullets = new List<GameObject>();

        //transform up

        for(int i = 0; i < 4; i++)
        {
            GameObject Bullet = Instantiate(GM.instance.bullet[0]);
            Bullet.transform.position = transform.position;
            Bullet.transform.rotation = transform.rotation;
            Bullet.transform.parent = transform.GetChild(0).Find("Bullets").transform;

            switch (i)
            {
                case 0:
                    Bullet.GetComponent<bullet>().Direction = -1 * Vector3.up;
                    break;
                case 1:
                    Bullet.GetComponent<bullet>().Direction = Vector3.up;
                    break;
                case 2:
                    Bullet.GetComponent<bullet>().Direction = Vector3.right;
                    break;
                case 3:
                    Bullet.GetComponent<bullet>().Direction = Vector3.left;
                    break;
            }

        }
    }
    public void ChangeTilt()
    {
        rb2.MoveRotation(rb2.rotation + tiltAngle * Time.deltaTime);
    }
    public void down()
    {
        transform.position += Vector3.down * Time.deltaTime * speed;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Aster" || collision.gameObject.tag == "ebullet" ||
            collision.gameObject.tag == "enemy")
        {
            Destroy(collision.gameObject);
            GM.instance.PlayerDestroyed();
        }

        //collision with power up

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "secondWeapon")
        {
            Destroy(collision.gameObject);
            GM.powerUpAbilityDelegate = Bullet2;
            swTimer = Time.time;
            sw = true;
            GM.instance.SecondWeaponShowUnshow();
            GM.instance.PowerUpSound();
        }
        if (collision.gameObject.tag == "speedUp")
        {
            Destroy(collision.gameObject);
            suTimer = Time.time;
            su = true;
            GM.instance.SpeedUpShowUnshow();
            speed *= 2f;
            GM.instance.PowerUpSound();
        }
        if (collision.gameObject.layer == LayerMask.NameToLayer("PowerUp"))
        {
            GM.instance.DestroyEnemy(collision.gameObject);
        }
    }
}
