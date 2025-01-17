using UnityEngine;

public class BallBehaviour : MonoBehaviour
{
    public float minX = -8.81f;
    public float maxX = 8.8f;
    public float minY = 4.93f;
    public float maxY = 4.99f;
    public float minSpeed;
    public float maxSpeed;
    Vector2 targetPosition;

    public int secondsToMaxSpeed;

    void Start()
    {
        secondsToMaxSpeed = 30;
        targetPosition = getRandomPosition();
        minSpeed= -0.75f;
        maxSpeed = 2.0f;

    }

    void Update()
    {
        Vector2 currentPos = gameObject.GetComponent<Transform>().position;

        if(targetPosition == currentPos)
        {
            float currentSpeed = minSpeed;
            Vector2 newPosition = Vector2.MoveTowards(currentPos, targetPosition, currentSpeed);
            transform.position = newPosition;
        } 
        else
        {
            transform.position = getRandomPosition();
        }

        // Debug.Log(getRandomPosition());
    }

    public Vector2 getRandomPosition()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        Vector2 v = new Vector2(randomX, randomY);
        return v;
    }
}
