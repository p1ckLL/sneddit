using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickDetector : MonoBehaviour
{
    public Transform doorLights;
    public AI aiInfo;

    private bool debounce = false;
    private bool debounce2 = false;
    private bool doorDebounce = false;
    public bool doorOn = false;
    public bool lightsOn = false;

    public Transform physicalDoor;
    public Transform doorEnd;

    public Transform physicalSneddy;
    public Transform darkDoor;

    public AudioSource scaryDoor;

    IEnumerator debounceTimer(float secs, Action after)
    {
        yield return new WaitForSeconds(secs);
        after();
    }

    IEnumerator doorClose(bool open)
    {
        Vector3 pos = physicalDoor.position;
        if (open)
        {
            for (float i = physicalDoor.position.y; i > 8.65; i -= 0.1f)
            {
                pos.y = i;
                physicalDoor.position = pos;
                yield return new WaitForSeconds(0.005f);
            }
        }
        if (!open)
        {
            for (float i = physicalDoor.position.y; i < 20.65; i += 0.1f)
            {
                pos.y = i;
                physicalDoor.position = pos;
                yield return new WaitForSeconds(0.005f);
            }
        }
    }
    

    void Update()
    {

        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Physics.Raycast(ray, out hit);
            if (hit.collider.tag == "Light" && debounce == false)
            {
                debounce = true;
                lightsOn = !lightsOn;
                doorLights.gameObject.SetActive(lightsOn);
                darkDoor.gameObject.SetActive(!lightsOn);
                StartCoroutine(debounceTimer(0.5f, delegate { debounce = false; }));
            }
            if (aiInfo.currentLocation == 1)
            {
                physicalSneddy.gameObject.SetActive(lightsOn);
            }
            if (lightsOn == true && debounce2 == false && aiInfo.currentLocation == 1)
            {
                debounce2 = true;
                StartCoroutine(debounceTimer(10f, delegate { debounce2 = false; }));
                scaryDoor.time = 1.5f;
                scaryDoor.Play();
            }
            if (hit.collider.tag == "Door" && doorDebounce == false)
            {
                doorDebounce = true;
                StartCoroutine(debounceTimer(1.0f, delegate { doorDebounce = false; }));
                doorOn = !doorOn;
                StartCoroutine(doorClose(doorOn));
            }
        }
    }
}
