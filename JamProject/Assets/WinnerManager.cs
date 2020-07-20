using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinnerManager : MonoBehaviour
{
    [SerializeField] PlayerController winner;
    [SerializeField] TextMeshProUGUI congrats;
    
    void Start()
    {
        congrats.text = string.Format("Congratulations, {0}!", winner.name);
    }
}
