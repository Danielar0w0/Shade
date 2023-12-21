using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonControllerScript : MonoBehaviour
{

    public GameObject spotLight;
    public Material newMaterial;

    // Start is called before the first frame update
    void Start()
    {
        spotLight.SetActive(false); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if(other.CompareTag("Player"))
        {
            spotLight.SetActive(true);
            GameObject.Find("Platform").GetComponent<Renderer>().material = newMaterial;
        }
        
    }
    
}
