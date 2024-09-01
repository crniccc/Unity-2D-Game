using System;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Collections;
using Unity.Jobs;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Jobs;

// SpawnObstaclesStress class handles the spawning and movement of obstacles in a stress test scenario
public class SpawnObstaclesStress : MonoBehaviour
{
    [SerializeField] private bool useJobs; // Determines whether to use Unity Jobs system
    public GameObject obstacle1; // Prefab for obstacle 1
    public GameObject obstacle2; // Prefab for obstacle 2
    public GameObject obstacle3; // Prefab for obstacle 3

    public float maxX; // Maximum X coordinate for obstacle spawning
    public float minX; // Minimum X coordinate for obstacle spawning
    public float minY; // Minimum Y coordinate for obstacle spawning
    public float maxY; // Maximum Y coordinate for obstacle spawning
    public float initialTimeBetweenSpawn; // Initial time between obstacle spawns
    private float timeBetweenSpawn; // Current time between obstacle spawns
    private float spawnTime; // Time until the next obstacle spawn
    private GameObject[] obstacles; // Array of obstacle prefabs
    private ScoreManager scoreManager; // Reference to ScoreManager
    public float obstacleSpeed = 15f; // Speed at which obstacles move

    private List<GameObject> spawnedObstacles = new List<GameObject>(); // List to track spawned obstacles
    private TransformAccessArray transformAccessArray; // TransformAccessArray used for Unity Jobs system

    // Start is called when the script is first run
    void Start()
    {
        // Initialize array with obstacle prefabs
        obstacles = new GameObject[] { obstacle1, obstacle2, obstacle3 };

        // Find ScoreManager instance in the scene
        scoreManager = FindObjectOfType<ScoreManager>();

        // Initialize timeBetweenSpawn with the initial value
        timeBetweenSpawn = initialTimeBetweenSpawn;

        // Initialize TransformAccessArray
        transformAccessArray = new TransformAccessArray(0, -1);
    }

    // OnDestroy is called when the object is destroyed
    void OnDestroy()
    {
        // Dispose of TransformAccessArray if it is created
        if (transformAccessArray.isCreated)
        {
            transformAccessArray.Dispose();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Adjust game difficulty based on the score
        AdjustDifficulty();

        // Check if it's time to spawn a new obstacle
        if (Time.time > spawnTime)
        {
            SpawnObstacle();
            spawnTime = Time.time + timeBetweenSpawn; // Set the time for the next spawn
        }

        // If useJobs is true, use Unity Jobs system for moving obstacles
        if (useJobs)
        {
            // Update TransformAccessArray with current obstacles
            UpdateTransformAccessArray();

            // Create and schedule the job for moving obstacles
            MoveObstaclesJob moveObstaclesJob = new MoveObstaclesJob
            {
                deltaTime = Time.deltaTime,
                obstacleSpeed = obstacleSpeed
            };
            JobHandle moveObstaclesJobHandle = moveObstaclesJob.Schedule(transformAccessArray);
            moveObstaclesJobHandle.Complete(); // Complete the job
        }
        else
        {
            // Move obstacles manually if Jobs system is not used
            for (int i = spawnedObstacles.Count - 1; i >= 0; i--)
            {
                if (spawnedObstacles[i] != null)
                {
                    // Move obstacle left
                    spawnedObstacles[i].transform.position += new Vector3(-obstacleSpeed * Time.deltaTime, 0, 0);

                    // Additional computation to stress the CPU
                    float value = 0f;
                    for (int j = 0; j < 1000; j++)
                    {
                        value = math.exp10(math.sqrt(value));
                    }

                    // Remove obstacles that have moved off-screen
                    if (spawnedObstacles[i].transform.position.x < -10)
                    {
                        Destroy(spawnedObstacles[i]);
                        spawnedObstacles.RemoveAt(i);
                    }
                }
            }
        }
    }

    // Method to spawn a new obstacle
    void SpawnObstacle()
    {
        float randomX = UnityEngine.Random.Range(minX, maxX); // Random X coordinate for obstacle
        float randomY = UnityEngine.Random.Range(minY, maxY); // Random Y coordinate for obstacle

        // Randomly select an obstacle prefab
        int randomIndex = UnityEngine.Random.Range(0, obstacles.Length);
        GameObject randomObstacle = obstacles[randomIndex];

        // Instantiate and position the new obstacle
        GameObject spawnedObstacle = Instantiate(randomObstacle, transform.position + new Vector3(randomX, randomY, 0), Quaternion.identity);

        // Add the new obstacle to the list
        spawnedObstacles.Add(spawnedObstacle);
    }

    // Method to adjust game difficulty based on the score
    void AdjustDifficulty()
    {
        if (scoreManager != null)
        {
            float score = scoreManager.score;
            // Increase difficulty based on score (extreme values for stress testing)
            float difficultyFactor = Mathf.Pow(score * 12000000 * 32000f, 1.5f);
            timeBetweenSpawn = Mathf.Max(0.0000000000000001f, initialTimeBetweenSpawn - difficultyFactor);
        }
    }

    // Method to update TransformAccessArray with the current obstacles
    void UpdateTransformAccessArray()
    {
        // Dispose of the existing TransformAccessArray
        if (transformAccessArray.isCreated)
        {
            transformAccessArray.Dispose();
        }

        // Recreate TransformAccessArray with the current list of obstacles
        transformAccessArray = new TransformAccessArray(spawnedObstacles.Count);
        for (int i = 0; i < spawnedObstacles.Count; i++)
        {
            if (spawnedObstacles[i] != null)
            {
                transformAccessArray.Add(spawnedObstacles[i].transform);
            }
        }
    }

    // BurstCompile attribute optimizes the job for faster execution
    [BurstCompile]
    public struct MoveObstaclesJob : IJobParallelForTransform
    {
        public float deltaTime; // Time elapsed since last frame
        public float obstacleSpeed; // Speed at which obstacles move

        // Method that is called for each transform in TransformAccessArray
        public void Execute(int index, TransformAccess transform)
        {
            // Move the obstacle
            transform.position += new Vector3(-obstacleSpeed * deltaTime, 0, 0);

            // Additional computation to stress the CPU
            float value = 0f;
            for (int i = 0; i < 1000; i++)
            {
                value = math.exp10(math.sqrt(value));
            }
        }
    }
}
