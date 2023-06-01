using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickingUp : MonoBehaviour
{
    public PlayerStateMachine stateMachine;
    
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            Invoke("Delay", 2.3f);
            stateMachine.SwitchState(new PlayerPickSwordState(stateMachine));
            

        }


    }

    private void Delay()
    {
        Destroy(gameObject);
    }
}




