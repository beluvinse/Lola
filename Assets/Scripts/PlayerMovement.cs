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
    private Vector3 pointToLook, lookAt;


    Rigidbody myRb;
    Camera _mainCamera;

    private Animator _myAnim;
    [SerializeField] private string _xAxisName;
    [SerializeField] private string _zAxisName;
    [SerializeField] private AnimationClip _shootAnimation;

    public GunController gun;

    [Header("Audio")]
    [SerializeField] private AudioClip _gunSFX;
    private AudioSource _myAudioSource;


    public Vector3 getLookAt()
    {
        return lookAt;
    }

    private void Start()
    {
        myRb = GetComponent<Rigidbody>();
        myRb.freezeRotation = true;
        _mainCamera = FindObjectOfType<Camera>();
        _myAnim = GetComponentInChildren<Animator>();
        startSpeed = _moveSpeed;
        _myAudioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Ray cameraRay = _mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float raylenght;

        if (groundPlane.Raycast(cameraRay, out raylenght))
        {
            pointToLook = cameraRay.GetPoint(raylenght);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);

            lookAt = new Vector3(pointToLook.x, transform.position.y, pointToLook.z);

            transform.LookAt(lookAt);
            //Debug.DrawLine(this.transform.position, lookAt, Color.red);
        }

        _grounded = Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, ground);

        MyInput();
        SpeedControl();

        if (_grounded)
            myRb.drag = groundDrag;
        else
            myRb.drag = 0;

        if (Input.GetMouseButtonDown(0))
        {
            gun.setIsFiring(true);
            StartCoroutine(crOnShoot());
        }
        if (Input.GetMouseButtonUp(0))
        {
            gun.setIsFiring(false);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(crOnRoll());
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

    public void OnShoot()
    {
        _myAudioSource.clip = _gunSFX;
        if (!_myAudioSource.isPlaying)
        {
            _myAudioSource.Play();
        }
    }

    private IEnumerator crOnShoot()
    {
        _moveSpeed = 0;
        _myAnim.SetTrigger("onShoot");
        OnShoot();
        yield return new WaitForSeconds(0.25f);
        _myAnim.SetTrigger("onEndShoot");
        _moveSpeed = startSpeed;
    }

    private IEnumerator crOnRoll()
    {
        //_moveSpeed = 0;
        _myAnim.SetTrigger("onRoll");
        //OnShoot();
        yield return new WaitForSeconds(0.25f);
        _myAnim.SetTrigger("onEndRoll");
        //_moveSpeed = startSpeed;
    }
}


