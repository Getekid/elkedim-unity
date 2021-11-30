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
    [SerializeField]
    private GameObject muzzleFlash;
    [SerializeField]
    private AudioSource weaponAudio;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }

        if (Input.GetMouseButton(0))
        {
            muzzleFlash.SetActive(true);
            if (!weaponAudio.isPlaying)
            {
                weaponAudio.Play();
            }
            
            Ray ray = Camera.main.ViewportPointToRay(new Vector3(0, 0, 0));
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo))
            {
                Debug.Log("hitInfo: " + hitInfo.transform.name);
            }
        }
        else
        {
            muzzleFlash.SetActive(false);
            weaponAudio.Stop();
        }
    }
}
