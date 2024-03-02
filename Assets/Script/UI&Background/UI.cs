using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{   
    //General
    public GameObject endPanel;
    private bool canTouchGround;
    public static bool TouchGround;
    public static Rigidbody2D rb2d;
   
    //Distance
    public Transform startPoint;
    public TextMeshProUGUI distanceText;
    private float Distance;
    public TextMeshProUGUI YouHaveReached;

    //Highscore
    public static float HighScore;
    public TextMeshProUGUI HighscoreText;

    //Coins
    public TextMeshProUGUI allCoins;
    public TextMeshProUGUI coinsText;
    public static float coins;
    public static int coinCounter;
    private int coinsThisRound;
    public TextMeshProUGUI TotalCoins;

    //Audio
    public AudioSource AS;
    public AudioClip coinDrop;
    public AudioClip coinPick;
    public AudioClip fallingDown;
    public AudioClip tryAgain;
    public AudioClip AirPodWoosh;
    public AudioClip Loser;
    public AudioClip newhighscore;



    void Start()
    {
        canTouchGround = true;
        endPanel.SetActive(false);
        rb2d = GetComponent<Rigidbody2D>();
        HighscoreText.text = "High Score: " + PlayerPrefs.GetFloat("HighScore",0).ToString("F1") +"m";
        coins = PlayerPrefs.GetFloat("TotalCoins", coins);
        coinCounter = 0;
        coinsThisRound = 0;
        TotalCoins.text = "Total Coins: " + coins; 

    }

    
    void Update()
    {
       
        distancePast();
        coinCollecting();
        

    }
   
    
    //Ground collision logic
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor" && canTouchGround == true)
        {
            AS.PlayOneShot(tryAgain);
            canTouchGround =false;
            Debug.Log("GameOver");
            StartCoroutine("gameOver");
            rb2d.gravityScale = 5;
            TouchGround = true;
            
        }
        if (collision.gameObject.tag == "Ground")
        {
            TouchGround = true;
        }
        
    }
    
    //GameOver stuff
    IEnumerator gameOver()
    {
        yield return new WaitForSeconds(2.4f);
        endPanel.SetActive(true);
        distanceText.enabled = false;
        rb2d.bodyType = RigidbodyType2D.Static;
        coinsText.enabled = false;
        TotalCoins.enabled = false;
        EndRound();



    }
    IEnumerator LoserEnd()
    {
        yield return new WaitForSeconds(1.6f);
        AS.PlayOneShot(Loser);
        EndRound();
    }
    
    void distancePast()//UI distance
    {
        Distance = (transform.position.x - startPoint.transform.position.x);
        if (Distance < 0) { Distance = 0; }
        distanceText.text = "Distance:" + Distance.ToString("F1") + "m";

        YouHaveReached.text = "You Have Reached:" + Distance.ToString("F1") + "m";
       
        //Highscore
        if(Distance > PlayerPrefs.GetFloat("HighScore",0))
        {
            PlayerPrefs.SetFloat("HighScore", Distance);
            HighscoreText.text = "High Score: " + Distance.ToString("F1")+"m";
        }
        
    }

    void coinCollecting()
    {
        if (coinsThisRound< 0) { coinsThisRound = 0; }
        coinsText.text = coinsThisRound.ToString() + " Coins collected";
        allCoins.text = "Total Coins: " + coins.ToString();

        
    }

    private void OnTriggerEnter2D(Collider2D collision)//Collectables and start
    {
        if(collision.tag == "Respawn" && TouchGround == true)
        {
           StartCoroutine(LoserEnd());
           StartCoroutine(gameOver()); 
        }
        if(collision.tag == "Pass")
        {
            StopAllCoroutines();   
            TouchGround = false;
        }

        //Collectable
        switch (collision.tag) 
        {
            case "AirPod":
                rb2d.AddForce(transform.up * 20, ForceMode2D.Impulse);
                Destroy(collision.gameObject);
                AS.PlayOneShot(AirPodWoosh);
                break;

            case "Coin":
                coinCounter++;
                coinsThisRound++;
                Destroy(collision.gameObject);
                AS.PlayOneShot(coinPick);
                break;

            case "AntiCoin":
                coinsThisRound--;
                Destroy(collision.gameObject);
                AS.PlayOneShot(coinDrop);
                break;

            case "Weight":

                GetComponent<Rigidbody2D>().gravityScale ++;
                Destroy(collision.gameObject);
                AS.PlayOneShot(fallingDown);
                break;
        }

    }
    public void EndRound()
    {
        coins += coinsThisRound;
        SaveTotalCoins();
    }
    void SaveTotalCoins()
    {
        PlayerPrefs.SetFloat("TotalCoins", coins);
        PlayerPrefs.Save();
    }
   
}
