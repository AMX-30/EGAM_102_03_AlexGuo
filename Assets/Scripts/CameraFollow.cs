using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 offset;
    public float damping;
    public Transform target;

    private Vector3 vel = Vector3.zero;
    private Vector3 originalPosition;

    private bool isShaking = false;
    private float shakeDuration = 0f;
    private float shakeMagnitude = 0f;

    private void FixedUpdate()
    {
        if (!isShaking)
        {
            Vector3 targetPosition = target.position + offset;
            targetPosition.z = transform.position.z;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref vel, damping);
        }
    }

    public void StartScreenShake(float duration, float magnitude)
    {
        if (!isShaking)
        {
            isShaking = true;
            shakeDuration = duration;
            shakeMagnitude = magnitude;
            originalPosition = transform.position;
            InvokeRepeating(nameof(Shake), 0f, 0.02f); // Calls Shake() every 0.02s
            Invoke(nameof(StopShake), duration);
        }
    }

    private void Shake()
    {
        if (shakeDuration > 0)
        {
            Vector3 shakeOffset = Random.insideUnitSphere * shakeMagnitude;
            shakeOffset.z = 0f; // Make sure the camera doesn't move on the Z axis
            transform.position = originalPosition + shakeOffset;
        }
    }

    private void StopShake()
    {
        isShaking = false;
        CancelInvoke(nameof(Shake));
    }
}