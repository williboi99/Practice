using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapSphere : MonoBehaviour
{
    [SerializeField] GameObject popup;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Activate();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Deactivate();
        }
    }
    void Activate()
    {
        popup.SetActive(true);
    }
    void Deactivate()
    {
        popup.SetActive(false);
    }



}
