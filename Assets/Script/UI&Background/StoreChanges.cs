using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreChanges : MonoBehaviour
{
    public static float Weight;
    public static float Aerodynamic;
    public static float fortune;
    void Start()
    {
        Weight = GetComponent<Rigidbody2D>().mass;
        Aerodynamic = GetComponent<Rigidbody2D>().angularDrag;
     
    
    
    }

    
    void Update()
    {
        ChangeWeight();
    }
    void ChangeWeight()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            Weight = Weight - 0.05f;
        }
    }

}
