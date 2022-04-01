using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterfallRestriction : MonoBehaviour
{
    public Enums.Axis[] onEnterAxis;
    public Enums.Axis[] onExitAxis;
    float timer = 0.0f;
    bool verify_exit = false;
    SplineFollower exitSplineFollow;

    private void FixedUpdate()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if (timer < 0)
        {
            timer = 0;
        }
        else
        {
            if(verify_exit)
            {
                exitSplineFollow.ignoreAxis = onExitAxis;
                verify_exit = false;
                exitSplineFollow = null;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Floatable")
        {
            if(timer == 0)
            {
                Debug.Log("Box Passed");
                SplineFollower splineFollow = other.gameObject.GetComponent<SplineFollower>();

                splineFollow.ignoreAxis = onEnterAxis;
            }
            timer += Time.deltaTime;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Floatable")
        {
            Debug.Log("Box Exit");
            verify_exit = true;
            exitSplineFollow = other.gameObject.GetComponent<SplineFollower>();
        }
        /*timer += Time.deltaTime;*/
    }
}
