using System.Collections;
using UnityEngine;

public class MoveUpDoor : MonoBehaviour
{
    public float moveDistance = 2f;   // How far to move up
    public float moveSpeed = 5f;      // Speed of movement

    private bool isMoving = false;    // Prevents overlapping movement

    void OnMouseDown()
    {
        if (!isMoving)
        {
            StartCoroutine(MoveSmoothly(moveDistance));
        }
    }

    IEnumerator MoveSmoothly(float distance)
    {
        isMoving = true;
        Vector3 startPosition = transform.position;
        Vector3 endPosition = startPosition + new Vector3(0, distance, 0);

        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime * moveSpeed;
            transform.position = Vector3.Lerp(startPosition, endPosition, t);
            yield return null;
        }

        transform.position = endPosition; // Ensure final position is exact
        isMoving = false;
    }
}