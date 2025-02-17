using UnityEngine;

public class Key : MonoBehaviour
{
    public Goal goal;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "KeyHole")
        {
            goal.isKey = true;
            Destroy(gameObject);
        }
    }
}
