using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVisibility : MonoBehaviour
{
    public float maxDistance = 10f;
    private MeshRenderer enemyRenderer;
    public float raycastYoffset = 0.5f ;

    //projectile variables
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float shootInterval = 2f;
    private float lastShootTime = 0f;
    public Transform spawnPoint;

    public float transparency = 0.5f;
    public List<Material> material = new List<Material>();


    void Start()
    {
        enemyRenderer = GetComponent<MeshRenderer>();
        
    }

    void Update()
    {
        RaycastHit hit;
        Vector3 rayCastOrigin = transform.position + new Vector3 (0f, 0f, 0f);
        // to make raycast only affect on a layer 
        int layermask = 1 << 6;

        if (Physics.Raycast(rayCastOrigin, transform.forward, out hit, maxDistance, layermask))
        {
            Debug.DrawRay(rayCastOrigin, transform.forward * hit.distance, Color.red);
            if (hit.collider.CompareTag("Player"))
            {
                //enemyRenderer.enabled = true;
                enemyRenderer.material = material[0];

                if (Time.time - lastShootTime >= shootInterval)
                {
                    ShootPlayer(hit.point);
                    lastShootTime = Time.time;
                }
            }
           /* else
            {
                enemyRenderer.enabled = false;
            }*/
        }
        else
        {
            //enemyRenderer.enabled = false;
            enemyRenderer.material = material[1];
            Debug.DrawRay(rayCastOrigin, transform.forward * maxDistance, Color.blue);
        }
    }

    void ShootPlayer(Vector3 playerPosition)
    {
        // Calculate direction to player
        Vector3 direction = (playerPosition - transform.position).normalized;

        // Instantiate bullet and set its position and rotation
        GameObject bullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.LookRotation(direction));

        // Add force to bullet in direction of player
        bullet.GetComponent<Rigidbody>().AddForce(direction * bulletSpeed, ForceMode.Impulse);
    }
}
