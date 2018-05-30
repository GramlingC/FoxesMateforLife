using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class rabbitBehavior : MonoBehaviour
{
    public float fieldView = 110f;
    public bool Seen;
    public Vector3 lastSeenLocation;

    // Use this for initialization 
    
   private NavMeshAgent nav;
    private SphereCollider col;
    
    GameObject player;
    
    private void Awake()
    {
       
        col = GetComponent<SphereCollider>();
        player = GameObject.FindGameObjectWithTag("Player");

    
    }
    // Use this for initialization 
    void Start()
    {
   
    } 
   

        //when player enters collider, checks to see if player is infront (in vision range), or is unstealthed (in sound range) 
        private void OnTriggerStay(Collider other)
        {
      


            if (other.gameObject == player)
            {
                Seen = false; // false by default  
                              // Create a vector from the enemy to the player and store the angle between it and forward. 
                Vector3 direction = other.transform.position - transform.position;
                float angle = Vector3.Angle(direction, transform.forward);
             if (angle < fieldView * .5f)
             { // If the angle between forward and where the player is, is less than half the angle of view 
                RaycastHit hit;
                //if raycast hits something 
                if (Physics.Raycast(transform.position, direction.normalized, out hit, col.radius))
                {
                    //if it hits the player 
                    if (hit.collider.gameObject == player)
                    {
                        Seen = true;
                        Debug.Log("seen");
                        lastSeenLocation = player.transform.position;
                    }
                }

             }
             

            }
        }

        //when player leaves the collider can no longer be seen 
        private void OnTriggerExit(Collider other)
        {
            Debug.Log("unseen");

            if (other.gameObject == player)
            {
                Seen = false;
                Debug.Log("player unseen");

            }
        }


    } 
