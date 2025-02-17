using System.Collections;
using UnityEngine;

public class RotateDoor : MonoBehaviour
{
    public float rotationAngle = 90f;  // Degrees to rotate
    public float rotationSpeed = 5f;   // Speed of rotation

    private bool isRotating = false;   // Prevents overlapping rotations

    void OnMouseDown()
    {
        if (!isRotating)
        {
            StartCoroutine(RotateSmoothly(rotationAngle));
        }
    }

    IEnumerator RotateSmoothly(float angle)
    {
        isRotating = true;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = Quaternion.Euler(transform.eulerAngles + new Vector3(0, 0, angle));

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * rotationSpeed;
            transform.rotation = Quaternion.Slerp(startRotation, endRotation, t);
            yield return null;
        }

        transform.rotation = endRotation; // Ensure final rotation is exact
        isRotating = false;
    }
}