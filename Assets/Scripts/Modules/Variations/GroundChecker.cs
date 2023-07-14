using System;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    public bool OnGround => _colsNumber != 0;
        

    private int _colsNumber;

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }

        _colsNumber++;
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.isTrigger)
        {
            return;
        }

        _colsNumber--;
    }
}
