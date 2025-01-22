using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float minSpeed;
    public float maxSpeed;
    Vector2 targetPosition;

    public int secondsToMaxSpeed;

    void Start()
    {
        //secondsToMaxSpeed = 30;
        targetPosition = getRandomPosition();
        //minSpeed= 0.1f;
        //maxSpeed = 20.0f;

    }

    void Update()
    {
        Vector2 currentPos = transform.position;
        float distance = Vector2.Distance(currentPos, targetPosition);
        if(distance > 0.1f)
        {
            //float currentSpeed = minSpeed;
            float currentSpeed = Mathf.Lerp(minSpeed, maxSpeed, getDifficultyPercentage());
            currentSpeed = currentSpeed * Time.deltaTime;
            Vector2 newPosition = Vector2.MoveTowards(currentPos, targetPosition, currentSpeed);
            transform.position = newPosition;
        } 
        else
        {
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
}
