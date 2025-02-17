using Unity.Mathematics;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public GameObject player;
    public float spd;
    public float distanceBetween;
    public float distance; 

    public BallControl ballControl;

    public AudioClip breath;
    public AudioSource AudioSource;

    public GameObject bloodEffectPrefab;

    public enum States
    {
        Idle,
        Chase,
        Touch,
    }

    public States states;

    void UpdateTouch()
    {
        ballControl.playerHP -= 4 * Time.deltaTime;
        Debug.Log("Tou");
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            FindFirstObjectByType<CameraFollow>().StartScreenShake(0.3f, 0.1f);
            AudioSource.PlayOneShot(breath);
            states = States.Touch;
        }
        if(other.gameObject.CompareTag("Spike"))
        {
            ballControl.killAmount += 1;
            GameObject blood = Instantiate(bloodEffectPrefab, transform.position, Quaternion.identity);
            Destroy(blood, 1f);
            Destroy(gameObject);
        }

    }

    void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            Invoke("SetToIdle", 1f);
        }

    }


    void SetToIdle()
    {
        states = States.Idle;
    }

    void UpdateChase()
    {
        distance = Vector2.Distance(transform.position, player.transform.position);
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, spd * Time.deltaTime);
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);
    }

    void Update()
    {
        switch (states)
        {
            case States.Idle:
                break;

            case States.Chase:
                UpdateChase();
                break;

            case States.Touch:
                UpdateTouch();
                break;
        }

        if (distance < distanceBetween && states != States.Touch)
        {
            states = States.Chase;
        }
    }
}
