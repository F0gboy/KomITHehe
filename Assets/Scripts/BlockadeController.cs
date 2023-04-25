using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class BlockadeController : MonoBehaviour
{
    public GameObject[] blockades;
    public GameObject[] exits;
    public static NavMeshPath navMeshPath;

    private SpawnPoint _spawnPoint;

    public static Exit closestExit;

    public GameObject testCube;
    
    public struct Exit
    {
        public GameObject exit;
        public float distance;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        navMeshPath = new NavMeshPath();
        _spawnPoint = FindObjectOfType<SpawnPoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            closestExit = GetClosestExit(exits.ToList());
            
            print(closestExit.distance + " " + closestExit.exit.name);
            
            //var testBlock = Instantiate(testCube, closestExit.exit.transform.position, Quaternion.identity);
            //testBlock.name = "Closest Exit";
        }
    }

    public Exit GetClosestExit(List<GameObject> exits)
    {
        var closestDist = 0f;
        GameObject closestExit = null;
        
        foreach (var exit in exits)
        {
            NavMesh.CalculatePath(SpawnPoint.spawnPosition, exit.transform.position, NavMesh.AllAreas, navMeshPath);

            var totalDistance = 0f;
            
            for (int i = 1; i < navMeshPath.corners.Length; i++)
            {
                totalDistance += Vector3.Distance(navMeshPath.corners[i - 1], navMeshPath.corners[i]);
            }

            if (!(totalDistance < closestDist) && closestDist != 0) continue;
            
            closestDist = totalDistance;
            closestExit = exit;
        }
        
        var exitStruct = new Exit();
        exitStruct.exit = closestExit;
        exitStruct.distance = closestDist;

        return exitStruct;
    }

    public static Exit GetDistance(GameObject exit)
    {
        NavMesh.CalculatePath(SpawnPoint.spawnPosition, exit.transform.position, NavMesh.AllAreas, navMeshPath);

        var totalDistance = 0f;
            
        for (int i = 1; i < navMeshPath.corners.Length; i++)
        {
            totalDistance += Vector3.Distance(navMeshPath.corners[i - 1], navMeshPath.corners[i]);
        }

        var exitStruct = new Exit();
        exitStruct.exit = exit;
        exitStruct.distance = totalDistance;

        return exitStruct;
    }
}
