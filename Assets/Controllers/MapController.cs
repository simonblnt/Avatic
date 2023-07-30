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
    private TopDownController2D playerController;
    private readonly uint chunkWidth = 20;
    private readonly uint chunkHeight = 20;


    // Start is called before the first frame update
    void Start()
    {
        levelManager = levelManagerObject.GetComponent<LevelManager>();
        checkerRadius = chunkWidth / 100;
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
            playerMovement = playerController.CurrentMovement;
            CheckChunks();
        }        
    }
    void CheckChunks()
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
            var goalPos = playerGoalPoint.position;
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
        Instantiate(terrainChunks[rand], terrainSpawnPosition, Quaternion.identity);
    }
}
