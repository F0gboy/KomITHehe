using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using TMPro;
using Unity.Collections.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OpenExitFound : MonoBehaviour
{
    private BlockadeController.Exit Closest;
    private BlockadeController.Exit Chosen;

    private BlockadeController.Exit lookingAt;

    public static bool CorrectExit = false;
    
    public Button button;
    public TMP_Text buttonText;
    
    public TMP_Text Titel;
    public TMP_Text Score;
    
    public TMP_Text valgt;
    public TMP_Text faktisk;
    
    public GameObject wall;
    public GameObject switchView;

    public Camera playerCam;
    public Camera exitCam;

    public CameraRotation _cameraRotation;

    private List<GameObject> wrongExits = new ();
    
    private SpawnPoint _spawnPoint;

    private void Start()
    {
        //_cameraRotation = FindObjectOfType<CameraRotation>();

        exitCam.gameObject.SetActive(false);
        
        _spawnPoint = FindObjectOfType<SpawnPoint>();
        
        SetUiState(false);
    }
    
    public void SetUiState(bool state)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name == "Timer") continue;
            
            transform.GetChild(i).gameObject.SetActive(state);
        }

        if (Closest.exit == Chosen.exit)
        {
            foreach (var exit in wrongExits)
            {
                var udgang = exit.GetComponent<Udgang>();
                udgang.active = true;
                udgang.col.SetActive(false);
            }
            
            switchView.SetActive(false);
            wrongExits.Clear();
            
            Titel.color = Color.green;
            Titel.text = "Du fandt den tætteste udgang!";

            Score.enabled = true;
            Score.text = "Score: " + (Chosen.distance / Timer.time * 100).ToString("F2");
            CorrectExit = true;
        }
        else
        {
            var udgang = Chosen.exit.GetComponent<Udgang>();

            switchView.SetActive(state);
            
            wrongExits.Add(udgang.gameObject);
            udgang.active = false;
            udgang.col.SetActive(true);
            
            Score.enabled = false;
            Titel.color = Color.red;
            Titel.text = "Du fandt ikke den tætteste udgang";

            CorrectExit = false;
        }
    }

    public void OpenUi(BlockadeController.Exit closest, BlockadeController.Exit chosen)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        Timer.timerActive = false;
        
        wall.SetActive(false);

        if (switchView.activeInHierarchy) switchView.GetComponent<Animation>().Play();
        
        exitCam.gameObject.SetActive(true);
        playerCam.gameObject.SetActive(false);

        Closest = closest;
        Chosen = chosen;

        lookingAt = Chosen;
        
        SetRotaionTarget();

        _cameraRotation.isMoving = true;
        
        SetUiState(true);

        valgt.text = $"Din valgte udgang: {chosen.distance:F2}";
        faktisk.text = $"Den tætteste udgang: {closest.distance:F2}";
        //closest.exit.GetComponent<Udgang>().active = false;
    }

    public void StopSpin()
    {
        playerCam.gameObject.SetActive(true);
        exitCam.gameObject.SetActive(false);
    }

    public void ButtonPress()
    {
        if (buttonText.text == "Se tætteste")
        {
            buttonText.text = "Se dit valg";
            lookingAt = Closest;
        }
        else
        {
            buttonText.text = "Se tætteste";
            lookingAt = Chosen;
        }

        if (switchView.GetComponent<Animation>().isPlaying)
        {
            switchView.GetComponent<Animation>().Stop();
            switchView.GetComponent<Image>().color = Color.white;
        }
        
        SetRotaionTarget();
    }

    public void BackToMain()
    {
        _spawnPoint.RestartLevel(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        
        SceneManager.LoadScene(0);
    }

    private void SetRotaionTarget()
    {
        _cameraRotation.target = lookingAt.exit.transform;
        _cameraRotation.MoveToNextTarget();
    }
}