using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Power : MonoBehaviour
{
    public int power = 100;
    public int usage = 1;
    public bool doorApplyingUsage = false;
    public bool lightApplyingUsage = false;
    public bool camApplyingUsage = false;

    public AI aiInfo;

    public ClickDetector clickDetectorInfo;
    public CameraSystem cameraSystemInfo;

    public Text powerText;

    IEnumerator Drain()
    {
        while (true)
        {
            power -= usage;
            yield return new WaitForSeconds(10f);
        }
    }

    void Start()
    {
        StartCoroutine(Drain());
    }

    void Update()
    {
        if (clickDetectorInfo.doorOn && !doorApplyingUsage)
        {
            doorApplyingUsage = true;
            power -= 2;
            usage += 1;
        } else if (!clickDetectorInfo.doorOn && doorApplyingUsage)
        {
            doorApplyingUsage = false;
            usage -= 1;
        }

        if (clickDetectorInfo.lightsOn && !lightApplyingUsage)
        {
            lightApplyingUsage = true;
            power -= 2;
            usage += 1;
        }
        else if (!clickDetectorInfo.doorOn && doorApplyingUsage)
        {
            lightApplyingUsage = false;
            usage -= 1;
        }

        if (cameraSystemInfo.isOpen && !camApplyingUsage)
        {
            camApplyingUsage = true;
            power -= 1;
            usage += 1;
        }
        else if (!cameraSystemInfo.isOpen && camApplyingUsage)
        {
            camApplyingUsage = false;
            usage -= 1;
        }

        if (power <= 0)
        {
            aiInfo.jumpscare();
        }

        powerText.text = "Power: " + power + "%";
    }
}
