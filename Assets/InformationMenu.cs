using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class InformationMenu : MonoBehaviour
{
    public GameObject MainMenu;
    
    public void Backbutton()
    {
        MainMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
