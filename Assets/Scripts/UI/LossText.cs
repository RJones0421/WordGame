using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LossText : MonoBehaviour
{

    [SerializeField] private TMP_Text lossText;

    public void SetLossText(bool fell)
    {
        if (fell) lossText.text = "You Fell!";
        else lossText.text = "Time's Up!";
    }
}
