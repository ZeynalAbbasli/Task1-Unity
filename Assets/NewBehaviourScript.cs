using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    CharacterController characterController;

    [SerializeField] float speed;
    
    float directionX, directionZ;
    
    Vector3 movement = Vector3.zero;

    bool jump;
    
    [SerializeField] float jumpHeight;
    
    [SerializeField] float gravity;
    
    bool jumpColorDepending;

    float reverseController;


    //Vector3 dir = Vector3.zero;
    void Start()
    {
        reverseController = 1f;

        jump = false;

        jumpColorDepending = true;

        speed = 10;

        gravity = 0.05f;

        jumpHeight = 0.5f;

        characterController= GetComponent<CharacterController>();
    }

    void Update()
    {
        directionX = reverseController * Input.GetAxis("Horizontal");
        directionZ = reverseController * Input.GetAxis("Vertical");

        if(!jump && Input.GetKeyDown(KeyCode.Space) && jumpColorDepending)
            jump = true;

    }

    private void FixedUpdate ()
    {
        //movement = new Vector3(directionX, 0, directionZ) * speed * Time.fixedDeltaTime;

        movement.x = speed * directionX * Time.fixedDeltaTime;
        movement.z = speed * directionZ * Time.fixedDeltaTime;

        if (jump)
        {
            movement.y = jumpHeight;
            jump = false;
        }

        if(jump && characterController.isGrounded)
            movement.y = 0;
        else
            movement.y -= gravity;
            



        characterController.Move(movement);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Red")
            speed = speed / 2;
        if (other.gameObject.name == "Green")
            speed = speed*2;
        if (other.gameObject.name == "Blue")
            jumpHeight = jumpHeight + 0.3f;
        if (other.gameObject.name == "Black")
            jumpColorDepending = false;
        if (other.gameObject.name == "Yellow")
            reverseController = -1f;

        other.gameObject.SetActive(false);
        gameObject.GetComponent<Renderer>().material.color = other.gameObject.GetComponent<Renderer>().material.color;

    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.name == "White")
        {
            hit.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-100, 100), 0, Random.Range(-100, 100)));
        }
    }
}
