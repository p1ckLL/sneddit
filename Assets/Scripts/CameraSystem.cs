using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSystem : MonoBehaviour
{
    public bool isOpen = false;
    public int currentCam = 1;

    public AI aiInfo;

    public Transform Locations;
    public Transform CameraButtons;
    public Transform AudioLureButton;

    public void uiInit(bool open)
    {
        CameraButtons.gameObject.SetActive(open);
        AudioLureButton.gameObject.SetActive(open);
        Locations.GetChild(currentCam - 1).gameObject.SetActive(open);
    }

    public void OpenCam()
    {
        isOpen = !isOpen;
        uiInit(isOpen);

        if (aiInfo.isStunned)
        {
            AudioLureButton.gameObject.SetActive(false);
        }
    }

    public void CamSwitch(int cam)
    {
        currentCam = cam;
        for (int i = 0; i < Locations.transform.childCount; i++)
        {
            Locations.GetChild(i).gameObject.SetActive(false);
        }
        Locations.GetChild(currentCam - 1).gameObject.SetActive(true);
    }
}
