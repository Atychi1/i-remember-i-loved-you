using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< HEAD
<<<<<<< HEAD

public class InspectScript : MonoBehaviour
{
    [SerializeField] private float distance; //Raycast max distance
    [SerializeField] private Transform InspectArea; //A gameobject child of the camera where the object will be moved and rotated
=======
=======
>>>>>>> 8665f6f1fbc5a67542c1cc8a4b5b1f642f418566
using TMPro;

public class InspectScript : MonoBehaviour
{
    [SerializeField] private float distance; // Raycast max distance
    [SerializeField] private Transform InspectArea; // A gameobject child of the camera where the object will be moved and rotated
<<<<<<< HEAD
>>>>>>> 8665f6f1fbc5a67542c1cc8a4b5b1f642f418566
=======
>>>>>>> 8665f6f1fbc5a67542c1cc8a4b5b1f642f418566
    [SerializeField] private float rotationSpeed = 125f;
    [SerializeField] private float dropSpeed = 0.2f;
    [SerializeField] private float pickupSpeed = 0.2f;
    [SerializeField] private bool InvertMouse = false;
    [SerializeField] private string TargetTag = "Object";
    [Header("Using the FPS camera is suggested")]
    [SerializeField] private Transform RayOrigin;
    [Header("Change 'PlayerController' in the code with your own FPS controller script")]
<<<<<<< HEAD
<<<<<<< HEAD
    [SerializeField] private FirstPersonController PlayerControllerScript; //my fps controller change it with yours


=======
=======
>>>>>>> 8665f6f1fbc5a67542c1cc8a4b5b1f642f418566
    [SerializeField] private FirstPersonController PlayerControllerScript; // my FPS controller change it with yours

    [SerializeField] private TextMeshProUGUI inspectText;
    [SerializeField] private TextMeshProUGUI putdownText;
<<<<<<< HEAD
>>>>>>> 8665f6f1fbc5a67542c1cc8a4b5b1f642f418566
=======
>>>>>>> 8665f6f1fbc5a67542c1cc8a4b5b1f642f418566

    private Vector3 originalPos;
    private bool Inspecting = false;
    private GameObject InspectedObj;

<<<<<<< HEAD
<<<<<<< HEAD


    void Update()
    {

      if(Input.GetKeyDown(KeyCode.E))
      {
=======
=======
>>>>>>> 8665f6f1fbc5a67542c1cc8a4b5b1f642f418566
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
<<<<<<< HEAD
>>>>>>> 8665f6f1fbc5a67542c1cc8a4b5b1f642f418566
=======
>>>>>>> 8665f6f1fbc5a67542c1cc8a4b5b1f642f418566
        Vector3 fwdV = RayOrigin.TransformDirection(Vector3.forward);
        Debug.DrawRay(RayOrigin.position, fwdV * distance, Color.red);

        RaycastHit hit;
<<<<<<< HEAD
<<<<<<< HEAD
        if(Physics.Raycast(RayOrigin.position, fwdV, out hit, distance))
        {
          if(hit.transform.tag == TargetTag && !Inspecting)
          {

            InspectedObj = hit.transform.gameObject;
            originalPos = hit.transform.position;
            Inspecting = true;
            StartCoroutine(pickupItem());

          }
        }
      }



      if(Inspecting)
      {
        InspectedObj.transform.position = Vector3.Lerp(InspectedObj.transform.position, InspectArea.position, pickupSpeed);

        if(!InvertMouse)
        InspectArea.Rotate(new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0)* Time.deltaTime * rotationSpeed);
        else
        InspectArea.Rotate(new Vector3(Input.GetAxis("Mouse Y"), -Input.GetAxis("Mouse X"), 0)* Time.deltaTime * rotationSpeed);

      }
      else if(InspectedObj != null)
      {
        InspectedObj.transform.SetParent(null);
        InspectArea.rotation = Quaternion.identity;
        InspectedObj.transform.position = Vector3.Lerp(InspectedObj.transform.position, originalPos, dropSpeed);
      }

      if(Input.GetKeyDown(KeyCode.Escape) && Inspecting)
      {
        Inspecting = false;
        StartCoroutine(droppItem());

      }


=======
=======
>>>>>>> 8665f6f1fbc5a67542c1cc8a4b5b1f642f418566
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
<<<<<<< HEAD
>>>>>>> 8665f6f1fbc5a67542c1cc8a4b5b1f642f418566
=======
>>>>>>> 8665f6f1fbc5a67542c1cc8a4b5b1f642f418566
    }

    IEnumerator pickupItem()
    {
<<<<<<< HEAD
<<<<<<< HEAD
        
      //collider and rigidbodies cause problem during the inspection so we disable them during it  
      if(InspectedObj.GetComponent<Collider>())
      {
        InspectedObj.GetComponent<Collider>().enabled = false;
      }
      if(InspectedObj.GetComponent<Rigidbody>())
      {
        InspectedObj.GetComponent<Rigidbody>().useGravity = false;
      }

      PlayerControllerScript.enabled = false;
      yield return new WaitForSeconds(0.2f);
      InspectedObj.transform.SetParent(InspectArea);
=======
=======
>>>>>>> 8665f6f1fbc5a67542c1cc8a4b5b1f642f418566
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
<<<<<<< HEAD
>>>>>>> 8665f6f1fbc5a67542c1cc8a4b5b1f642f418566
=======
>>>>>>> 8665f6f1fbc5a67542c1cc8a4b5b1f642f418566
    }

    IEnumerator droppItem()
    {
<<<<<<< HEAD
<<<<<<< HEAD
      InspectedObj.transform.rotation = Quaternion.identity;
      yield return new WaitForSeconds(0.2f);
      PlayerControllerScript.enabled = true;

      InspectedObj.transform.SetParent(InspectArea);

      if(InspectedObj.GetComponent<Collider>())
      {
        InspectedObj.GetComponent<Collider>().enabled= true;
      }
      if(InspectedObj.GetComponent<Rigidbody>())
      {
        InspectedObj.GetComponent<Rigidbody>().useGravity = true;
      }
    }

}
=======
=======
>>>>>>> 8665f6f1fbc5a67542c1cc8a4b5b1f642f418566
        InspectedObj.transform.rotation = Quaternion.identity;
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
<<<<<<< HEAD
}
>>>>>>> 8665f6f1fbc5a67542c1cc8a4b5b1f642f418566
=======
}
>>>>>>> 8665f6f1fbc5a67542c1cc8a4b5b1f642f418566
