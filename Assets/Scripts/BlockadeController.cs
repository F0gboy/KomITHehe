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
        deactivateBlockade();
        navMeshPath = new NavMeshPath();
        _spawnPoint = FindObjectOfType<SpawnPoint>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            StartCoroutine(CreateObstacles());
            
            //closestExit = GetClosestExit(exits.ToList());
            
            //print(closestExit.distance + " " + closestExit.exit.name);
        }
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

    private void deactivateBlockade()
    {
        foreach (var blockade in blockades)
        {
            blockade.SetActive(false);
        }
    }
    
    public void StartCreateObstacles()
    {
        StartCoroutine(CreateObstacles());
    }
    
    IEnumerator CreateObstacles()
    {
        foreach (var blockade in activeBlockade)
        {
            blockade.SetActive(false);
        }
        
        activeBlockade.Clear();
        
        const float chance = 0.2f;

        foreach (var blockade in blockades)
        {
            var rand = Random.Range(0f, 1f);
            if (rand > chance) continue;
            
            activeBlockade.Add(blockade);
            
            blockade.SetActive(true);
        }

        /*
        // Build the NavMesh sources array, including the obstacles
        var sources = new List<NavMeshBuildSource>();
        NavMeshBuilder.CollectSources(obj.GetComponent<Collider>().bounds, 
            LayerMask.GetMask("Obstacle"), 
            NavMeshCollectGeometry.PhysicsColliders,
            0, 
            new List<NavMeshBuildMarkup>(), 
            sources);

        // Update the NavMesh asynchronously
        var operation = NavMeshBuilder.UpdateNavMeshDataAsync(
            new NavMeshData(NavMesh.AllAreas), // Update the entire NavMesh
            NavMesh.GetSettingsByID(0), // Use default NavMesh settings
            sources,
            obj.GetComponent<Collider>().bounds); // Bounds of the NavMesh to update

        // Wait for the NavMesh update to complete
        while (!operation.isDone)
        {
            yield return null;
        }
        */

        closestExit = GetClosestExit(exits.ToList());
        if (closestExit.exit == null)
        {
            StartCoroutine(CreateObstacles());
            yield break;
        }
        print(closestExit.distance + " " + closestExit.exit.name);
    }
}
