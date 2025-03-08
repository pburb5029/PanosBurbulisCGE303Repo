using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TriggerZone : MonoBehaviour
{
    public TMP_Text output;
    public string textToDisplay;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            output.text = textToDisplay;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
