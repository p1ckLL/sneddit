using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkside : MonoBehaviour
{
    [SerializeField] CameraSystem cameraSystem;

    void Update()
    {
        Vector2 mouse = new Vector2(Input.mousePosition.x, Screen.height - Input.mousePosition.y);

        if (mouse.x < Screen.width / 1.5 && cameraSystem.isOpen == false)
        {
            transform.Rotate(new Vector3(0, -1f, 0) * Time.deltaTime * 75);
        }

        if (mouse.x > Screen.width / 2.5 && cameraSystem.isOpen == false)
        {
            transform.Rotate(new Vector3(0, 1f, 0) * Time.deltaTime * 75);
        }
    }
}
