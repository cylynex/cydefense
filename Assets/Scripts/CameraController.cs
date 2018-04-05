using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [Header("Attributes")]
    public float panSpeed = 30f;
    public float panBorderThickness = 10f;
    public float scrollSpeed = 5f;
    public float minY = 10f;
    public float maxY = 80f;

    [Header("Camera")]
    public Vector2 zoomRange = new Vector2(-50, 100);
    public float CurrentZoom = 0;
    public float ZoomSpeed = 1;
    public float ZoomRotation = 1;
    public Vector2 zoomAngleRange = new Vector2(20, 70);
    public float rotateSpeed = 10;

    private Vector3 initialPosition;
    private Vector3 initialRotation;


    void Update() {

        if (Input.GetKey("w") || Input.mousePosition.y >= Screen.height * panBorderThickness) {
            Debug.Log("up");
            transform.Translate(Vector3.back * panSpeed * Time.deltaTime, Space.World);
        }
            if (Input.GetKey("s") || Input.mousePosition.y >= Screen.height * panBorderThickness) {
            transform.Translate(Vector3.forward * panSpeed * Time.deltaTime, Space.World);
        }
            if (Input.GetKey("a") || Input.mousePosition.x >= Screen.width * panBorderThickness) {
            transform.Translate(Vector3.right * panSpeed * Time.deltaTime, Space.World);
        }
            if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width * panBorderThickness) {
            transform.Translate(Vector3.left * panSpeed * Time.deltaTime, Space.World);
        }

        // Mouse scroll zoom
        /*
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        Debug.Log(scroll); 
        Vector3 position = transform.position;
        position.y -= scroll * 1000 * scrollSpeed * Time.deltaTime;

        // restrict Y
        position.y = Mathf.Clamp(position.y, minY, maxY);
        transform.position = position;
        */


        // zoom in/out
        CurrentZoom -= Input.GetAxis("Mouse ScrollWheel") * Time.deltaTime * 1000 * ZoomSpeed;
        CurrentZoom = Mathf.Clamp(CurrentZoom, zoomRange.x, zoomRange.y);
        transform.position = new Vector3(transform.position.x, transform.position.y - (transform.position.y - (initialPosition.y + CurrentZoom)) * 0.1f, transform.position.z);

        float x = transform.eulerAngles.x - (transform.eulerAngles.x - (initialRotation.x + CurrentZoom * ZoomRotation)) * 0.1f;
        x = Mathf.Clamp(x, zoomAngleRange.x, zoomAngleRange.y);

        transform.eulerAngles = new Vector3(x, transform.eulerAngles.y, transform.eulerAngles.z);

    }




}
