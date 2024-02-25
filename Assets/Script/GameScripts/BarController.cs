using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.UI;

public class BarController : MonoBehaviour
{
    public Image fillImage; // Reference to the UI image representing the fill amount
    private float fillSpeed = 1.0f; // Speed at which the bar fills
    private float drainSpeed = 0.5f; // Speed at which the bar empties
    private float launchForceMultiplier =15f; // Multiplier for launch force
    private bool spacePressed = false; // Flag to check if spacebar is pressed
    private bool filling = true; // Flag to track if the bar is filling or emptying
    private Rigidbody2D playerRigidbody;
    bool moreForce;

    //Rotation
    private float maxRota;
    private float minRota;

    //Audio
    public AudioSource AS;
    public AudioClip fastWoosh;
    public AudioClip slowWoosh;


    // AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA



    void Start()
    {
        maxRota = 2;
        minRota = -2;
            
        
        moreForce = true;
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        spacePress();
        Rotation();
        FillAndEmptyBar();
    }

    void FillAndEmptyBar()
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

    void LaunchPlayer()
    {
        // Calculate launch force based on the current fill amount of the bar
        float launchForce = fillImage.fillAmount * launchForceMultiplier * 5;

        // Apply the launch force to the player
        playerRigidbody.AddForce(transform.right * launchForce, ForceMode2D.Impulse);

        // Set spacePressed flag to true to stop auto-filling the bar
        spacePressed = true;
    }

    void  Rotation()
    {
        Vector3 clampedRotation = Vector3.ClampMagnitude(transform.eulerAngles, maxRota);
        clampedRotation.z = Mathf.Clamp(clampedRotation.z, minRota, maxRota);

        transform.eulerAngles = clampedRotation;
    }
    
    void spacePress()
    {
        // If space bar is pressed, launch the player forward
        if (Input.GetKeyDown(KeyCode.Space) && moreForce == true && fillImage.fillAmount < 0.5)
        {
            AS.PlayOneShot(slowWoosh);
            LaunchPlayer();
            moreForce = false;
            fillImage.enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.Space) && moreForce == true && fillImage.fillAmount > 0.5)
        {
            AS.PlayOneShot(fastWoosh);
            LaunchPlayer();
            moreForce = false;
            fillImage.enabled = false;
        }
    }

    
}
