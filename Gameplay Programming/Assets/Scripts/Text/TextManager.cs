using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextManager : MonoBehaviour
{

    public string message = "";
    public int font_size;
   /* public Color color;*/
    bool displaying = false;
    bool updated = false;

    public TextMeshProUGUI canvasText;

    private void Update()
    {
        UpdateText();
    }
    void UpdateText()
    {
        if(!updated && displaying)
        {
            /*canvasText.color = color;*/
            canvasText.fontSize = font_size;
            canvasText.text = message;
            updated = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (!displaying)
            {
                displaying = true;
                canvasText.enabled = true;
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (displaying)
            {
                displaying = false;
                canvasText.enabled = false;
                updated = false;
            }
        }
    }
}
