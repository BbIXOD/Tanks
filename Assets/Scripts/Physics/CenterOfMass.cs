using UnityEngine;

public class CenterOfMass : MonoBehaviour
{
    private void Awake()
    {
        GetComponentInParent<Rigidbody>()
            .centerOfMass = transform.localPosition;
    }
}
