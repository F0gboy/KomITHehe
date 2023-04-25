using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDetector : MonoBehaviour
{
    OpenExitFound _openExitFound;

    private void Start()
    {
        _openExitFound = FindObjectOfType<OpenExitFound>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ExitBox"))
        {
            _openExitFound.OpenUi(BlockadeController.closestExit, BlockadeController.GetDistance(other.gameObject));

            GetComponent<FirstPersonController>().enabled = false;
        }
    }
}
