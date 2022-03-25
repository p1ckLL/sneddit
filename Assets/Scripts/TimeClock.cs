using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeClock : MonoBehaviour
{
    public AI aiInfo;
    public int TOD = 12;
    public Text timeText;
    public Text nightText;
    public Text sixamtext;
    public int night = 1;

    public AudioSource night1;
    public AudioSource night2;
    public AudioSource night3;
    public AudioSource night4;
    public AudioSource night5;

    private void Awake()
    {
        int loadedNight = PlayerPrefs.GetInt("Night");
        print("Data loaded. Night: " + loadedNight);
        if (loadedNight != 0)
        {
            night = loadedNight;
        }
        nightText.text = "Night " + night;
    }

    IEnumerator wait()
    {
        yield return new WaitForSeconds(5f);
    }

    void Start()
    {

        switch (night)
        {
            case 1:
                night1.Play();
                aiInfo.snedy = 4;
                aiInfo.movementSpeed = 9f;
                break;
            case 2:
                night2.Play();
                aiInfo.snedy = 6;
                aiInfo.movementSpeed = 8f;
                break;
            case 3:
                night3.Play();
                aiInfo.snedy = 8;
                aiInfo.movementSpeed = 7f;
                break;
            case 4:
                night4.Play();
                aiInfo.snedy = 10;
                aiInfo.movementSpeed = 6.5f;
                break;
            case 5:
                night5.Play();
                aiInfo.snedy = 12;
                aiInfo.movementSpeed = 5f;
                break;
        }

        InvokeRepeating("changeTime", 60.0f, 60.0f);
    }

    public void changeTime()
    {
        if (TOD == 12)
        {
            TOD = 1;
        }
        else if (TOD == 5)
        {
            print("you won");
            sixamtext.gameObject.SetActive(true);
            night++;
            PlayerPrefs.SetInt("Night", night);
            print("game data saved");
            Task t = new Task(wait());
            t.Finished += delegate
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            };
        }
        else
        {
            TOD++;
        }
        timeText.text = TOD + " AM";
        aiInfo.snedy++;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PlayerPrefs.SetInt("Night", 0);
        }
    }
}
