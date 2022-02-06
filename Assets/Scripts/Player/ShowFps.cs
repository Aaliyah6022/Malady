using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowFps : MonoBehaviour
{
    public GameObject FPSCanvas;

    void Start()
    {
        FPSCanvas.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F8))
        {
            bool isActive = FPSCanvas.activeSelf;
            FPSCanvas.SetActive(!isActive);
        }
    }
}
