using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellPowerUp : MonoBehaviour
{
    public PlayerStateMachine stateMachine;

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            Invoke("Delay", 2.3f);
            stateMachine.SwitchState(new PlayerGetsSpellPower(stateMachine));


        }


    }

    private void Delay()
    {
        Destroy(gameObject);
    }
}
