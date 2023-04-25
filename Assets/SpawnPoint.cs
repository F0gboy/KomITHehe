using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    
    [SerializeField] private GameObject[] ScienceSpawnPoints;
    [SerializeField] private GameObject[] GangSpawnPoints;
    
    private BlockadeController _blockadeController;
    
    public List<GameObject> SpawnPoints = new();
    
    [SerializeField] private GameObject Player;

    public Vector3 spawnPosition { get; private set; } = Vector3.zero;



    // Start is called before the first frame update
    void Start()
    {
        _blockadeController = FindObjectOfType<BlockadeController>();
        
        foreach (var spawnPoint in ScienceSpawnPoints)
            SpawnPoints.Add(spawnPoint);

        foreach (var spawnPoint in GangSpawnPoints)
            SpawnPoints.Add(spawnPoint);
        
        Player.transform.position = RandomSpawnPoint();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) Player.transform.position = RandomSpawnPoint();
    }

    private Vector3 RandomSpawnPoint()
    {
       var ChosenSpawn = Random.Range(0, SpawnPoints.Count);
       var pos = SpawnPoints[ChosenSpawn].transform.position;
       spawnPosition = pos;
       return pos;

    }
}
