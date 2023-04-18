using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    
    [SerializeField] private GameObject[] ScienceSpawnPoints;
    [SerializeField] private GameObject[] GangSpawnPoints;
    
    [SerializeField] private List<GameObject[]> SpawnPoints = new();
    
    [SerializeField] private GameObject Player;

   
    
    // Start is called before the first frame update
    void Start()
    {
        SpawnPoints.Add(ScienceSpawnPoints);
        SpawnPoints.Add(GangSpawnPoints);

        Player.transform.position = RandomSpawnPoint();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 RandomSpawnPoint()
    {
       var ChosenSpawn = Random.Range(0, SpawnPoints.Count);
       return SpawnPoints[ChosenSpawn][Random.Range(0, SpawnPoints[ChosenSpawn].Length)].transform.position;

    }
}
