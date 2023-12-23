using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillSelf : MonoBehaviour
{
    float current;
    float total;
    bool isDistance;
    Vector3 originalPos;
    float distanceAllowed;

    public void SetUpTimer(float total)
    {
        this.total = total;
    }
    public void SetUpDistance(Vector3 originalPos, float distanceAllowed)
    {
        isDistance = true;


       
        this.originalPos = originalPos;
        this.distanceAllowed = distanceAllowed * 0.95f;
    }

    private void FixedUpdate()
    {
        if(total > 0 )
        {
            if(total > current)
            {
                current += 0.01f;
            }
            else
            {
     
                Destroy(gameObject);
            }

        }

        if (isDistance)
        {
            if (Vector2.Distance(transform.position, originalPos) > distanceAllowed)
            {
                
                Destroy(gameObject);
            }
        }


    }

}
