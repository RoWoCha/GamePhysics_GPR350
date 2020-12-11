using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance = null;

    public Text gameSpeedText;
    public Text cameraSpeedText;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }

    public void UpdateUI()
    {
        gameSpeedText.text = "Game speed: " + GameManager.instance.gameSpeed.ToString("F2") + "x (+/-)";
    }

    public void UpdateCameraSpeedUI(float newSpeed)
    {
        cameraSpeedText.text = "Camera speed: " + newSpeed.ToString("F2") + "x (wheel up/down)";
    }
}
