using System;
using UnityEngine;

public class EdgeCollider : MonoBehaviour
{
    private const string ArmourTag = "Armour";
    
    private void OnTriggerStay(Collider collider)
    {
        if (!collider.CompareTag(ArmourTag))
        {
            return;
        }
        
    }
}
