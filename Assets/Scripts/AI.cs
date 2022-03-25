using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AI : MonoBehaviour
{
    public bool isStunned = false;
    public ClickDetector clickDetectorInfo;
    public AudioSource footstepSound;
    public AudioSource jumpscareSound;
    public Transform jumpscareSneddy;
    public Transform uiSneddy;
    public Transform physicalSneddy;
    public int snedy = 4;
    public float movementSpeed = 8f;
    public int currentLocation = 6;
    public Transform Locations;

    void Start()
    {
        InvokeRepeating("move", 30f, movementSpeed);
    }

    IEnumerator stunDebounce()
    {
        yield return new WaitForSeconds(movementSpeed + 2);
        isStunned = false;
    }

    IEnumerator jumpscareTimer()
    {
        yield return new WaitForSeconds(3f);
    }

    IEnumerator jumpscareInterval()
    {
        for (int i = 0; i < 10; i++)
        {
            jumpscareSneddy.localScale += new Vector3(2, 2, 0);
            yield return new WaitForSeconds(0.1f);
            jumpscareSneddy.localScale -= new Vector3(2, 2, 0);
            yield return new WaitForSeconds(0.1f);
        }
        jumpscareSneddy.gameObject.SetActive(false);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator jumpscareRandomizer()
    {
        float randomTime = UnityEngine.Random.Range(3, 7);
        yield return new WaitForSeconds(randomTime);
    }

    public void jumpscare()
    {
        Task randomJumpscareTime = new Task(jumpscareRandomizer());

        randomJumpscareTime.Finished += delegate
        {
            jumpscareSound.Play();
            jumpscareSneddy.gameObject.SetActive(true);
            StartCoroutine(jumpscareInterval());
        };
    }

    public void stun(int currentCamLocation)
    {
        isStunned = true;
        StartCoroutine(stunDebounce());
        currentLocation = currentCamLocation;
        uiSneddy.SetParent(Locations.GetChild(currentLocation - 1));
    }

    void move()
    {
        if (!isStunned)
        {
            int randomNumber = Random.Range(0, 20);
            if (randomNumber <= snedy && currentLocation != 1)
            {
                currentLocation--;
                uiSneddy.SetParent(Locations.GetChild(currentLocation - 1));
                uiSneddy.gameObject.SetActive(true);
            }
            else if (randomNumber <= snedy && currentLocation == 1)
            {
                uiSneddy.gameObject.SetActive(false);
                print("sneddy in hallway");
                footstepSound.Play();
                Task jumpscare_timer = new Task(jumpscareTimer());

                jumpscare_timer.Finished += delegate
                {
                    if (clickDetectorInfo.doorOn)
                    {
                        footstepSound.Play();
                        currentLocation = 4;
                        uiSneddy.SetParent(Locations.GetChild(currentLocation - 1));
                        uiSneddy.gameObject.SetActive(true);
                        physicalSneddy.gameObject.SetActive(false);
                    }
                    else
                    {
                        jumpscare();
                    }
                };
            }
        }
    }
}
