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

    public Camera playerCam;
    public Camera exitCam;

    public CameraRotation _cameraRotation;
    
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
            Titel.color = Color.green;
            Titel.text = "Du fandt udgangen!";

            Score.enabled = true;
            Score.text = "Score: " + (Chosen.distance / Timer.time).ToString("F2");
            CorrectExit = true;
        }
        else
        {
            Score.enabled = false;
            Titel.color = Color.red;
            Titel.text = "Du fandt en udgang!";
        }
    }

    public void OpenUi(BlockadeController.Exit closest, BlockadeController.Exit chosen)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        Timer.timerActive = false;
        
        exitCam.gameObject.SetActive(true);
        playerCam.gameObject.SetActive(false);

        Closest = closest;
        Chosen = chosen;

        lookingAt = Chosen;
        
        SetRotaionTarget();

        _cameraRotation.isMoving = true;
        
        SetUiState(true);

        for (int i = 0; i < transform.childCount; i++)
        {
            var text = transform.GetChild(i).GetComponent<TMP_Text>();

            if (transform.GetChild(i).name == "Valgte")
            {
                text.text = $"Din valgte udgang: {chosen.distance}";
            }
            else if (transform.GetChild(i).name == "Faktiske")
            {
                text.text = $"Den tætteste udgang: {closest.distance}";
            }
        }

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