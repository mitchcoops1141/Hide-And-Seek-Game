using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Player_Cs : MonoBehaviour
{
    NavMeshAgent agent;
    [HideInInspector] public bool canFunction = false;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canFunction)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;

                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    agent.SetDestination(hit.point);
                }
            }
        }
            //agent.speed = 0;
    }

    void SeekerCollision()
    {
        //end round
        LevelManager.instance.Lose();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (canFunction)
        {
            if (collision.gameObject.tag == "Seeker")
            {
                print("here");
                SeekerCollision();
            }
        }
        
    }
}
