using UnityEngine;

public class Oxygen : MonoBehaviour
{
    public BallControl ballControl;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            ballControl.playerHP += 20;
            Destroy(gameObject);
        }
    }
}
