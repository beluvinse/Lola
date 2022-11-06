using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _moveSpeed;
    float startSpeed;
    public float groundDrag;

    [Header("Grounde Check")]
    [SerializeField] private float _playerHeight;
    [SerializeField] private bool _grounded;
    public LayerMask ground;


    public Transform orientation;

    private float _horizontalInput, _verticalInput;
    private Vector3 moveDirection;


    Rigidbody myRb;
    Camera _mainCamera;

    private Animator _myAnim;
    [SerializeField] private AnimationClip _punchAnimation;
    [SerializeField] private string _xAxisName;
    [SerializeField] private string _zAxisName;
    [SerializeField] private AnimationClip _shootAnimation;

    public GunController gun;


    private void Start()
    {
        myRb = GetComponent<Rigidbody>();
        myRb.freezeRotation = true;
        _mainCamera = FindObjectOfType<Camera>();
        _myAnim = GetComponentInChildren<Animator>();
        startSpeed = _moveSpeed;
    }

    private void Update()
    {
        Ray cameraRay = _mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float raylenght;

        if (groundPlane.Raycast(cameraRay, out raylenght))
        {
            Vector3 pointToLook = cameraRay.GetPoint(raylenght);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);

            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }

        _grounded = Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, ground);

        MyInput();
        SpeedControl();

        if (_grounded)
            myRb.drag = groundDrag;
        else
            myRb.drag = 0;


        if (Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(crOnPunch());
        }

        if (Input.GetMouseButtonDown(0))
        {
            gun.setIsFiring(true);
            StartCoroutine(crOnShoot());
        }
        if (Input.GetMouseButtonUp(0))
        {
            gun.setIsFiring(false);

        }
    }



    private void FixedUpdate()
    {
        if (!gun.getIsFiring())
        {
            MovePlayer();
            _myAnim.SetFloat(_xAxisName, _horizontalInput);
            _myAnim.SetFloat(_zAxisName, _verticalInput);
        }
        
    }

    private void MyInput()
    {
        _horizontalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MovePlayer()
    {
        //moveDirection = orientation.forward * _verticalInput + orientation.right * _horizontalInput;
        Vector3 moveInput = new Vector3(_horizontalInput, 0f, _verticalInput).normalized;

        myRb.AddForce(moveInput * _moveSpeed * 10f, ForceMode.Force);

    }


    private void SpeedControl()
    {

        Vector3 flatVel = new Vector3(myRb.velocity.x, 0f, myRb.velocity.z);

        if (flatVel.magnitude > _moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * _moveSpeed;
            myRb.velocity = new Vector3(limitedVel.x, myRb.velocity.y, limitedVel.z);
        }

    }

    private IEnumerator crOnPunch()
    {
        var speed = _moveSpeed;
        _moveSpeed = 0;
        _myAnim.SetTrigger("onPunch");
        yield return new WaitForSeconds(_punchAnimation.length);
        _myAnim.SetTrigger("onEndPunch");
        _moveSpeed = speed;
    }

    private IEnumerator crOnShoot()
    {
        _moveSpeed = 0;
        _myAnim.SetTrigger("onShoot");
        yield return new WaitForSeconds(gun.shotDelay);
        _myAnim.SetTrigger("onEndShoot");
        _moveSpeed = startSpeed;
    }

}


