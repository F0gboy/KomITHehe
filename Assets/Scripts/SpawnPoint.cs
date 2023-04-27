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
        BlockadeController.closestExit = _blockadeController.GetClosestExit(_blockadeController.exits.ToList());
        print(BlockadeController.closestExit.distance + " " + BlockadeController.closestExit.exit.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) Player.transform.position = RandomSpawnPoint();
    }

    public void RestartLevel(bool respawn = true)
    {
        _openExitFound.wall.SetActive(true);
        _openExitFound.SetUiState(false);
        Player.GetComponent<FirstPersonController>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        
        _openExitFound.StopSpin();

        /*
        foreach (var exit in _blockadeController.exits.Where(x => !x.GetComponent<Udgang>().active))
        {
            exit.GetComponent<Udgang>().active = true;
        }
        */

        if (respawn) return;
        
        if (OpenExitFound.CorrectExit) Player.transform.position = RandomSpawnPoint();
        else
        {
            Player.transform.position = spawnPosition;
            Timer.timerActive = true;
        }
        BlockadeController.closestExit = _blockadeController.GetClosestExit(_blockadeController.exits.ToList());
        print(BlockadeController.closestExit.distance + " " + BlockadeController.closestExit.exit.name);
    }
    
    private Vector3 RandomSpawnPoint()
    {
        if (OpenExitFound.CorrectExit)
        {
            OpenExitFound.CorrectExit = false;
            Timer.time = 0;
        }
        
       Timer.timerActive = true;
       var ChosenSpawn = Random.Range(0, SpawnPoints.Length);
       var pos = SpawnPoints[ChosenSpawn].transform.position;
       spawnPosition = pos;
       return pos;

    }
}
