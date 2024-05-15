using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InspectScript : MonoBehaviour
{
    [SerializeField] private float distance; // Raycast max distance
    [SerializeField] private Transform InspectArea; // A gameobject child of the camera where the object will be moved and rotated
    [SerializeField] private float rotationSpeed = 125f;
    [SerializeField] private float dropSpeed = 0.2f;
    [SerializeField] private float pickupSpeed = 0.2f;
    [SerializeField] private bool InvertMouse = false;
    [SerializeField] private string TargetTag = "Object";
    [Header("Using the FPS camera is suggested")]
    [SerializeField] private Transform RayOrigin;
    [Header("Change 'PlayerController' in the code with your own FPS controller script")]
    [SerializeField] private FirstPersonController PlayerControllerScript; // my FPS controller change it with yours

    [SerializeField] private TextMeshProUGUI inspectText;
    [SerializeField] private TextMeshProUGUI putdownText;

    private Vector3 originalPos;
    private Quaternion originalRot; // Add this line to save the original rotation
    private bool Inspecting = false;
    private GameObject InspectedObj;

    public Vector3 targetPosition; // Target position where you want to move the UI parent
    public float moveSpeed = 5f; // Speed at which the UI parent moves

    private GameObject uidesc;

    private RectTransform uidesctransform;

    void Start()
    {
        // Disable the inspect text initially
        inspectText.gameObject.SetActive(false);
        putdownText.gameObject.SetActive(false);
    }

    void Update()
    {
        Vector3 fwdV = RayOrigin.TransformDirection(Vector3.forward);
        Debug.DrawRay(RayOrigin.position, fwdV * distance, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(RayOrigin.position, fwdV, out hit, distance))
        {
            if (hit.transform.CompareTag(TargetTag) && !Inspecting)
            {
                // Enable the inspect text when the ray hits the object
                inspectText.gameObject.SetActive(true);

                if (Input.GetKeyDown(KeyCode.E))
                {
                    InspectedObj = hit.transform.gameObject;
                    originalPos = hit.transform.position;
                    originalRot = hit.transform.rotation; // Save the original rotation
                    Inspecting = true;
                    StartCoroutine(pickupItem());
                    inspectText.gameObject.SetActive(false);
                    putdownText.gameObject.SetActive(true);

                    uidesc = GameObject.Find(InspectedObj.name + "UI");
                    uidesctransform = uidesc.GetComponent<RectTransform>();

                    if (uidesc != null)
                    {
                        uidesctransform.anchoredPosition = new Vector2(500, 0);
                    }
                    else
                    {
                        // Log a message if the GameObject was not found
                        Debug.Log("No GameObject with the name " + InspectedObj.name + "UI was found.");
                    }
                }
            }
            else
            {
                // Disable the inspect text when the ray doesn't hit any object or when inspecting
                inspectText.gameObject.SetActive(false);
            }
        }

        if (Inspecting)
        {
            InspectedObj.transform.position = Vector3.Lerp(InspectedObj.transform.position, InspectArea.position, pickupSpeed);

            if (!InvertMouse)
                InspectArea.Rotate(new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0) * Time.deltaTime * rotationSpeed);
            else
                InspectArea.Rotate(new Vector3(Input.GetAxis("Mouse Y"), -Input.GetAxis("Mouse X"), 0) * Time.deltaTime * rotationSpeed);
        }
        else if (InspectedObj != null)
        {
            InspectedObj.transform.SetParent(null);
            InspectArea.rotation = Quaternion.identity;
            InspectedObj.transform.position = Vector3.Lerp(InspectedObj.transform.position, originalPos, dropSpeed);
        }

        if (Input.GetKeyDown(KeyCode.R) && Inspecting)
        {
            Inspecting = false;
            StartCoroutine(droppItem());
            uidesctransform.anchoredPosition = new Vector2(3000, 0);
            putdownText.gameObject.SetActive(false);
        }
    }

    IEnumerator pickupItem()
    {
        // Collider and rigidbodies cause problems during the inspection so we disable them during it  
        if (InspectedObj.GetComponent<Collider>())
        {
            InspectedObj.GetComponent<Collider>().enabled = false;
        }
        if (InspectedObj.GetComponent<Rigidbody>())
        {
            InspectedObj.GetComponent<Rigidbody>().useGravity = false;
        }

        PlayerControllerScript.enabled = false;
        yield return new WaitForSeconds(0.2f);
        InspectedObj.transform.SetParent(InspectArea);
    }

    IEnumerator droppItem()
    {
        // Restore the original rotation
        InspectedObj.transform.rotation = originalRot;
        yield return new WaitForSeconds(0.2f);
        PlayerControllerScript.enabled = true;

        InspectedObj.transform.SetParent(InspectArea);

        if (InspectedObj.GetComponent<Collider>())
        {
            InspectedObj.GetComponent<Collider>().enabled = true;
        }
        if (InspectedObj.GetComponent<Rigidbody>())
        {
            InspectedObj.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
