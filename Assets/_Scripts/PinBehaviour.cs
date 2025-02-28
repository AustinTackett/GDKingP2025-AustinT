using UnityEngine;
using System.Collections;

public class PinBehaviour : MonoBehaviour
{
    private float currentSpeed;
    public float baseSpeed = 2.0f;
    
    public float dashSpeed = 5.0f;
    public float dashDuration = 2.0f;
    private float timeDashStart;
    private float timeLastDashEnded;
    private bool dashing;
    public static float dashCooldownRate = 5.0f;
    public static float dashCooldown;
    
    public float invincibleDuration = 1.5f;
    private float timeInvincibleStart;
    private float timeLastInvincibleEnded;
    private bool invincible;
    public static float invincibleCooldownRate = 10.0f;
    public static float invincibleCooldown;

    private bool dead = false;
    private bool invincibleKeyPressed = false;
    private bool dashKeyPressed = false;

    private SpriteRenderer sprite;
    private AudioSource[] audioSources;
    public Vector2 newPosition;
    public Vector3 mousePosG;
    private Camera cam;

    void Start()
    {
        dead = false;
        invincibleKeyPressed = false;
        currentSpeed = baseSpeed;
        dashKeyPressed = false;
        cam = Camera.main;
        audioSources = GetComponents<AudioSource>();
        sprite = GetComponent<SpriteRenderer>();

        // Do keep any previous dash or invincible data if game is played multiple times.
        dashing = false;
        dashCooldown = 0;
        invincible = false;
        invincibleCooldown = 0;
    }

    void Update()
    {
        // Set flag if key press happens
        if (Input.GetKeyDown(KeyCode.Space))
        {
            invincibleKeyPressed = true;
        }
        if (Input.GetMouseButton(0))
        {
            dashKeyPressed = true;
        }
    }

    void FixedUpdate()
    {
        if(dead) return;

        Dash();
        Invincible();
        
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
                dashCooldown = dashCooldownRate;
            }
        } else {
            dashCooldown = dashCooldown - Time.deltaTime;

            if (dashCooldown < 0)
            {
                dashCooldown = 0;
            }

            if (dashKeyPressed && dashCooldown <= 0) {
                dashKeyPressed = false;
                dashing = true;
                currentSpeed = dashSpeed;
                timeDashStart = Time.time;
                if (!audioSources[1].isPlaying)
                {
                    audioSources[1].Stop();
                }
                audioSources[1].Play();
            }
        }       
    }

    private void Invincible(){
        if (invincible == true)
        {
            
            float howLong = Time.time - timeInvincibleStart;
            if (howLong > invincibleDuration)
            {
                invincible = false;
                timeLastInvincibleEnded = Time.time;
                invincibleCooldown = invincibleCooldownRate;
            }
        } else {
            // Set to original colors
            sprite.color = Color.white;
            
            invincibleCooldown = invincibleCooldown - Time.deltaTime;

            if (invincibleCooldown < 0)
            {
                invincibleCooldown = 0;
            }

            if (invincibleKeyPressed  && invincibleCooldown <= 0) {
                invincibleKeyPressed = false;
                invincible = true;
                timeInvincibleStart = Time.time;

                // Give pin a blueish hint
                sprite.color = new Color(121/255, 235/255, 255/255);
                if (audioSources[2].isPlaying)
                {
                    audioSources[2].Stop();
                }
                audioSources[2].Play();
            }
        }       
    }

    // We may be invincible when we first make contact, so lets check every frame for collisions just in case invincibility goes away
    private void OnCollisionStay2D(Collision2D collision)
    {
        string collided = collision.gameObject.tag;

        if ( (collided == "Ball" || collided == "Wall") && !invincible)
        {
            dead = true;
            StartCoroutine(WaitForSoundAndTransition());
        }
    }

    private IEnumerator WaitForSoundAndTransition()
    {
        if (!audioSources[0].isPlaying)
        {
            audioSources[0].Play();
        }
        yield return new WaitForSeconds(audioSources[0].clip.length);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOverMenu");
    }
}
