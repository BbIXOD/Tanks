
using UnityEngine;

public class PhysicsManager : MonoBehaviour
{
    [SerializeField] private float gravity;
    
    private void Awake()
    {
        Physics.gravity = Vector3.down * gravity;
    }
}
