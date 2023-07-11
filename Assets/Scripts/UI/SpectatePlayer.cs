using UnityEngine;

public class SpectatePlayer : MonoBehaviour
{
    private Transform _cameraTransform;

    private void Start()
    {
        _cameraTransform = Camera.main!.transform;
    }

    private void Update()
    {
        transform.LookAt(transform.position * 2 - _cameraTransform.position);
    }
}
