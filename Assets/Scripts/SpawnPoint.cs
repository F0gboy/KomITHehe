using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{

    private BlockadeController _blockadeController;
    
    public GameObject[] SpawnPoints;
    
    [SerializeField] private GameObject Player;
    
    private OpenExitFound _openExitFound;

    public static Vector3 spawnPosition { get; private set; } = Vector3.zero;



    // Start is called before the first frame update
    void Start()
    {
        _openExitFound = FindObjectOfType<OpenExitFound>();
        _blockadeController = FindObjectOfType<BlockadeController>();

        Player.transform.position = RandomSpawnPoint();
        _blockadeController.StartCreateObstacles();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) Player.transform.position = RandomSpawnPoint();
    }

    public void RestartLevel()
    {
        _openExitFound.SetUiState(false);
        Player.GetComponent<FirstPersonController>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        _openExitFound.StopSpin();
        
        Player.transform.position = RandomSpawnPoint();
        _blockadeController.StartCreateObstacles();
    }
    
    private Vector3 RandomSpawnPoint()
    {
       var ChosenSpawn = Random.Range(0, SpawnPoints.Length);
       var pos = SpawnPoints[ChosenSpawn].transform.position;
       spawnPosition = pos;
       return pos;

    }
}