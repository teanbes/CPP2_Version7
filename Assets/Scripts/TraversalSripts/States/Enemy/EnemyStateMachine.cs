using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class EnemyStateMachine : StateMachine
{
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public CharacterController Controller { get; private set; }
    [field: SerializeField] public ForceReceiver ForceReceiver { get; private set; }
    [field: SerializeField] public NavMeshAgent Agent { get; private set; }
    [field: SerializeField] public WeaponDamage Weapon { get; private set; }
    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public Target Target { get; private set; }
    [field: SerializeField] public Ragdoll Ragdoll { get; private set; }

    [field: SerializeField] public float MovementSpeed { get; private set; }
    [field: SerializeField] public float PlayerChasingRange { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; }
    [field: SerializeField] public int AttackDamage { get; private set; }
    [field: SerializeField] public int AttackKnockback { get; private set; }

    //Vars for anemy path
    [field: SerializeField] public Transform PathTarget;
    [field: SerializeField] public GameObject[] Path { get; private set; }
    [field: SerializeField] public int PathIndex;
    [field: SerializeField] public float DistThreshhold { get; private set; }

    //PowerUps
    [field: SerializeField] public GameObject[] collectibles;
    Vector3 currentEulerAngles;
    Quaternion currentRotation;
    public bool enemyDead = false;

    public Health Player { get; private set; }

    private void Start()
    {
        
        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();

        //looks for empty gameobjects to set the path
        if (Path.Length <= 0) Path = GameObject.FindGameObjectsWithTag("Patrol");

        //Aproximation for path, almost never reaches 0 because of float calculations
        if (DistThreshhold <= 0) DistThreshhold = 0.5f;

        Agent.updatePosition = false;
        Agent.updateRotation = false;

        SwitchState(new EnemyPatrollingState(this));
    }
   

    /*private IEnumerator FindPlayer()
    {
        yield return new WaitForSeconds(1f); // wait for 1 second
        
    }*/

    private void OnEnable()
    {
        Health.OnTakeDamage += HandleTakeDamage;
        Health.OnDie += HandleDie;
    }

    private void OnDisable()
    {
        Health.OnTakeDamage -= HandleTakeDamage;
        Health.OnDie -= HandleDie;
    }

    private void HandleTakeDamage()
    {
        SwitchState(new EnemyImpactState(this));
    }

    private void HandleDie()
    {
        SwitchState(new EnemyDeadState(this));
        enemyDead = true;
        StartCoroutine(SpawnCollectible());
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, PlayerChasingRange);
    }

    private IEnumerator SpawnCollectible()
    {
        yield return new WaitForSeconds(2f); // wait for 1 second
        SpawnPowerUps(collectibles);
        

    }


    public void SpawnPowerUps(GameObject[] collectibles)
    {
        if (collectibles.Length > 0)
        {
            currentEulerAngles = new Vector3(-90, 0, 0);
            currentRotation.eulerAngles = currentEulerAngles;
            transform.rotation = currentRotation;
            
            int index = Random.Range(0, collectibles.Length);
            Vector3 newPos = transform.position;
            newPos.y += 2;


            Instantiate(collectibles[index], newPos, transform.rotation);
            

        }

    }
}
