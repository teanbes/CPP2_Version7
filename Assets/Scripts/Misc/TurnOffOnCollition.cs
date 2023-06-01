using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffOnCollition : MonoBehaviour
{
    [SerializeField] public GameObject gameObject1;
    [SerializeField] public GameObject gameObject2;
    [SerializeField] public GameObject gameObject3;


    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            Invoke("TurnOffObject", 2.3f);
            
        }


    }

    public void TurnOffObject()
    {
        gameObject1.SetActive(false);
        gameObject2.SetActive(false);
        gameObject3.SetActive(false);
    }
}
