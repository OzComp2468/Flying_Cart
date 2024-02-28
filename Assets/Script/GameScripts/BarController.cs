using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class BarController : MonoBehaviour
{
   //Power Meter
    public Image fillImage; // Reference to the UI image representing the fill amount
    private float fillSpeed = 1.0f; // Speed at which the bar fills
    private bool filling = true; // Flag to track if the bar is filling or emptying
    private float drainSpeed = 0.5f; // Speed at which the bar empties
    
    //launch
    private float launchForceMultiplier =15f; // Multiplier for launch force
    private bool spacePressed; // Flag to check if spacebar is pressed
    public ParticleSystem bigStart;
    
    //Rigidbody
    private Rigidbody2D playerRigidbody;
    bool moreForce;
   

    //Rotation
    private float maxRota;
    private float minRota;

    //Audio
    public AudioSource AS;
    public AudioClip fastWoosh;
    public AudioClip slowWoosh;
    public AudioClip Thruster;
    public AudioClip xtrarocket;


    //jump button (dont see after jumping)
    public Image jumpButton;


    //Extra Rocket stuff
    public bool extraRocket;
    public ParticleSystem xtraRocket;


    void Start()
    {
        //jumpButton.gameObject.SetActive(true);
        maxRota = 2;
        minRota = -2;


        spacePressed = false;
        moreForce = true;
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        extraPower();
        spacePress();
        Rotation();
        FillAndEmptyBar();
    }

    void FillAndEmptyBar()//Power Meter fills & emptying
    {
        // Calculate fill direction
        float direction = filling ? 1.0f : -1.0f;

        fillImage.fillAmount += fillSpeed * direction * Time.deltaTime;

        // If the fill amount reaches maximum or minimum, toggle fill direction
        if (fillImage.fillAmount >= 1.0f)
        {
            filling = false;
        }
        else if (fillImage.fillAmount <= 0.0f)
        {
            filling = true;
            spacePressed = false; 
        }

        if (!filling && !spacePressed)
        {
            fillImage.fillAmount -= drainSpeed * Time.deltaTime;
        }
    }

    void LaunchPlayer()//launch... wait4It.. THE PLAYER!!
    {
        // Calculate launch force based on the current fill amount of the bar
        float launchForce = fillImage.fillAmount * launchForceMultiplier * 5;

        // Apply the launch force to the player
        playerRigidbody.AddForce(transform.right * launchForce, ForceMode2D.Impulse);

        // Set spacePressed flag to true to stop auto-filling the bar
        spacePressed = true;
    }

    void  Rotation()//Clamp the rotation
    {
        Vector3 clampedRotation = Vector3.ClampMagnitude(transform.eulerAngles, maxRota);
        clampedRotation.z = Mathf.Clamp(clampedRotation.z, minRota, maxRota);

        transform.eulerAngles = clampedRotation;
    }
    
    void spacePress()// If space bar is pressed, launch the player forward
    {
        
        if (Input.GetKeyDown(KeyCode.Space) && moreForce == true && fillImage.fillAmount < 0.5)
        {
            AS.PlayOneShot(slowWoosh);
            LaunchPlayer();
            moreForce = false;
            fillImage.enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.Space) && moreForce == true && fillImage.fillAmount > 0.5
            && fillImage.fillAmount < 0.8)
        {
            AS.PlayOneShot(fastWoosh);
            LaunchPlayer();
            moreForce = false;
            fillImage.enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.Space) && moreForce == true && fillImage.fillAmount > 0.8)
        {
            LaunchPlayer();
            bigStart.Play();
            AS.PlayOneShot(Thruster);
            moreForce = false;
            fillImage.enabled = false;
        }

    }

    public void YeyJump()//Mobile control
    {
       
        if (moreForce == true && fillImage.fillAmount < 0.5)
        {
            AS.PlayOneShot(slowWoosh);
            LaunchPlayer();
            moreForce = false;
            fillImage.enabled = false;
            //jumpButton.gameObject.SetActive(false);
            jumpButton.enabled = false;
        }
        if (moreForce == true && fillImage.fillAmount > 0.5)
        {
            AS.PlayOneShot(fastWoosh);
            LaunchPlayer();
            moreForce = false;
            fillImage.enabled = false;
            //jumpButton.gameObject.SetActive(false);
            jumpButton.enabled = false;
        }

    }

    public void extraPower()//Extra boost while in the air
    {
        if (Input.GetKeyDown(KeyCode.RightArrow) && extraRocket == true && UI.TouchGround == false)
        {
            playerRigidbody.AddForce(transform.right * 20, ForceMode2D.Impulse);
            playerRigidbody.AddForce(transform.up * 5, ForceMode2D.Impulse);
            xtraRocket.Play();
            AS.PlayOneShot(xtrarocket);
            extraRocket = false;

        }
    }
}
