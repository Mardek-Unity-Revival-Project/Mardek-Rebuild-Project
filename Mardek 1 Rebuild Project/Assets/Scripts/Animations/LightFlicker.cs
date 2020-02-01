using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightFlicker : MonoBehaviour
{
    int flickerspeed = 5; //change this to edit the speed of the flicker
    int timer = 0;
    int count = 0;
    
    void Start()
    {
       
    }

    
    void Update()
    {
        timer++;
        
        if (timer == flickerspeed)
        {
            if (count == 0)
            {
                 transform.localScale += new Vector3(0.01f, 0.01f); // Widen the object by x and y values, first is x, second is y.
                timer = 0;
                count++;
            }
            else
            {
                transform.localScale -= new Vector3(0.01f, 0.01f); // Shorten the object by x and y values, first is x, second it y.
                timer = 0;
                count = 0;
            }
        }
        
    }
}
