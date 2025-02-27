using UnityEngine;
using System;

public class SpawningBehaviour : MonoBehaviour
{
    public GameObject[] ballVariants;
    public GameObject targetObject;
    GameObject newObject;
    public float startTime;
    public float spawnRatio = 1.0f;
    public PinSO PinsDB;

    public event EventHandler increaseScore;

    public float minX;
    public float maxX;
    public float minY;
    public float maxY;

    void Start()
    {
        spawnPin();
        spawnBall();
    }

    void Update()
    {
        float currentTime = Time.time;
        float timeElapsed = currentTime - startTime;
        if (timeElapsed > spawnRatio)
        {
            spawnBall();
            increaseScore?.Invoke(this, EventArgs.Empty);
            Debug.Log("Spawning Ball");
        }
    }

    void spawnBall() 
    {
        int numVariants = ballVariants.Length;
        if (numVariants > 0)
        {
            int selection = UnityEngine.Random.Range(0, numVariants);
            newObject = Instantiate(ballVariants[selection], new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
            BallBehaviour ballBehaviour = newObject.GetComponent<BallBehaviour>();
            ballBehaviour.setBounds(minX, maxX, minY, maxY);
            ballBehaviour.setTarget(targetObject);
            ballBehaviour.initialPosition();
        }
        startTime = Time.time;
    }

    void spawnPin()
    {
        targetObject = Instantiate(
            PinsDB.getPin(CharacterManager.selection).prefab,
            Vector3.zero,
            Quaternion.identity
        );
    }
}
