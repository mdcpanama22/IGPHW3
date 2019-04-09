using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GM : MonoBehaviour
{

    public delegate void powerUpAbility();
    public static powerUpAbility powerUpAbilityDelegate;

    static Vector2 bounds;
    public static GM instance = null;

    private GameObject canvas;

    public Text Timer;
    public Text WaveT;
    public Text Lives;
    public Text High;
    public Text HighScoreT;

    public Text Object_Score;

    public Text SecondWeapon;
    public Text SpeedUp;

    public int time;
    public int Wave = 1;
    public int lives = 3;
    public int highscore = 0;
    public int oldhighscore;



    public GameObject SpaceShip;
    public GameObject spaceShipPrefab;
    public GameObject []bullet;

    public GameObject NyanCat;

    public bool PlayerSpawning = true;

    public GameObject audioEnemy;
    public GameObject audioAsteroid;
    public GameObject audioPowerUp;
    // Start is called before the first frame update

    void Start()
    {
        Instantiate(NyanCat);
        canvas = GameObject.Find("Canvas");
        ChangeScore(0);
        SpaceShip = Instantiate(spaceShipPrefab);
        PlayerSpawning = false; 
        Timer.text = Time.time.ToString();
        bounds =  new Vector2(10, 5);
        Wave = 1;

        HighScoreT = GameObject.Find("High_Score").GetComponent<Text>();
        High = GameObject.Find("Score").GetComponent<Text>();
        Timer = GameObject.Find("Time").GetComponent<Text>();
        SecondWeapon = GameObject.Find("Second_Weapon").GetComponent<Text>();
        SecondWeapon.enabled = false;
        SpeedUp = GameObject.Find("Speed_Up").GetComponent<Text>();
        SpeedUp.enabled = false;
        oldhighscore = PlayerPrefs.GetInt("HS", 1000);
        HighScore(oldhighscore);
        
    }

    // Update is called once per frame
    void Update()
    {
        T();//Time
        if(time >= 15 * Wave)
        {
            Wave++;
            W();
        }

    }

    private void T()
    {
        time = Mathf.RoundToInt(Time.timeSinceLevelLoad);
        if(Timer != null)
            Timer.text = time.ToString();

        WaveT.text = "Wave: " + Wave.ToString();
    }
    private void W()
    {
        ResetAsteroids();
        WaveT.text = "Wave: " + Wave.ToString();
        Instantiate(NyanCat);
        
    }

    public void DelegateShot()
    {
        powerUpAbilityDelegate();
    }

    private void CurrentLife()
    {
        if (lives == 1)
        {
            Lives.text = "Life: ";
        }
        else
        {
            Lives.text = "Lives: ";
        }

        Lives.text += lives.ToString();
    }

    public void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            DontDestroyOnLoad(gameObject);
    }

    public static Vector2 Boundary(Vector2 pos, Vector2 sBounds)
    {
        bounds = sBounds;
        return Boundary(pos);
    }
    public static Vector2 Boundary(Vector2 pos)
    {
        if(Mathf.Abs(pos.x) > bounds.x)
        {
            pos.x *= -1f;
            pos.x *= .90f;

        }
        if(Mathf.Abs(pos.y) > bounds.y)
        {
            pos.y *= -1f;
            pos.y *= .90f;
        }
        return pos;
    }

    public Vector3 ChooseLocation(Vector3 L)
    {
        Vector3 Location;
        float rX = 0;
        float rY = 0;
        if(L.x < 0)
        {
            rX = Random.Range(0, 9);
        }else if( L.x > 0)
        {
            rX = Random.Range(-9, 0);
        }

        if(L.y < 0)
        {
            rY = Random.Range(0, 4);
        }else if(L.y > 0)
        {
            rY = Random.Range(-4, 0);
        }
        Location = new Vector3(rX, rY, 0);
        /*loat locationX = Location.x;
        float lX = L.x;

        float locationY = Location.y;
        float ly = L.y;

        if((locationX > L.x + 5 && locationX < L.x - 5) && (locationY > ly + 5 && locationY < ly -5) )
        {
            ChooseLocation(L);
        }*/

        return Location;
    }
    private void ResetAsteroids()
    {
        Transform Asteroids = GameObject.Find("Random_Asteroids").transform;
        foreach (Transform t in Asteroids)
        {
            Destroy(t.gameObject);
        }
    }
    private void ResetEnemies()
    {
        Transform Enemies = GameObject.Find("Random_Enemies").transform;
        foreach(Transform t in Enemies)
        {
            Destroy(t.gameObject);
        }
    }

    private void SpawnTextScore(int s, Transform t)
    {
        Text objS = Instantiate(Object_Score);
        objS.transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, t.transform.position);

        objS.transform.parent = canvas.transform;
        objS.text = s.ToString();
    }
    private void EnemySound()
    {
        Instantiate(audioEnemy);
    }
    private void AsteroidSound()
    {
        Instantiate(audioAsteroid);
    }
    public void PowerUpSound()
    {
        Instantiate(audioPowerUp);
    }
    public void DestroyEnemy(GameObject e)
    {
        if(e.layer == LayerMask.NameToLayer("Enemies"))
        {
            if (e.GetComponent<EnemyAI>().enemyT == EnemyAI.ENEMY.BASIC)
            {
                ChangeScore(1000);
                SpawnTextScore(1000, e.transform);
                EnemySound();
            }
        }
        if(e.tag == "Aster")
        {
            ChangeScore(200);
            SpawnTextScore(200, e.transform);
            AsteroidSound();
        }
        if(e.tag == "Plus_Apple")
        {
            ChangeScore(100);
            SpawnTextScore(100, e.transform);
            PowerUpSound();
        }
        if(e.tag == "Plus_Strawberry")
        {
            ChangeScore(500);
            SpawnTextScore(500, e.transform);
            PowerUpSound();
        }
        if(e.tag == "Minus_Apple")
        {
            ChangeScore(-100);
            SpawnTextScore(-100, e.transform);
            PowerUpSound();
        }
        if(e.tag == "Minus_Moonshine")
        {
            ChangeScore(-500);
            SpawnTextScore(-500, e.transform);
            PowerUpSound();
        }
        Destroy(e);
    }
    public void ChangeScore(int score)
    {
        highscore += score;
        string CS = highscore.ToString();
        string REAL = "";
        for (int i = 0; i < 6 - CS.Length; ++i)
        {
            REAL += "0";
        }
        REAL += CS;
        High.text = "Score: " + REAL;
    }
    public void HighScore(float score)
    {
        string CS = score.ToString();
        string REAL = "";
        for (int i = 0; i < 6 - CS.Length; ++i)
        {
            REAL += "0";
        }
        REAL += CS;
        HighScoreT.text = "High Score: " + REAL;
    }
    //Second Weapon
    public void SecondWeaponShowUnshow()
    {
        SecondWeapon.enabled = !SecondWeapon.enabled;
    }
    public void SecondWeaponTimer(float secondWeaponT, float t)
    {
       int timeLeft = Mathf.RoundToInt(secondWeaponT - t);
        SecondWeapon.text = "Second Weapon: " + timeLeft.ToString();
    }
    //Speed up
    public void SpeedUpShowUnshow()
    {
        SpeedUp.enabled = !SpeedUp.enabled;
    }
    public void SpeedUpTimer(float speedUpT, float t)
    {
        int timeLeft = Mathf.RoundToInt( speedUpT - t);
        SpeedUp.text = "Speed up: " + timeLeft.ToString();
    }

    public void PlayerDestroyed()
    {
        if(SecondWeapon.enabled)
            SecondWeaponShowUnshow();
        if (SpeedUp.enabled)
            SpeedUpShowUnshow();
        //set to true so that asteroids spawn after the player spawns
        PlayerSpawning = true;

        //Destroy everything in the scene
        ResetAsteroids();

        //Decrease Life
        lives--;
        CurrentLife();
        //Destroy Player
        Destroy(SpaceShip);
        //if Life == 0 then end game
        if(lives <= 0)
        {
            //END GAME SCENE
            EndGame();
        }
        else
        {
            SpaceShip = Instantiate(spaceShipPrefab);
            PlayerSpawning = false;
        }
        //if not Respawn
    }
    public void EndGame()
    {
        if (oldhighscore < highscore)
        {
            PlayerPrefs.SetInt("HS", highscore);
            HighScore(highscore);
        }
        SceneManager.LoadScene("SampleScene");
    }
    //public static Vector2

}
