using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioLure : MonoBehaviour
{
    public bool debounce = false;
    [SerializeField] AudioSource laraCroft;

    public AI aiInfo;
    public CameraSystem camInfo;

    IEnumerator debounceHandler(Action after)
    {
        yield return new WaitForSeconds(15);
        after();
    }

    public bool effectivity(float camLocation)
    {
        float max = Mathf.Max(aiInfo.currentLocation, camLocation);
        float min = Mathf.Min(aiInfo.currentLocation, camLocation);
        float effectiveness = 6 / (max-min);
        float luck = UnityEngine.Random.Range(0, 6);
        print(effectiveness);
        print(luck);
        return luck <= effectiveness;
    }

    public void lure(GameObject lureButton)
    {
        StartCoroutine(debounceHandler(delegate { lureButton.SetActive(true); }));
        lureButton.SetActive(false);
        laraCroft.Play();
        int currentCamLocation = camInfo.currentCam;
        bool success = effectivity(currentCamLocation);
        
        if (success)
        {
            aiInfo.stun(currentCamLocation);
        }
        else
        {
            return;
        }

    }
}
