using UnityEngine;
using System.Collections;

public class PinBehaviour : MonoBehaviour
{
    public float currentSpeed;
    public float dashSpeed = 5.0f;
    public float dashDuration = 2.0f;
    public float timeDashStart;
    public bool dashing;
    public float baseSpeed = 2.0f;
    public float timeLastDashEnded;
    public static float cooldownRate = 5.0f;
    public static float cooldown;
    public AudioSource[] audioSources;
    public Vector2 newPosition;
    public Vector3 mousePosG;
    Camera cam;

    void Start()
    {
        currentSpeed = baseSpeed;
        cam = Camera.main;
        audioSources = GetComponents<AudioSource>();
    }

    void Update()
    {
        Dash();

        mousePosG = cam.ScreenToWorldPoint(Input.mousePosition);
        newPosition = Vector2.MoveTowards(transform.position, mousePosG, currentSpeed * Time.fixedDeltaTime);
        transform.position = newPosition;
    }

    private void Dash(){
        if (dashing == true)
        {
            float howLong = Time.time - timeDashStart;
            if (howLong > dashDuration)
            {
                dashing = false;
                currentSpeed = baseSpeed;
                timeLastDashEnded = Time.time;
                cooldown = cooldownRate;
            }
        } else {
            cooldown = cooldown - Time.deltaTime;

            if (cooldown < 0)
            {
                cooldown = 0;
            }

            if (Input.GetMouseButton(0) == true && cooldown <= 0) {
                dashing = true;
                currentSpeed = dashSpeed;
                timeDashStart = Time.time;
                if (audioSources[1].isPlaying)
                {
                    audioSources[1].Stop();
                }
                audioSources[1].Play();
            }
        }       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string collided = collision.gameObject.tag;
        Debug.Log(collided);

        if (collided == "Ball" || collided == "Wall")
        {
            Debug.Log("Game Over");
            StartCoroutine(WaitForSoundAndTransition());
        }

    }

    private IEnumerator WaitForSoundAndTransition()
    {
        audioSources[0].Play();
        yield return new WaitForSeconds(audioSources[0].clip.length);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverMenu");
    }
}
