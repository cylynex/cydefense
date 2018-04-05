using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [Header("Attributes")]
    public float panSpeed = 30f;

    // The world-position of the mouse last frame.
    Vector3 lastFramePosition;
    Vector3 currFramePosition;

    void Update() {
        /*
        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height - 10) {
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
        */

        UpdateCameraMovement();

        // Save the mouse position from this frame
        lastFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lastFramePosition.z = 0;

    }


    void UpdateCameraMovement() {
        // movement
        if (Input.GetMouseButton(1)) {   // Right Mouse Button

            Vector3 diff = lastFramePosition - currFramePosition;
            Camera.main.transform.Translate(diff);

        }

        Camera.main.orthographicSize -= Camera.main.orthographicSize * Input.GetAxis("Mouse ScrollWheel");

        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 3f, 25f);
    }

}
