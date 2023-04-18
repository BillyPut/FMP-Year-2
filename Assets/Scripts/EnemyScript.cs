using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;
using Random = UnityEngine.Random;

public class EnemyScript : MonoBehaviour
{
    private Animator anim;
    //[HideInInspector]
    public int animNum;

    public Transform[] points;

    public float health;
    public GameManager gameManager;
    
    public GameObject hitObject;
    public LayerMask visionLayer;
    public LayerMask playerLayer;
    RaycastHit hit;
    private float hitDistance;
    private Vector3 startPoint;

    private NavMeshAgent nav;
    private int destPoint;

    public GameObject player;
    public bool detectPlayer;

    [HideInInspector]
    public float dist;
    [HideInInspector]
    public float maxDist;
    [HideInInspector]
    public float minDist;

    public bool reloading;
    public float fireRate;
    public int currentAmmo;
    public Transform playerTargetter;

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();    
        maxDist = 30f;
        minDist = 5f;

        health = 100f;
        currentAmmo = 30;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (animNum != 5)
        {
            if (!nav.pathPending && nav.remainingDistance < 0.5f && detectPlayer == false)
            {
                GoToNextPoint();
            }

            HitDetection();

            if (detectPlayer == true)
            {
                ChasePlayer();
                ShootAtPlayer();
            }
            else
            {
                animNum = 1;
            }

           
        }
        else
        {
            nav.ResetPath();
        }
        
        anim.SetInteger("AnimNum", animNum);

        if (health <= 0)
        {
            animNum = 5;
        }
    }

    void HitDetection()
    {
        startPoint = new Vector3(transform.position.x, transform.position.y + 3f, transform.position.z);

        if (Physics.SphereCast(startPoint, 4f, transform.forward, out hit, 20f, visionLayer))
        {
            hitObject = hit.transform.gameObject;
            hitDistance = hit.distance;
        }
        else
        {
            hitDistance = 10f;
            hitObject = null;
        }

        if (hitObject == player)
        {
            detectPlayer = true;
        }

    }

    void GoToNextPoint()
    {
        nav.speed = 3.5f;

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
            nav.destination = new Vector3(player.transform.position.x, 1f, player.transform.position.z);
            nav.speed = 3.5f;
            animNum = 3;
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
        Vector3 direction = (player.transform.position - playerTargetter.transform.position).normalized;

        playerTargetter.transform.LookAt(player.transform.position);

        Debug.DrawRay(playerTargetter.transform.position, /*direction * 20*/ new Vector3(direction.x + Random.Range(0.2f, -0.2f), direction.y + Random.Range(0.2f, -0.2f), direction.z), Color.green);

        fireRate -= Time.deltaTime;

        if (fireRate <= 0 && currentAmmo > 0)
        {
            RaycastHit laserHit;
            if (Physics.Raycast(playerTargetter.transform.position, /*direction*/ new Vector3(direction.x + Random.Range(0.2f, -0.2f), direction.y + Random.Range(0.2f, -0.2f), direction.z), out laserHit, playerLayer))
            {

                //Debug.Log(laserHit.collider.name);

                if (laserHit.collider.name == "Player")
                {
                    gameManager.health -= Random.Range(5, 10);
                }
            }

            fireRate = 0.6f;
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

        currentAmmo = 30;
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

    

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(startPoint, startPoint + transform.forward * hitDistance);
        Gizmos.DrawWireSphere(startPoint + transform.forward * hitDistance, 4f);
    }

}
