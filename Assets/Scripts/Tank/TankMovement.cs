using UnityEngine;

public class TankMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject cannon;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody rigidbody;


    private Vector3 inputVector;
    private Vector3 lookAtPosition;

    private Camera mainCamera;


    /*
    public int m_PlayerNumber = 1;         
    public float m_Speed = 12f;            
    public float m_TurnSpeed = 180f;       
    public AudioSource m_MovementAudio;    
    public AudioClip m_EngineIdling;       
    public AudioClip m_EngineDriving;      
    public float m_PitchRange = 0.2f;
    */
    /*
    private string m_MovementAxisName;     
    private string m_TurnAxisName;         
    private Rigidbody m_Rigidbody;         
    private float m_MovementInputValue;    
    private float m_TurnInputValue;        
    private float m_OriginalPitch;         


    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }


    private void OnEnable ()
    {
        m_Rigidbody.isKinematic = false;
        m_MovementInputValue = 0f;
        m_TurnInputValue = 0f;
    }


    private void OnDisable ()
    {
        m_Rigidbody.isKinematic = true;
    }


    private void Start()
    {
        m_MovementAxisName = "Vertical" + m_PlayerNumber;
        m_TurnAxisName = "Horizontal" + m_PlayerNumber;

        m_OriginalPitch = m_MovementAudio.pitch;
    }
    */


    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        rigidbody.isKinematic = false;
        mainCamera = Camera.main;

    }

    private void OnDisable()
    {
        rigidbody.isKinematic = false;
    }

    private void Update()
    {
        // Store the player's input and make sure the audio for the engine is playing.
        inputVector = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        lookAtPosition = Input.mousePosition;
        lookAtPosition.y = transform.position.y;

    }


    private void EngineAudio()
    {
        // Play the correct audio clip based on whether or not the tank is moving and what audio is currently playing.
    }


    private void FixedUpdate()
    {
        Move();
        Turn();
    }


    private void Move()
    {
        // Adjust the position of the tank based on the player's input.

        //movement = transform.forward * inputVector.y + transform.right * inputVector.x;

        rigidbody.MovePosition(rigidbody.position + inputVector * speed * Time.fixedDeltaTime);

    }


    private void Turn()
    {
        //Debug.Log(mainCamera.name);
        
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        
        Vector3 target;
        if(Physics.Raycast(ray, out var hit, Mathf.Infinity, groundLayer))
        {
            target = hit.point;
        }
        else
        {
            target = Vector3.zero;
        }

        Vector3 direction = new Vector3(target.x - cannon.transform.position.x, 0, target.z - cannon.transform.position.z);

        //Debug.Log(direction);
        cannon.transform.forward = direction;
        
    }
}