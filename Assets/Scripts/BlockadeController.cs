using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class BlockadeController : MonoBehaviour
{
    public GameObject[] blockades;
    public GameObject[] exits;
    private static NavMeshPath navMeshPath;

    public GameObject obj;

    private List<GameObject> activeBlockade = new();

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


    public Exit GetClosestExit(List<GameObject> exits)
    {
        var closestExit = exits.OrderBy(e => {
            NavMesh.CalculatePath(SpawnPoint.spawnPosition, e.transform.position, NavMesh.AllAreas, navMeshPath);
            var totalDistance = 0f;
            for (int i = 1; i < navMeshPath.corners.Length; i++)
            {
                totalDistance += Vector3.Distance(navMeshPath.corners[i - 1], navMeshPath.corners[i]);
            }
            return totalDistance;
        }).FirstOrDefault();
    
        var exitStruct = new Exit();
        exitStruct.exit = closestExit;
        exitStruct.distance = Vector3.Distance(SpawnPoint.spawnPosition, closestExit.transform.position);

        return exitStruct;
        
        /*var closestDist = float.MinValue;
        GameObject closestExit = null;
        
        foreach (var exit in exits)
        {
            NavMesh.CalculatePath(SpawnPoint.spawnPosition, exit.transform.position, NavMesh.AllAreas, navMeshPath);

            var totalDistance = 0f;
            
            for (int i = 1; i < navMeshPath.corners.Length; i++)
            {
                totalDistance += Vector3.Distance(navMeshPath.corners[i - 1], navMeshPath.corners[i]);
            }

            if (!(totalDistance < closestDist)) continue;
            
            closestDist = totalDistance;
            closestExit = exit;
        }
        
        var exitStruct = new Exit();
        exitStruct.exit = closestExit;
        exitStruct.distance = closestDist;

        return exitStruct;
        */
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
