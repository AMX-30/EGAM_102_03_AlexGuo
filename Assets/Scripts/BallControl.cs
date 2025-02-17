using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class BallControl : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private Vector2 startMousePosition;
    private Vector2 releaseMousePosition;
    private bool isDragging = false;
    public float maxDragDistance;

    public float launchForce = 5f;
    private LineRenderer lineRenderer;

    public float playerHP;
    public float playerMaxHP;
    public int killAmount;
    public TMP_Text heathBar;
    public TMP_Text kill;

     public GameObject bloodEffectPrefab;
     public GameObject outline;

     public AudioSource audioSource;
     public AudioClip audioClip;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        lineRenderer = gameObject.AddComponent<LineRenderer>();

        lineRenderer.startWidth = 0.1f;
        lineRenderer.endWidth = 0.1f;
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.red;
        lineRenderer.endColor = Color.red;
        lineRenderer.enabled = false;

        playerHP = playerMaxHP;
        killAmount = 0;
    }


    void OnMouseDown()
    {
        startMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;
        rb2d.linearVelocity = Vector2.zero;
        lineRenderer.enabled = true;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector2 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (startMousePosition - currentMousePosition).normalized;
            float dragDistance = Vector2.Distance(startMousePosition, currentMousePosition);
            dragDistance = Mathf.Min(dragDistance, maxDragDistance);

            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, transform.position + (Vector3)(direction * dragDistance * 2));
        }
    }

    void OnMouseUp()
    {
        if (isDragging)
        {
            releaseMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 launchDirection = (startMousePosition - releaseMousePosition).normalized;
            float dragDistance = Vector2.Distance(startMousePosition, releaseMousePosition);
              dragDistance = Mathf.Min(dragDistance, maxDragDistance);

            rb2d.AddForce(launchDirection * launchForce * dragDistance, ForceMode2D.Impulse);
            isDragging = false;
            lineRenderer.enabled = false;
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Spike"))
        {
            FindFirstObjectByType<CameraFollow>().StartScreenShake(0.3f, 0.1f);
            GameObject blood = Instantiate(bloodEffectPrefab, transform.position, Quaternion.identity);
            playerHP -= 15;
            Destroy(blood, 1f);
        }
        if(collision.gameObject.CompareTag("Enemy"))
        {
            GameObject blood = Instantiate(bloodEffectPrefab, transform.position, Quaternion.identity);
            Destroy(blood, 1f);
        }
        if(collision.gameObject != null && !collision.gameObject.CompareTag("Key") && !collision.gameObject.CompareTag("Oxygen"))
        {
            audioSource.PlayOneShot(audioClip);
        }
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            FindFirstObjectByType<CameraFollow>().StartScreenShake(0.5f, 0.2f);
        }

        if(playerHP >= playerMaxHP)
        {
            playerHP = playerMaxHP;
        }

        if (playerHP == 0 || playerHP < 0)
        {
            SceneManager.LoadScene("Fail");
        }

        outline.transform.position = this.transform.position;
        int Conver = Mathf.RoundToInt(playerHP);
        heathBar.text = Conver.ToString();
        kill.text = killAmount.ToString();

    }
}
