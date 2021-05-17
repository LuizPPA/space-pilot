using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] float inGravityMoveSpeed = 0f;
    [SerializeField] float zeroGravityMoveSpeed = 100f;
    [SerializeField] float acceleration = 10f;
    [SerializeField] float lookSpeed = 100f;
    [SerializeField] float jumpImpulse = 100f;
    [SerializeField] float propulsion = 10f;

    private bool inGravity = false;
    private float lookPosition = 0f;
    private float moveSpeed = 0f;
    private Transform currentPlanetTransform = null;
    private Vector3 moveAmount = Vector3.zero;
    private Vector3 smoothVelocity = Vector3.zero;
    private Rigidbody rigidBd = null;

    // Start is called before the first frame update
    void Start()
    {
        rigidBd = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        moveSpeed = inGravity ? inGravityMoveSpeed : zeroGravityMoveSpeed;
        Move();
        Look();
    }

    void FixedUpdate(){
        if(inGravity){
            Vector3 newPosition = rigidBd.position + (moveAmount * Time.fixedDeltaTime);
            rigidBd.MovePosition(newPosition);
        }
    }

    void Move(){
        if(inGravity){
            MoveInGravity();
        }
        else{
            MoveZeroGravity();
        }
    }

    void MoveInGravity(){
        Vector3 input = new Vector3(
            Input.GetAxis("Horizontal"),
            Input.GetAxis("Vertical"),
            0f
        );

        var moveDirection = GetMovementDirection(input);
        var targetMoveAmount = moveDirection * moveSpeed;
        moveAmount = Vector3.SmoothDamp(moveAmount, targetMoveAmount, ref smoothVelocity, 0.15f);
        // rigidBd.MovePosition(rigidBd.position + moveAmount * Time.deltaTime);
        if(Input.GetKeyDown("space")){
            rigidBd.AddForce((transform.up * jumpImpulse), ForceMode.Impulse);
        }
    }

    void MoveZeroGravity(){
        Vector3 input = new Vector3(
            Input.GetAxis("Horizontal") * propulsion,
            Input.GetAxis("Vertical") * propulsion,
            Input.GetAxis("Up/Down") * propulsion
        );

        var movement = GetMovementDirection(input);
        var speed =  GetZeroGMovementSpeed();
        rigidBd.AddForce(movement * speed * Time.deltaTime, ForceMode.Impulse);
    }

    Vector3 GetMovementDirection(Vector3 input){
        return ((transform.right * input.x) + (transform.forward * input.y) + (transform.up * input.z)).normalized;
    }

    float GetZeroGMovementSpeed(){
        return Mathf.Clamp(acceleration + rigidBd.velocity.magnitude, -moveSpeed, moveSpeed);
    }

    void Look(){
        var player = transform;
        if(inGravity){
            lookPosition += Input.GetAxis("Mouse Y") * lookSpeed * 2f * Time.deltaTime;
            lookPosition = Mathf.Clamp(lookPosition, -60f, 90f);
            Camera.main.transform.localEulerAngles = Vector3.left * lookPosition;
        }
        else{
            player.Rotate(-Input.GetAxis("Mouse Y") * lookSpeed * Time.deltaTime, 0, 0);
        }

        if(Input.GetKey("r") && !inGravity){
            player.Rotate(0, 0, -Input.GetAxis("Mouse X") * lookSpeed * Time.deltaTime);
        }
        else{
            player.Rotate(0, Input.GetAxis("Mouse X") * lookSpeed * Time.deltaTime, 0);
        }

        if (Input.GetMouseButtonDown(0)){
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void SetInGravity(bool inGravity, Transform planet){
        this.inGravity = inGravity;
        if(!inGravity){
            currentPlanetTransform = planet;
            StartCoroutine("RealignCamera");
        }
        else{
            currentPlanetTransform = null;
        }
    }

    public bool isInGravity(){
        return inGravity;
    }

    private IEnumerator RealignCamera(){
        while(Camera.main.transform.localEulerAngles != Vector3.zero){
            yield return new WaitForSeconds(0.01f);
            var newPosition = Vector3.MoveTowards(Camera.main.transform.localEulerAngles, Vector3.zero, lookSpeed * 0.01f);
            Camera.main.transform.localEulerAngles = newPosition;
        }
    }
}
