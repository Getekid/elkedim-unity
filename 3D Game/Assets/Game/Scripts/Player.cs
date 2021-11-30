using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private float speed = 3.5f;
    private float gravity = 9.81f;

    [SerializeField]
    private NavMeshAgent navMeshAgent;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        Vector3 velocity = direction * speed;
        velocity.y -= gravity;
        velocity = transform.TransformDirection(velocity);
        controller.Move(velocity * Time.deltaTime);
        transform.position = navMeshAgent.nextPosition;
    }
}
