using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;
using Random = UnityEngine.Random;

public class EnemyScript : MonoBehaviour
{
    private Animator anim;
    private CapsuleCollider col;
    public GameObject ragdoll;
    //[HideInInspector]
    public int animNum;

    public Transform[] points;

    public float health;
    public GameManager gameManager;
    
    private GameObject hitObject;
    public LayerMask visionLayer;
    public LayerMask playerLayer;
    [HideInInspector]
    public LayerMask enemyLayer;
    RaycastHit hit;
    private float hitDistance;
    private Vector3 startPoint;

    public float sphereSize;
    public bool stationary, patrolling;
    private float randomness;

    private NavMeshAgent nav;
    private int destPoint;

    public GameObject player;
    public bool detectPlayer;
    private float lastPosTimer;
    private Vector3 direction;
    public Transform spine;

    [HideInInspector]
    public float dist;
    [HideInInspector]
    public float maxDist;
    [HideInInspector]
    public float minDist;

    private bool reloading;
    private float fireRate;
    private int currentAmmo;
    public Transform playerTargetter;

    public float heightDiff;
    public float walkingSpeed;

    [HideInInspector]
    public EnemyScript enemyScript;
    private NavMeshPath path;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();    
        col = GetComponent<CapsuleCollider>();
        path = new NavMeshPath();
        maxDist = 30f;
        minDist = 5f;

        currentAmmo = 20;
        randomness = 0.05f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (animNum != 5)
        {
            if (patrolling == true)
            {
                if (!nav.pathPending && nav.remainingDistance < 0.5f && detectPlayer == false)
                {
                    GoToNextPoint();
                }
            }

            HitDetection();

            if (detectPlayer == true)
            {
                ChasePlayer();
                ShootAtPlayer();
            }
            else
            {
                if (patrolling == true)
                {
                    animNum = 1;
                    nav.speed = walkingSpeed;
                }
                else
                {
                    animNum = 0;
                    nav.speed = 0f;
                }
               
            }

            

        }
            
        anim.SetInteger("AnimNum", animNum);

        if (health <= 0)
        {
            nav.enabled = false;
            animNum = 5;
            col.enabled = false;
            Instantiate(ragdoll, new Vector3(transform.position.x, transform.position.y + 0.02f, transform.position.z), transform.rotation);
            Death();
        }


       

    }

    void LateUpdate()
    {
        if (detectPlayer == true)
        {
            //spine.transform.LookAt(player.transform.position);
        }

    }

    void HitDetection()
    {
        startPoint = new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z);

        if (Physics.SphereCast(startPoint, sphereSize, transform.forward, out hit, 25f, visionLayer))
        {
            hitObject = hit.transform.gameObject;
            hitDistance = hit.distance;
        }
        else
        {
            hitDistance = 25f;
            hitObject = null;
        }

        if (hitObject == player)
        {
            detectPlayer = true;
        }

        enemyLayer = LayerMask.GetMask("Enemy");
        Collider[] colliders = Physics.OverlapSphere(transform.position, 20f, enemyLayer);
        for (int i = 0; i < colliders.Length; i++)
        {
            GameObject collider = colliders[i].gameObject; 
            if (collider.CompareTag("Enemy") && collider.gameObject != gameObject)
            {
                enemyScript = collider.GetComponent<EnemyScript>();
                if (enemyScript != null)
                {
                    if (enemyScript.detectPlayer == true)
                    {
                        detectPlayer = true;
                    }
                }
            }
        }

    }

    void GoToNextPoint()
    {

        if (points.Length == 0)
        {
            return;
        }

        nav.destination = points[destPoint].position;
        destPoint = (destPoint + 1) % points.Length; 

    }

    void ChasePlayer()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        

        //Debug.Log(dist);

        if (dist < maxDist && dist > minDist)
        {
            nav.ResetPath();
            RaycastHit laserHit;
            Physics.Raycast(player.transform.position, -Vector3.up, out laserHit, playerLayer);

            //Vector3 laserPosition = laserHit.collider.transform.TransformDirection(laserHit.point);
            //Debug.Log(laserPosition.y);
            //Debug.Log(laserHit.point.y);

            nav.destination = new Vector3(player.transform.position.x, laserHit.point.y,player.transform.position.z);
            float disty = (laserHit.point.y - transform.position.y);
            //float disty = (player.transform.position - transform.position.y) - 1.4f;

            //Debug.Log(disty);

            bool reachable = nav.CalculatePath(new Vector3(player.transform.position.x, laserHit.point.y, player.transform.position.z), path);
            //Debug.Log(reachable);

            if (stationary == false && disty > heightDiff && reachable == true)
            {
                nav.speed = 3.5f;
                animNum = 3;
            }
            else
            {
                nav.speed = 0;
                transform.LookAt(player.transform.position);
                animNum = 4;
            }
           
        }

        if (dist <= minDist)
        {
            nav.speed = 0f;
            transform.LookAt(player.transform.position);
            animNum = 4;
        }
        
        if (dist >= maxDist)
        {
            nav.ResetPath();
            detectPlayer = false;
        }
       

    }
    
    void ShootAtPlayer()
    {
     

        if (lastPosTimer <= 0)
        {
            direction = (player.transform.position - playerTargetter.transform.position).normalized;           
        }      

        playerTargetter.transform.LookAt(player.transform.position);

        fireRate -= Time.deltaTime;
        lastPosTimer -= Time.deltaTime;

        Debug.DrawRay(playerTargetter.transform.position, new Vector3(direction.x, direction.y, direction.z), Color.red);

        if (fireRate <= 0 && currentAmmo > 0)
        {
            RaycastHit laserHit;
            if (Physics.Raycast(playerTargetter.transform.position, /*direction*/ new Vector3(direction.x + Random.Range(randomness, -randomness), direction.y + Random.Range(randomness, -randomness), direction.z), out laserHit, playerLayer))
            {
               
                if (laserHit.collider.name == "Player")
                {
                    gameManager.health -= Random.Range(2, 5);
                    Debug.DrawRay(playerTargetter.transform.position, new Vector3(direction.x + Random.Range(randomness, -randomness), direction.y + Random.Range(randomness, -randomness), direction.z), Color.green);
                }
            }

            fireRate = 0.3f;
            lastPosTimer = 0.27f;
            currentAmmo -= 1;
        }

        if (currentAmmo == 0 && reloading == false) 
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        reloading = true;

        yield return new WaitForSeconds(3.0f);

        currentAmmo = 20;
        reloading = false;
    }

    void Death()
    {
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        detectPlayer = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Explosion")
        {
            health = 0;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(startPoint, startPoint + transform.forward * hitDistance);
        Gizmos.DrawWireSphere(startPoint + transform.forward * hitDistance, sphereSize);
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, 20f);
    }

}
