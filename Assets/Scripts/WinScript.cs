using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScript : MonoBehaviour
{

    public Text t;

    // Set Text when touching the flag
    void OnTriggerEnter2D(Collider2D other)
    {
        t.text = "You win!";
    }
}
