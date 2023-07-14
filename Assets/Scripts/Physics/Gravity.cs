using UnityEngine;

public class Gravity : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField]private float scale;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        _rb.AddForce(0, -scale * Time.fixedDeltaTime, 0, ForceMode.Acceleration);
    }
}
