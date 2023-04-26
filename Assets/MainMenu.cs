using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject InformationMenu;
    
    // Start is called before the first frame update
    void Start()
    {
        InformationMenu.SetActive(false);
    }

    public void informationPress()
    {
        InformationMenu.SetActive(true);
        gameObject.SetActive(false);
    }
    
    public void exitPress()
    {
        Application.Quit();
    }
    
    public void startPress()
    {
        gameObject.SetActive(false);
        SceneManager.LoadScene(1);
    }
}
