using UnityEngine;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public bool isKey;

    void Start()
    {
        isKey = false;
    }
    
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (isKey)
        {
            if(collision.gameObject.CompareTag("Player"))
            {
                SceneManager.LoadScene("Win");
            }
        }
    }
}
