using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CounterScript : MonoBehaviour
{
    public int countedRun = 0;
    public Text counterText;

    void Update()
    {
        counterText.text = "Current Run: " + countedRun;
    }
}
