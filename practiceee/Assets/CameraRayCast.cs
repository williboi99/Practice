using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRayCast : MonoBehaviour
{
    [SerializeField] Camera FPcamera;
    [SerializeField] float range = 20f;

    [SerializeField] float TalkRange = 2f;
    [SerializeField] float doorRange = 3f;
    //public Animator doorAnim;

    

    
    [SerializeField] AudioClip DoorSound;

    bool interact;

    [SerializeField] GameObject talkPopup;
    [SerializeField] GameObject doorPopup;
    [SerializeField] GameObject pickupPopup;

    public RaycastHit sphereHit;
    public RaycastHit hit;


    // Start is called before the first frame update
    void Start()
    {
        //Application.targetFrameRate = 60;
        talkPopup.SetActive(false);
        doorPopup.SetActive(false);
    }

    
    

    void Scan()
    {

        Physics.Raycast(FPcamera.transform.position, FPcamera.transform.forward, out hit, range);
        //Physics.SphereCast(FPcamera.transform.position, .25f, FPcamera.transform.forward, out sphereHit, 5f);

        if (hit.transform.CompareTag("Door") && hit.distance <= doorRange)
        {
            doorPopup.SetActive(true);
            //if (interact == true)
            if(Input.GetKeyDown(KeyCode.E))
            {
                hit.transform.GetComponent<Animator>().SetBool("doorInteract", !hit.transform.GetComponent<Animator>().GetBool("doorInteract"));
                hit.transform.GetComponent<AudioSource>().PlayOneShot(DoorSound);
                //doorAnim.SetBool("doorInteract",!doorAnim.GetBool("doorInteract"));
            }
            if (Input.GetButtonDown("Fire1"))
            {
                hit.transform.GetComponent<Animator>().SetBool("doorInteract", !hit.transform.GetComponent<Animator>().GetBool("doorInteract"));
                hit.transform.GetComponent<AudioSource>().PlayOneShot(DoorSound);
                //doorAnim.SetBool("doorInteract",!doorAnim.GetBool("doorInteract"));
            }
        }
        else
        {
            doorPopup.SetActive(false);
        }


        


        if (hit.transform.CompareTag("Talkable") & hit.distance <= TalkRange)
        {
            talkPopup.SetActive(true);
        }
        else
        {
            talkPopup.SetActive(false);
        }

        if (sphereHit.transform.CompareTag("pickup"))
        {
            if (sphereHit.distance <= 2f)
            {
                pickupPopup.SetActive(true);
            }
        }
        else
        {
            pickupPopup.SetActive(false);
        }
        
        



    }
    void Update()
    {
        Application.targetFrameRate = 60;
        Scan();

    }

    public void OnInteract(InputAction.CallbackContext context)
    {

        if (sphereHit.transform.CompareTag("pickup"))
        {
            if (sphereHit.distance <= 2f)
            {
                
                if (context.performed)
                {
                    sphereHit.transform.parent = FPcamera.transform;
                    sphereHit.transform.GetComponent<BoxCollider>().isTrigger = true;
                    sphereHit.transform.GetComponent<Rigidbody>().useGravity = false;
                    sphereHit.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    
                }
                else if (context.canceled)
                {
                    sphereHit.transform.parent = null;
                    sphereHit.transform.GetComponent<BoxCollider>().isTrigger = false;
                    sphereHit.transform.GetComponent<Rigidbody>().useGravity = true;
                    sphereHit.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
                    sphereHit.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
                    sphereHit.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX;
                    sphereHit.transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationZ;

                    //pickupPopup.SetActive(false);
                }
            }
            else pickupPopup.SetActive(false);
        }
        else pickupPopup.SetActive(false);
    }

}
