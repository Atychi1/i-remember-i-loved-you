using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class Object_Interact : MonoBehaviour
{
    public GameObject offset;
    public GameObject targetObject;
    public GameObject tableObject;
    public Canvas canvas;

    public bool isExamining = false;

    private Vector3 lastMousePosition;
    private Transform examinedObject;
    private readonly float examineDistanceThreshold = 2f;

    private readonly Dictionary<Transform, Vector3> originalPositions = new Dictionary<Transform, Vector3>();
    private readonly Dictionary<Transform, Quaternion> originalRotations = new Dictionary<Transform, Quaternion>();

    void Start()
    {
        canvas.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleExamination();
            if (isExamining)
            {
                StartExamination();
            }
            else
            {
                StopExamination();
            }
        }

        if (CheckUserClose())
        {
            if (isExamining)
            {
                canvas.enabled = false;
                Examine();
            }
            else
            {
                canvas.enabled = true;
                NonExamine();
            }
        }
        else
        {
            canvas.enabled = false;
        }
    }

    void ToggleExamination()
    {
        isExamining = !isExamining;
    }

    void StartExamination()
    {
        lastMousePosition = Input.mousePosition;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void StopExamination()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Examine()
    {
        if (examinedObject != null)
        {
            examinedObject.position = Vector3.Lerp(examinedObject.position, offset.transform.position, 0.2f);

            Vector3 deltaMouse = Input.mousePosition - lastMousePosition;
            float rotationSpeed = 1.0f;
            examinedObject.Rotate(deltaMouse.x * rotationSpeed * Vector3.up, Space.World);
            examinedObject.Rotate(deltaMouse.y * rotationSpeed * Vector3.left, Space.World);
            lastMousePosition = Input.mousePosition;
        }
    }

    void NonExamine()
    {
        if (examinedObject != null && originalPositions.ContainsKey(examinedObject) && originalRotations.ContainsKey(examinedObject))
        {
            examinedObject.position = Vector3.Lerp(examinedObject.position, originalPositions[examinedObject], 0.2f);
            examinedObject.rotation = Quaternion.Slerp(examinedObject.rotation, originalRotations[examinedObject], 0.2f);
        }
    }

    bool CheckUserClose()
    {
        float distance = Vector3.Distance(targetObject.transform.position, tableObject.transform.position);
        return distance < examineDistanceThreshold;
    }
}
