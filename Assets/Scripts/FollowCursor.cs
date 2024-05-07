using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCursor : MonoBehaviour
{
    // Sensitivity of camera movement towards the cursor
    public float sensitivity = 0.05f;

    // Update is called once per frame
    void Update()
    {
        // Get the position of the cursor in screen space
        Vector3 cursorPos = Input.mousePosition;

        // Adjust the camera's position based on the cursor's position
        transform.position = new Vector3(cursorPos.x * sensitivity, cursorPos.y * sensitivity, transform.position.z);
    }
}
