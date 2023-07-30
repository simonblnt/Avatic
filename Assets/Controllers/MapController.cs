using MoreMountains.TopDownEngine;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MapController : MonoBehaviour
{
    public List<GameObject> terrainChunks;
    public GameObject player; // TowDown Controller 2D - Current Movement values
    public GameObject levelManagerObject;
    public GameObject currentChunk;


    private LevelManager levelManager;
    private float checkerRadius;
    Vector3 noTerrainPosition;
    public LayerMask terrainMask;
    public Vector3 playerMovement;
    public Vector3 playerPosition;
    private TopDownController2D playerController;
    private readonly uint chunkSize = 20;
    Vector3 goalPos;

    [Header("Optimization")]
    public List<GameObject> spawnedChunks;
    GameObject latestChunk;
    public float maxOpDist;
    public float opDist;


    // Start is called before the first frame update
    void Start()
    {
        levelManager = levelManagerObject.GetComponent<LevelManager>();
        checkerRadius = 0.2f;
        maxOpDist = 50;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController == null)
        {
            playerController = levelManager.Players[0].GetComponent<TopDownController2D>();
        }
        else
        {
            playerPosition = levelManager.Players[0].transform.position;
            playerMovement = playerController.CurrentMovement;
            UpdateChunks();
            OptimizeChunks();
        }        
    }
    void UpdateChunks()
    {
        if (!currentChunk)
        {
            return;
        }

        string positionStr = "Idle";
        switch (playerMovement.x) // right
        {
            case > 0 when playerMovement.y == 0:
                positionStr = "Right";
                break;
            case < 0 when playerMovement.y == 0:
                positionStr = "Left";
                break;
            case 0 when playerMovement.y > 0:
                positionStr = "Up";
                break;
            case 0 when playerMovement.y < 0:
                positionStr = "Down";
                break;
            case < 0 when playerMovement.y > 0:
                positionStr = "LeftUp";
                break;
            case > 0 when playerMovement.y > 0:
                positionStr = "RightUp";
                break;
            case < 0 when playerMovement.y < 0:
                positionStr = "LeftDown";
                break;
            case > 0 when playerMovement.y < 0:
                positionStr = "RightDown";
                break;
        }

        if (positionStr != "Idle")
        {
            var playerGoalPoint = currentChunk.transform.Find(positionStr);
            goalPos = playerGoalPoint.position;
            if (playerGoalPoint != null && !CheckForTerrain(goalPos))
            {
                SpawnChunk(goalPos);
            }
        }     
    }

    bool CheckForTerrain(Vector3 searchPoint)
    {
        if (!Physics2D.OverlapCircle(searchPoint, checkerRadius, terrainMask))
            return false;
        else
            return true;
    }

    void SpawnChunk(Vector3 terrainSpawnPosition)
    {        
        int rand = Random.Range(0, terrainChunks.Count);
        latestChunk = Instantiate(terrainChunks[rand], terrainSpawnPosition, Quaternion.identity);
        spawnedChunks.Add(latestChunk);
    }

    void OptimizeChunks()
    {
        foreach (var chunk in spawnedChunks)
        {
            opDist = Vector3.Distance(playerPosition, chunk.transform.position);
            if (opDist > maxOpDist)
                chunk.SetActive(false);
            else
                chunk.SetActive(true);
        }
    }
}
