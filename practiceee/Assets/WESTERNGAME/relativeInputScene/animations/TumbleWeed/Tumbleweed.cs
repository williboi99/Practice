using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tumbleweed : MonoBehaviour
{


    [SerializeField] AudioSource audSource;
    [SerializeField] AudioClip rustle;
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
       audSource.PlayOneShot(rustle);
    }
}
