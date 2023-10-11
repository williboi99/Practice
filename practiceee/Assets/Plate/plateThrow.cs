using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plateThrow : MonoBehaviour
{
    [SerializeField] GameObject fullPlate;
    [SerializeField] GameObject brokenPlate;
    Rigidbody rb;
    [SerializeField] float throwForce = 500f;

    

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.R))
        {
            
            GameObject thrownPlate = Instantiate(fullPlate, transform.position, transform.rotation);
            rb = thrownPlate.GetComponentInChildren<Rigidbody>();
            rb.AddRelativeForce(0, -throwForce, 50);
        }
    }
    
}
