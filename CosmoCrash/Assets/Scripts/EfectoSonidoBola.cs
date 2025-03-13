using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfectoSonidoBola : MonoBehaviour
{
    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bloque1"))
        {
            audioSource.Play();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
