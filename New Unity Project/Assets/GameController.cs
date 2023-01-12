using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject StartMenu;

    public GameObject PWeUp;
    public GameObject Enemy;
    public GameObject Coin;

    public GameObject GOCDSL;
    public GameObject GOCDSW;

    public Text CDSlowText;
    public Text CDSwallText;

    public GameObject Player;
    public GameObject CurrentPlayer;

    public GameObject Timer;
    public GameObject currentScore;

    public BoxCollider2D PlayerCollider;
    public BoxCollider2D GamesBounds;

    public Text TheTime;
    public Text BestTime;

    public Text CurrentScore;
    public Text BestScore;

    public Text Deaths;

    public bool start;
    public bool SpawnEnemies;
    public bool SpawnCoins;
    public bool SpawnPowerUps;

    public bool Sheild;
    public bool Swallow;

    public float CountDown = 20f;
    public float CountDown2 = 20f;
    public float CountDown3 = 20f;
    public float CountMax = 20f;
    public float SlowCD = 0f;
    public float SwallowCD = 0f;

    public float time;
    public float bestTime;

    public int Score;
    public int bestScore;

    public int deaths;

    public int SpawnCount;

    public AudioSource powerUpAudio;
    public AudioSource DeathAudio;
    public AudioSource CoinPickUpAudio;
    public AudioSource musicAudio;
    public AudioSource tutorialBoi;

    void Start()
    {
        musicAudio.Play();
        tutorialBoi.Play();
        CountDown = 20f;
        CountDown2 = 20f;
        CountDown3 = 20f;
        CountMax = 20f;
        bestScore = 0;
        bestTime = 0;
        deaths = 0;
        SpawnCount = 0;
    }

    void Update()
    {
        if (!musicAudio.isPlaying)
        {
            musicAudio.Play();
        }
        if (start)
        {
            time = time - Time.deltaTime;
            float Display = -time;
            TheTime.text = "Time: " + Display.ToString("#0");
            Time.timeScale = Time.timeScale + (0.01f * Time.deltaTime);
        }
        if (Input.GetKey("escape"))
        {
            Quit();
        }
    }

    void FixedUpdate()
    {
        if (start)
        {
            CurrentScore.text = "Score: "+Score.ToString();

            foreach (GameObject Enemies in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                if (PlayerCollider.bounds.Contains(Enemies.transform.position))
                {
                    if (!Swallow)
                    {
                        Debug.Log("Sheild is: " + Sheild);
                        if (Sheild == false)
                        {
                            DeathAudio.Play();
                            Debug.Log("Death");
                            StartGame();
                        }
                        if (Sheild = true)
                        {
                            Sheild = false;
                            Destroy(Enemies);
                        }
                    }
                    else
                    {
                        Score++;
                        CoinPickUpAudio.Play();
                        Destroy(Enemies);
                    }
                }
            }
            foreach (GameObject PowerUps in GameObject.FindGameObjectsWithTag("PowerUp"))
            {
                if (PlayerCollider.bounds.Contains(PowerUps.transform.position))
                {
                    Debug.Log("Picked up power up");
                    PowerUp PU = PowerUps.GetComponent<PowerUp>();
                    if (PU.name == "Sheild")
                    {
                        Sheild = true;
                    }
                    if(PU.name == "Slow")
                    {
                        foreach (GameObject Enemies in GameObject.FindGameObjectsWithTag("Enemy"))
                        {
                            Enemy enemy = Enemies.GetComponent<Enemy>();
                            enemy.Speed = 0.0f;
                        }
                        SlowCD = 10f;
                    }
                    if(PU.name == "Swallow")
                    {
                        Swallow = true;
                        SwallowCD = 10f;
                    }
                    powerUpAudio.Play();
                    Destroy(PowerUps);
                }
            }
            foreach (GameObject Coins in GameObject.FindGameObjectsWithTag("Coin"))
            {
                
                if (PlayerCollider.bounds.Contains(Coins.transform.position))
                {
                    CoinPickUpAudio.Play();
                    Debug.Log("Picked up Coin");
                    Score++;
                    Destroy(Coins);
                }
            }

            if (CountDown.ToString("#0") == "5")
            {
                CountDown = CountMax;
                SpawnCount++;
                SpawnPowerUp(1);
            }
            CountDown = CountDown - Time.deltaTime;
            if (CountDown2.ToString("#0") == "10")
            {
                CountDown2 = CountMax; 
                SpawnEnemy(SpawnCount);
            }
            CountDown2 = CountDown2 - Time.deltaTime;
            if (CountDown3.ToString("#0") == "15")
            {
                CountDown3 = CountMax;
                SpawnCoin(1);
            }
            CountDown3 = CountDown3 - Time.deltaTime;
            //Slow CD
            if(SlowCD <= 0f)
            {
                SlowCD = 0f;
                GOCDSL.SetActive(false);
                foreach (GameObject Enemies in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    Enemy enemy = Enemies.GetComponent<Enemy>();
                    enemy.Speed = 1;
                }
            }
            else
            {
                GOCDSL.SetActive(false);
                foreach (GameObject Enemies in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    Enemy enemy = Enemies.GetComponent<Enemy>();
                    enemy.Speed = 0.0f;
                }
                GOCDSL.SetActive(true);
                CDSlowText.text = SlowCD.ToString("#0");
                SlowCD = SlowCD - Time.deltaTime;
            }
            //Swallow CD
            if (SwallowCD <= 0f)
            {
                SwallowCD = 0f;
                Swallow = false;
                GOCDSW.SetActive(false);
            }
            else
            {
                GOCDSW.SetActive(true);
                CDSwallText.text = SwallowCD.ToString("#0");
                SwallowCD = SwallowCD - Time.deltaTime;
            }
            //color
            if(Swallow == true)
            {
                foreach (GameObject Enemies in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    SpriteRenderer enemy = Enemies.GetComponent<SpriteRenderer>();
                    enemy.color = new Color(255, 255, 0);
                }
            }
            else
            {
                foreach (GameObject Enemies in GameObject.FindGameObjectsWithTag("Enemy"))
                {
                    SpriteRenderer enemy = Enemies.GetComponent<SpriteRenderer>();
                    enemy.color = new Color(255, 255, 255);
                }
            }
            if(Sheild == true)
            {
                SpriteRenderer SR = CurrentPlayer.GetComponent<SpriteRenderer>();
                SR.color = new Color(0, 0, 255);
            }
            else
            {
                SpriteRenderer SR = CurrentPlayer.GetComponent<SpriteRenderer>();
                SR.color = new Color(255, 0, 0);
            }
        }
    }

    public void StartGame()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Debug.Log("Called");
        if (StartMenu.active)
        {
            Debug.Log("Start");
            Sheild = false;
            StartMenu.SetActive(false);
            TheTime.text = "0.00";
            time = 0f;
            Score = 0;
            CountDown = 20f;
            CountDown2 = 20f;
            CountDown3 = 20f;
            SlowCD = 0f;
            SwallowCD = 0f;
            SpawnCount = 0;
            Timer.SetActive(true);
            currentScore.SetActive(true);
            start = SpawnPlayer();
        }
        else
        {
            Debug.Log("End");
            GOCDSL.SetActive(false);
            GOCDSW.SetActive(false);
            SlowCD = 0f;
            SwallowCD = 0f;
            deaths++;
            SpawnCount = 0;
            Time.timeScale = 1f;
            if (Score >= bestScore)
            {
                bestScore = Score;
            }
            if((-time) > bestTime)
            {
                bestTime = -time;
            }
            Debug.Log(bestTime);
            StartMenu.SetActive(true);
            Timer.SetActive(false);
            currentScore.SetActive(false);
            start = SpawnPlayer();
        }
    }

    public bool SpawnPlayer()
    {
        if (!StartMenu.active)
        {
            CurrentPlayer = Instantiate(Player);
            PlayerCollider = CurrentPlayer.GetComponent<BoxCollider2D>();
            return true;
        }
        else
        {
            Debug.Log("Destroy all active objects");
            foreach (GameObject Players in GameObject.FindGameObjectsWithTag("Player"))
            {
                Destroy(Players);
            }
            foreach (GameObject Enemies in GameObject.FindGameObjectsWithTag("Enemy"))
            {
                Destroy(Enemies);
            }
            foreach (GameObject PowerUps in GameObject.FindGameObjectsWithTag("PowerUp"))
            {
                Destroy(PowerUps);
            }
            foreach (GameObject Coins in GameObject.FindGameObjectsWithTag("Coin"))
            {
                Destroy(Coins);
            }
            Display();
            return false;
        }
    }

    public void SpawnEnemy(int number)
    {
        for (int count = number; count > 0; count--) 
        {
            Instantiate(Enemy, SpawnLocation(),new Quaternion(0,0,0,0));
        }
    }

    public void SpawnPowerUp(int number)
    {
        for (int count = number; count > 0; count--)
        {
            Instantiate(PWeUp, SpawnLocation(), new Quaternion(0, 0, 0, 0));
        }
    }

    public void SpawnCoin(int number)
    {
        for (int count = number; count > 0; count--)
        {
            Instantiate(Coin, SpawnLocation(), new Quaternion(0, 0, 0, 0));
        }
    }

    public Vector3 SpawnLocation()
    {
        float x = Random.Range(PlayerCollider.bounds.min.x, PlayerCollider.bounds.max.x - 1);
        float y = Random.Range(GamesBounds.bounds.min.y, GamesBounds.bounds.max.y - 1);
        Vector3 V3 = new Vector3(x, y ,0);
        if (Vector3.Distance(CurrentPlayer.transform.position,V3) <= 3)
        {
            x = -x;
            y = -y;
            V3 = new Vector3(x, y);
        }
        return V3;
    }


    public void Display()
    {
        Debug.Log(bestTime);
        BestTime.text = "Best Time: "+bestTime.ToString("#0")+" ("+(-time).ToString("#0")+")";
        BestScore.text = "Most Coins: "+bestScore.ToString()+" ("+Score.ToString()+")";
        Deaths.text = "Deaths: "+deaths.ToString();
    }

    public void Quit()
    {
        Application.Quit();
    }

}
