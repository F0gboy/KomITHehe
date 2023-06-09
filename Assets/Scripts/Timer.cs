using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static bool timerActive = false;
    public static float time = 0;
    private bool updated;

    private TMP_Text text;
    
    private SpawnPoint _spawnPoint;
    
    void Start()
    {
        text = GetComponent<TMP_Text>();
        StartCoroutine(Counter());
        _spawnPoint = FindObjectOfType<SpawnPoint>();
    }

    private void Update()
    {
        if (time == 0 && !timerActive && !updated)
        {
            updated = true;
            UpdateText();
        }
    }

    IEnumerator Counter()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            if (timerActive)
            {
                if (updated) updated = false;
                
                _spawnPoint.Player.GetComponent<AudioSource>().Play();
                
                time++;
                UpdateText();
            }
        }
    }

    private void UpdateText()
    {
        text.text = time.ToString();
    }
}
