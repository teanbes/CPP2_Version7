using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class ActivateCombos : MonoBehaviour
{
    
    private PlayerStateMachine stateMachine;
    

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            if (collision.TryGetComponent<PlayerStateMachine>(out PlayerStateMachine stateMachine))
            {
                this.stateMachine = stateMachine;
                stateMachine.isCombo = true;
                
            }

            Destroy(gameObject);

        }
    }
}
