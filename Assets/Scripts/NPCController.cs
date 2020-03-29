using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{
    public float patrolTime = 10f;
    public float aggroRange = 10f;
    public Transform[] waypoints;

    private int index;
    private float speed, agentSpeed;
    private Transform Player;

    private Animator anim;
    private NavMeshAgent agent;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        if (agent != null){ agentSpeed = agent.speed;}
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        index = Random.Range(0, waypoints.Length);

        InvokeRepeating("Tick", 0, 0.5f);

        if (waypoints.Length> 0)
        {
            InvokeRepeating("Patrol", 0, patrolTime);
        }

    }

    void Patrol()
    {
        index = index == waypoints.Length - 1 ?  0 : index + 1;
    }


    void Tick()
    {
        agent.destination = waypoints[index].position;
        agent.speed = agentSpeed / 2;

        if (Player != null && Vector3.Distance(transform.position, Player.position) < aggroRange)
        {
            anim.SetFloat("Chase", 0.8f);
            agent.destination = Player.position;
            agent.speed = agentSpeed;
            if (Player != null && Vector3.Distance(transform.position, Player.position) <= 1.55)
            {
                anim.SetFloat("Chase", 0f);
            }
        }
    }
    private void Update()
    {
        anim.SetFloat("Speed", agent.velocity.magnitude);
    
    }

}
