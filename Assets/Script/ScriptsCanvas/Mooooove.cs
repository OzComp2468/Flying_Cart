using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Mooooove : MonoBehaviour
{
    float dirX, speed;



    // Start is called before the first frame update
    void Start()
    {
        dirX = 0;
        speed = 2;

        
    }

    // Update is called once per frame
    void Update()
    {

        // MaxDist();


    }

    public void Righghg(string dir)
    {
        if (dir == "Right")
        {
            dirX = 10;
        }

         if (dir == "Left")
        {
            dirX = -10;
        }


        else if (dir == "Stop")
        {
            dirX = 0;
        }

        transform.position = new Vector3(transform.position.x + dirX * speed/4, 0, -10);
    }

    public void MaxDist()
    {
        if (transform.position.x >= -1)
        {
            dirX = 0;

        }
        else if (transform.position.x == 5)
        {
            dirX = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pet")
        {
            GameObject pet = collision.gameObject;
           
            pet.transform.localScale = new Vector3(1.2f, 1.2f, 0);
        }
       
      

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Pet")
        {
            GameObject pet = collision.gameObject;

            pet.transform.localScale = new Vector3(1, 1, 0);
        }



    }








}
