using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OpenExitFound : MonoBehaviour
{
    private BlockadeController.Exit Closest;
    private BlockadeController.Exit Chosen;

    private BlockadeController.Exit lookingAt;
    
    public Button button;
    
    public void OpenUi(BlockadeController.Exit closest, BlockadeController.Exit chosen)
    {
        Closest = closest;
        Chosen = chosen;

        lookingAt = Chosen;

        for (int i = 0; i < transform.childCount; i++)
        {
            var text = transform.GetChild(i).GetComponent<TMP_Text>();
            text.enabled = true;
            
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
            button.GetComponent<TMP_Text>().text = "Se tætteste";
            lookingAt = Closest;
        }
        else
        {
            button.GetComponent<TMP_Text>().text = "Se dit valg";
            lookingAt = Chosen;
        }
    }
}
