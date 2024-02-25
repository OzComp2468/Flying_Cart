using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{   
    //General
    public GameObject endPanel;
    private bool canTouchGround;
    private bool TouchGround;
    private Rigidbody2D rb2d;
   
    //Distance
    public Transform startPoint;
    public TextMeshProUGUI distanceText;
    private float Distance;

    public TextMeshProUGUI YouHaveReached;
    public TextMeshProUGUI YouHaveColected;

    //Money
    public TextMeshProUGUI coinsText;
    private int Coins;


    //Audio
    public AudioSource AS;
    public AudioClip coinDrop;
    public AudioClip coinPick;
    public AudioClip fallingDown;
    public AudioClip tryAgain;
    public AudioClip AirPodWoosh;
    public AudioClip Loser;



    void Start()
    {
        canTouchGround = true;
        endPanel.SetActive(false);
        rb2d = GetComponent<Rigidbody2D>();
    }

    
    void Update()
    {
        if (Coins < 0) { Coins = 0; }
        distancePast();
        collectingCoins();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Floor" && canTouchGround == true)
        {
            AS.PlayOneShot(tryAgain);
            canTouchGround =false;
            Debug.Log("GameOver");
            StartCoroutine("gameOver");
            rb2d.gravityScale = 5;
            
        }
        if(collision.gameObject.tag == "Ground")
        {
            TouchGround = true;
        }
    }

    IEnumerator gameOver()
    {
        yield return new WaitForSeconds(2.4f);
        endPanel.SetActive(true);
        distanceText.enabled = false;
        coinsText.enabled = false;
        rb2d.bodyType = RigidbodyType2D.Static;
    }
    IEnumerator LoserEnd()
    {
        yield return new WaitForSeconds(1.6f);
        AS.PlayOneShot(Loser);
    }
    void distancePast()
    {
        Distance = (transform.position.x - startPoint.transform.position.x);
        if (Distance < 0) { Distance = 0; }
        distanceText.text = "Distance:" + Distance.ToString("F1") + "m";

        YouHaveReached.text = "You Have Reached:" + Distance.ToString("F1") + "m";
    }
    void collectingCoins()
    {
        coinsText.text = "Coins Collected:" + Coins.ToString();

        YouHaveColected.text = "Coins Collected:" + Coins.ToString();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Respawn" && TouchGround == true)
        {
           StartCoroutine(LoserEnd());
           StartCoroutine(gameOver()); 
        }
        if(collision.tag == "Pass")
        {
            StopAllCoroutines();    
        }

        //Collectable
        switch (collision.tag) 
        {
            case "AirPod":
                rb2d.AddForce(transform.up * 20, ForceMode2D.Impulse );
                Destroy(collision.gameObject);
                AS.PlayOneShot(AirPodWoosh);
                break;

            case "Coin":
                Coins++;
                Destroy(collision.gameObject);
                AS.PlayOneShot(coinPick);
                break;

            case "AntiCoin":
                Coins--;
                Destroy(collision.gameObject);
                AS.PlayOneShot(coinDrop);
                break;

            case "Weight":

                GetComponent<Rigidbody2D>().gravityScale = 5;
                Destroy(collision.gameObject);
                AS.PlayOneShot(fallingDown);
                break;



        }

    }
}
