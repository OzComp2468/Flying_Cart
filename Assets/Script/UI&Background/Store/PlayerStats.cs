using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    // Initial mass and linear drag values
    private float initialMass = 1.5f;
    private float initialLinearDrag = 0.05f;

    // Upgrade parameters
    private const int maxUpgrades = 5;
    private const float MassdecreaseAmount = 0.1f;
    private const float dragdecreaseAmount = 0.01f;
    private int currentMassUpgrades = 0;
    private int currentDragUpgrades = 0;
    private int massUpgradeCost = 5;
    private int dragUpgradeCost = 5;
    private const float upgradeCostIncrease = 1.2f;
   
    //rocket upgrade
    public static bool haveRocket;
    private int rocketCost = 20;
    public static int rocketOwned = 0;
   

    // PlayerPrefs keys for upgrades
    private string massUpgradesKey = "MassUpgrades";
    private string dragUpgradesKey = "DragUpgrades";
    private string rocketKey = "rocketUpgrade";


    // References to UI and Rigidbody2D
    private Rigidbody2D rb2d;

    private void Start()
    {
        haveRocket = false;
        print(rocketOwned);
        rb2d = GetComponent<Rigidbody2D>();

        // Load saved upgrades
        currentMassUpgrades = PlayerPrefs.GetInt(massUpgradesKey, 0);
        currentDragUpgrades = PlayerPrefs.GetInt(dragUpgradesKey, 0);
       
        initialMass = PlayerPrefs.GetFloat("PlayerMass", initialMass); // Load saved mass value
        initialLinearDrag = PlayerPrefs.GetFloat("PlayerLinearDrag", initialLinearDrag); // Load saved linear drag value
      
        haveRocket = PlayerPrefs.GetInt(rocketKey, 0) == 1;

        // Apply upgrades
        ApplyMassUpgrades();
        ApplyDragUpgrades();
    }

    public void UpgradeMass()
    {
        if (currentMassUpgrades < maxUpgrades && UI.coins >= massUpgradeCost)
        {
            initialMass -= MassdecreaseAmount;

            currentMassUpgrades++;
            massUpgradeCost = Mathf.RoundToInt(massUpgradeCost * upgradeCostIncrease);

            PlayerPrefs.SetInt(massUpgradesKey, currentMassUpgrades);
            PlayerPrefs.SetFloat("PlayerMass", initialMass);
            PlayerPrefs.SetInt("MassUpgradeCost", massUpgradeCost);
            PlayerPrefs.Save();

            UI.coins -= massUpgradeCost;

            ApplyMassUpgrades();
        }
    }

    public void UpgradeLinearDrag()
    {
        if (currentDragUpgrades < maxUpgrades && UI.coins >= dragUpgradeCost)
        {
            initialLinearDrag -= dragdecreaseAmount;

            currentDragUpgrades++;
            dragUpgradeCost = Mathf.RoundToInt(dragUpgradeCost * upgradeCostIncrease);

            PlayerPrefs.SetInt(dragUpgradesKey, currentDragUpgrades);
            PlayerPrefs.SetFloat("PlayerLinearDrag", initialLinearDrag);
            PlayerPrefs.SetInt("DragUpgradeCost", dragUpgradeCost);
            PlayerPrefs.Save();

            UI.coins -= dragUpgradeCost;

            ApplyDragUpgrades();
        }
    }




    private void Update()
    {
        


       

    }


    public void buyRocket()
    {
        if(UI.coins > rocketCost && rocketOwned == 0)
        {
            rocketOwned++;
            PlayerPrefs.SetInt(rocketKey, 1); // Save rocket status
            PlayerPrefs.Save();
            UI.coins -= rocketCost;

            // Set haveRocket to true only if the rocket is bought
            haveRocket = true;
        }
    }

    private void ApplyMassUpgrades()
    {
        rb2d.mass = initialMass;
    }

    private void ApplyDragUpgrades()
    {
        rb2d.drag = initialLinearDrag;
    }
}
