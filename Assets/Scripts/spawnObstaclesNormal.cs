using System;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Jobs;

// SpawnObstaclesNormal class that inherits from MonoBehaviour, used for generating and moving obstacles/hearts in the game
public class SpawnObstaclesNormal : MonoBehaviour
{
    [SerializeField] private bool useJobs; // Boolean that determines whether to use the Unity Jobs system
    public GameObject obstacle1; // Prefab for obstacle 1
    public GameObject obstacle2; // Prefab for obstacle 2
    public GameObject obstacle3; // Prefab for obstacle 3
    public GameObject heart; // Prefab for heart
    public float maxX; // Maximum X coordinate for spawning
    public float minX; // Minimum X coordinate for spawning
    public float minY; // Minimum Y coordinate for spawning
    public float maxY; // Maximum Y coordinate for spawning
    public float initialTimeBetweenSpawn; // Initial time between spawning obstacles
    private float timeBetweenSpawn; // Current time between spawning obstacles
    private float heartSpawnTime; // Time until the next heart spawn
    private float spawnTime; // Time until the next obstacle spawn
    private GameObject[] obstacles; // Array of obstacle prefabs
    private ScoreManager scoreManager; // Reference to ScoreManager
    public float obstacleSpeed = 15f; // Speed at which obstacles move

    // List to track spawned obstacles/hearts
    private List<GameObject> spawnObstaclesAndHearts = new List<GameObject>();

    private TransformAccessArray transformAccessArray; // TransformAccessArray used for Unity Job system

    // Start method is called when the script starts executing
    void Start()
    {
        // Initialize the array of obstacles
        obstacles = new GameObject[] { obstacle1, obstacle2, obstacle3 };

        // Find the ScoreManager in the scene
        scoreManager = FindObjectOfType<ScoreManager>();

        // Initialize timeBetweenSpawn with the initial value
        timeBetweenSpawn = initialTimeBetweenSpawn;

        // Initialize TransformAccessArray
        transformAccessArray = new TransformAccessArray(0, -1);
    }

    // OnDestroy method is called when the object is destroyed
    void OnDestroy()
    {
        // Release resources of TransformAccessArray
        if (transformAccessArray.isCreated)
        {
            transformAccessArray.Dispose();
        }
    }

    // Update method is called once per frame
    void Update()
    {
        // Adjust game difficulty based on score
        AdjustDifficulty();

        // Check if it's time to spawn an obstacle
        if (Time.time > spawnTime)
        {
            SpawnObstacle();
            spawnTime = Time.time + timeBetweenSpawn;
        }

        // Check if it's time to spawn a heart
        if (Time.time > heartSpawnTime)
        {
            SpawnHeart();
            float randomInterval = UnityEngine.Random.Range(10f, 20f);
            heartSpawnTime = Time.time + randomInterval;
        }

        // If useJobs is true, use Unity Jobs system
        if (useJobs)
        {
            // Update TransformAccessArray
            UpdateTransformAccessArray();

            // Create and schedule the job
            MoveObstaclesJob moveObstaclesJob = new MoveObstaclesJob
            {
                deltaTime = Time.deltaTime
            };
            JobHandle moveObstaclesJobHandle = moveObstaclesJob.Schedule(transformAccessArray);
            moveObstaclesJobHandle.Complete();
        }
        else
        {
            // If Unity Jobs system is not enabled, move obstacles serially
            for (int i = spawnObstaclesAndHearts.Count - 1; i >= 0; i--)
            {
                if (spawnObstaclesAndHearts[i] != null)
                {
                    spawnObstaclesAndHearts[i].transform.position += new Vector3(-obstacleSpeed * Time.deltaTime, 0, 0);
                    // Remove obstacles that have gone off-screen
                    if (spawnObstaclesAndHearts[i].transform.position.x < -13)
                    {
                        Destroy(spawnObstaclesAndHearts[i]);
                        spawnObstaclesAndHearts.RemoveAt(i);
                    }
                }
            }
        }
    }

    // Method to spawn an obstacle
    void SpawnObstacle()
    {
        float randomX = UnityEngine.Random.Range(minX, maxX);
        float randomY = UnityEngine.Random.Range(minY, maxY);

        // Possible indices are 0, 1, 2, which will result in different colored obstacles being spawned
        int randomIndex = UnityEngine.Random.Range(0, obstacles.Length);
        GameObject randomObstacle = obstacles[randomIndex];

        // Instantiate a new obstacle
        GameObject spawnedObstacle = Instantiate(randomObstacle, transform.position + new Vector3(randomX, randomY, 0), Quaternion.identity);

        // Add the new obstacle to the list
        spawnObstaclesAndHearts.Add(spawnedObstacle);
    }

    // Method to adjust game difficulty based on score
    void AdjustDifficulty()
    {
        if (scoreManager != null)
        {
            float score = scoreManager.score;
            // Gradually adjust game difficulty
            float difficultyFactor = Mathf.Pow(score / 30, 1.5f);
            // 0.5f is the minimum time between spawning obstacles
            timeBetweenSpawn = Mathf.Max(0.5f, initialTimeBetweenSpawn - difficultyFactor);
        }
    }

    // Method to update TransformAccessArray
    void UpdateTransformAccessArray()
    {
        // Release existing TransformAccessArray
        OnDestroy();
        // Recreate TransformAccessArray with the current list of spawned obstacles
        transformAccessArray = new TransformAccessArray(spawnObstaclesAndHearts.Count);
        for (int i = 0; i < spawnObstaclesAndHearts.Count; i++)
        {
            if (spawnObstaclesAndHearts[i] != null)
            {
                transformAccessArray.Add(spawnObstaclesAndHearts[i].transform);
            }
        }
    }

    // Method to spawn a heart
    void SpawnHeart()
    {
        float randomX = UnityEngine.Random.Range(minX, maxX);
        float randomY = UnityEngine.Random.Range(minY, maxY);

        // Instantiate a heart
        GameObject newHeart = Instantiate(heart, transform.position + new Vector3(randomX, randomY, 0), Quaternion.identity);

        spawnObstaclesAndHearts.Add(newHeart);
    }

    // BurstCompile attribute optimizes the code for faster execution
    [BurstCompile]
    public struct MoveObstaclesJob : IJobParallelForTransform
    {
        public float deltaTime;
        // Method that is called for each transform in TransformAccessArray
        public void Execute(int index, TransformAccess transform)
        {
            transform.position += new Vector3(-15f * deltaTime, 0, 0);
        }
    }
}
