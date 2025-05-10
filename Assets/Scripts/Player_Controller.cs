using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    // Components
    private Rigidbody myRB;
    private Transform myAvatar;
    private Transform myLightLeft;
    private Transform myLightRight;
    public CrateManager crateManager;
    public TimeController timeController;

    [Header("Effects")]
    [SerializeField] private ParticleSystem waterTrailLeft;

    // Input
    private InputAction movementAction;
    private Vector2 movementInput;
    private Vector3 targetVelocity;

    private bool areLightsOn;
    private InputAction toggleLightsAction;

    [Header("Movement Settings")]
    [SerializeField] float acceleration = 8f;
    [SerializeField] float maxSpeed = 12f;
    [SerializeField] float drag = 3f;

    [Header("Tilt Settings")]
    [SerializeField] float tiltAngle = 15f;
    [SerializeField] float tiltSmoothing = 5f;

    [Header("Water Surface Settings")]
    [SerializeField] float surfaceY = -321.5f;
    [SerializeField] float waterDrag = 0.5f;
    [SerializeField] float airDrag = 0f;
    [SerializeField] float bobForce = 4f;
    [SerializeField] float bobRange = 0.6f;
    [SerializeField] float airborneGravityMultiplier = 1.2f;

    private bool isInAir = false;
    private bool wasInAirLastFrame = false;
    private float currentDirection = 1f;

    private void OnEnable()
    {
        SetupInput();
        movementAction.Enable();
    }

    private void OnDisable()
    {
        movementAction.Disable();
    }

    void Start()
    {
        myRB = GetComponent<Rigidbody>();
        myAvatar = transform.Find("Sprite");
        myLightLeft = myAvatar.Find("Light Left");
        //myLightRight = myAvatar.Find("Light Right");
        myRB.linearDamping = drag;

        if (waterTrailLeft != null)
        {
            waterTrailLeft.Stop();
        }
    }

    void ToggleLights()
    {
        areLightsOn = !areLightsOn;
        myLightRight.gameObject.SetActive(areLightsOn);
    }

    void Update()
    {
        if (!isInAir)
            movementInput = movementAction.ReadValue<Vector2>();
        else
            movementInput = Vector2.zero;

        if (movementInput.x > 0.1f)
            currentDirection = 1f;

        // Rear-left water trail logic
        bool isMoving = movementInput.magnitude > 0.1f;
        if (!isInAir && isMoving)
        {
            if (!waterTrailLeft.isPlaying)
                waterTrailLeft.Play();
        }
        else
        {
            if (waterTrailLeft.isPlaying)
                waterTrailLeft.Stop();
        }

        if (toggleLightsAction.triggered)
            ToggleLights();

        ApplyTilt(movementInput);
    }
    void FixedUpdate()
    {
        Vector3 inputDir = new Vector3(movementInput.x, movementInput.y, 0f).normalized;
        float speedMultiplier = movementInput.x < 0f ? 0.3f : 1f;
        targetVelocity = inputDir * maxSpeed * speedMultiplier;

        if (!isInAir)
        {
            myRB.linearVelocity = Vector3.MoveTowards(myRB.linearVelocity, targetVelocity, acceleration * Time.fixedDeltaTime);
        }

        wasInAirLastFrame = isInAir;
        isInAir = transform.position.y > surfaceY;

        if (isInAir)
        {
            myRB.linearDamping = airDrag;
            myRB.AddForce(Physics.gravity * airborneGravityMultiplier, ForceMode.Acceleration);
            timeController.timeRemaining = timeController.maxTime;
            if (!wasInAirLastFrame)
                movementInput = Vector2.zero;
        }
        else
        {
            myRB.linearDamping = waterDrag;

            float distanceToSurface = surfaceY - transform.position.y;
            if (distanceToSurface <= bobRange && myRB.linearVelocity.y > -0.5f)
            {
                float bobStrength = Mathf.Clamp01(1f - (distanceToSurface / bobRange));
                myRB.AddForce(Vector3.up * bobForce * bobStrength, ForceMode.Acceleration);
            }
        }
    }

    void ApplyTilt(Vector2 input)
    {
        float tilt = 0f;

        if (Mathf.Abs(input.x) > 0.1f && Mathf.Abs(input.y) > 0.1f)
        {
            tilt = input.y * tiltAngle;
        }

        Quaternion baseRotation = Quaternion.Euler(0f, 0f, 270f);
        Quaternion tiltRotation = Quaternion.Euler(0f, 0f, tilt * currentDirection);
        Quaternion finalRotation = Quaternion.Slerp(myAvatar.localRotation, baseRotation * tiltRotation, Time.deltaTime * tiltSmoothing);

        myAvatar.localRotation = finalRotation;
    }


    private void SetupInput()
    {
        movementAction = new InputAction("Movement", InputActionType.Value, null, null, null, "Vector2");

        movementAction.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");

        movementAction.AddBinding("<Gamepad>/leftStick");

        toggleLightsAction = new InputAction("ToggleLights", InputActionType.Button);
        toggleLightsAction.AddBinding("<Keyboard>/h");
        toggleLightsAction.AddBinding("<Gamepad>/buttonSouth");

        toggleLightsAction.Enable();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("LootCrate"))
        {
            Destroy(other.gameObject);
            crateManager.crateCount++;
        }
        if (other.gameObject.CompareTag("Bomb")) {
            timeController.GameOver();
        }
    }
}