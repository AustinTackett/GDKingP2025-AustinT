using UnityEngine;
using UnityEditor;

public class BallBehaviour : MonoBehaviour
{
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float minSpeed;
    public float maxSpeed;
    public Vector2 targetPosition;

    public GameObject target;
    //private GameObject pinInstance;
    public float minLaunchSpeed;
    public float maxLaunchSpeed;
    public float minTimeToLaunch;
    public float maxTimeToLaunch;
    public float cooldown;
    public bool launching;
    public float launchDuration;
    public float timeLastLaunch;
    public float timeLaunchStart;
    public int secondsToMaxSpeed;

    Rigidbody2D body;
    public bool rerouting;

    public void Start()
    {
        targetPosition = getRandomPosition();
        target = GameObject.FindGameObjectWithTag("Pin");
        //initialPosition();
    }

    public void FixedUpdate()
    {
        body = GetComponent<Rigidbody2D>();
        Vector2 currentPos = body.position;
        if (onCooldown() == false)
        {
            if (launching)
            {
                float currentLaunchTime = Time.time - timeLaunchStart;
                if (currentLaunchTime > launchDuration)
                {
                    startCooldown();
                }
            } else {
                    launch();
                }
        }

        float distance = Vector2.Distance(currentPos, targetPosition);
        if(distance > 0.1f)
        {
            float currentSpeed;
            if(launching == true)
            {
                currentSpeed = Mathf.Lerp(minLaunchSpeed, maxLaunchSpeed, getDifficultyPercentage());
            } else {
                currentSpeed = Mathf.Lerp(minSpeed, maxSpeed, getDifficultyPercentage());
            }
            currentSpeed = currentSpeed * Time.deltaTime;
            Vector2 newPosition = Vector2.MoveTowards(currentPos, targetPosition, currentSpeed);
            body.MovePosition(newPosition);
        } else {
            if (launching == true)
            {
                startCooldown();
            }
            targetPosition = getRandomPosition();
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        string collided = collision.gameObject.tag;
        Debug.Log(this + " Collided with: " + collision.gameObject.name);
        if (collided == "Wall") {
            targetPosition = getRandomPosition();
            //Debug.Log("Game Over");
        }
        if (collided == "Ball")
        {
            //Debug.Log("rerouting");
            Reroute(collision);
        }
    }

    public void initialPosition()
    {
        body = GetComponent<Rigidbody2D>();
        body.position = getRandomPosition();
        targetPosition = getRandomPosition();
        launching = false;
        rerouting = true;
    }

    public void Reroute(Collision2D collision)
    {
        GameObject otherball = collision.gameObject;
        if (rerouting == true)
        {
            otherball.GetComponent<BallBehaviour>().rerouting = false;
            
            Rigidbody2D ballBody = otherball.GetComponent<Rigidbody2D>();
            Vector2 contact = collision.GetContact(0).normal;
            targetPosition = Vector2.Reflect(targetPosition, contact).normalized;
            
            launching = false;
            float seperationDistance = 0.01f;
            ballBody.position += contact * seperationDistance;
        } else {
            rerouting = true;
        }
    }

    public Vector2 getRandomPosition()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        Vector2 v = new Vector2(randomX, randomY);
        return v;
    }

    public float getDifficultyPercentage()
    {
        return Mathf.Clamp01(Time.timeSinceLevelLoad / secondsToMaxSpeed);
    }

    public void launch()
    {   
        //targetPosition = target.transform.position;
        Rigidbody2D targetBody = target.GetComponent<Rigidbody2D>();
        targetPosition = targetBody.position;

        if (launching == false) 
        {
            timeLaunchStart = Time.time;
            launching = true;
        }

        // random cooldown 
        cooldown = Random.Range(1, 5);
    }

    public bool onCooldown()
    {
        bool onCooldown = true;
        float timeSinceLastLaunch = Time.time - timeLastLaunch;

        if(timeSinceLastLaunch > cooldown)
        {
            onCooldown = false;
        }

        return onCooldown;
    }

    public void startCooldown()
    {
        timeLastLaunch = Time.time;
        launching = false;
    }
}
