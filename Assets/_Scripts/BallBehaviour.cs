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
    private GameObject pinInstance;
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

    void Start()
    {
        targetPosition = getRandomPosition();
        pinInstance = GameObject.FindGameObjectWithTag("Pin");
    }

    void FixedUpdate()
    {
        Vector2 currentPos = transform.position;
        if (onCooldown() == false)
        {
            if (launching)
            {
                float currentLaunchTime = Time.time - timeLaunchStart;
                if (currentLaunchTime > launchDuration)
                {
                    startCooldown();
                } else {
                    launch();
                }
            }
        }
        else
        {
            launch();
        }

        float distance = Vector2.Distance((Vector2) currentPos, targetPosition);
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
            transform.position = newPosition;
        } else {
            if (launching == true)
            {
                startCooldown();
            }
            targetPosition = getRandomPosition();
        }

        

        //Debug.Log(getRandomPosition(s));
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
        targetPosition = pinInstance.transform.position;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(this + " Collided with: ");
    }
}
