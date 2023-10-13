using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class plateThrow : MonoBehaviour
{
    [SerializeField] GameObject fullPlate;
    [SerializeField] GameObject brokenPlate;
    Rigidbody rb;
    [SerializeField] float throwForce = 500f;
    [SerializeField] float plateTimer = 1.5f;

    

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        plateTimer -= Time.fixedDeltaTime;
        Debug.Log(plateTimer); 
    }

    public void OnPlateThrow(InputAction.CallbackContext context)
    {
        
        if ((context.performed) && plateTimer <= 0)
        {
            plateTimer = 1.5f;
            GameObject thrownPlate = Instantiate(fullPlate, transform.position, transform.rotation);
            rb = thrownPlate.GetComponentInChildren<Rigidbody>();
            rb.AddRelativeForce(0, -throwForce, 50);
        }
    }
    
}
