using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OpenExitFound : MonoBehaviour
{
    private BlockadeController.Exit Closest;
    private BlockadeController.Exit Chosen;

    private BlockadeController.Exit lookingAt;
    
    public Button button;
    public TMP_Text buttonText;
    
    public Camera playerCam;
    public Camera exitCam;

    CameraRotation _cameraRotation;

    private void Start()
    {
        _cameraRotation = FindObjectOfType<CameraRotation>();

        exitCam.gameObject.SetActive(false);
        
        SetUiState(false);
    }
    
    private void SetUiState(bool state)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(state);
        }
    }

    public void OpenUi(BlockadeController.Exit closest, BlockadeController.Exit chosen)
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
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
                text.text += $"{chosen.distance:F}";
            }
            else if (transform.GetChild(i).name == "Faktiske")
            {
                text.text += $"{closest.distance:F}";
            }
        }
    }

    public void ButtonPress()
    {
        if (lookingAt.exit == Chosen.exit)
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

    private void SetRotaionTarget()
    {
        _cameraRotation.target = lookingAt.exit.transform;
        _cameraRotation.MoveToNextTarget();
    }
}
