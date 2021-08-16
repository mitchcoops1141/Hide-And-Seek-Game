using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Player_Cs : MonoBehaviour
{
    NavMeshAgent agent;
    [HideInInspector] public bool canFunction = false;
    public RuntimeAnimatorController animatorController;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //anim = GetComponentInChildren<Animator>();

        GameObject mesh = Instantiate(GameObject.FindGameObjectWithTag("MeshSaver").GetComponent<MeshSaver>().GetMesh(), transform);
        mesh.SetActive(true);
        mesh.GetComponent<Animator>().runtimeAnimatorController = animatorController;
        mesh.transform.localPosition = Vector3.zero;
        anim = GetComponentInChildren<Animator>();
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
        anim.SetFloat("MoveSpeed", agent.velocity.magnitude);
        transform.GetChild(0).localPosition = Vector3.zero;
        transform.GetChild(0).localRotation = Quaternion.Euler(0, 0, 0);
        
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
