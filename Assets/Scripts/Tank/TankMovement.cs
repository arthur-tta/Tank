using UnityEngine;

public class TankMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject cannon;

    private Rigidbody rigidbody;


    private Vector3 inputVector;
    private Vector3 lookAtPosition;


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
        // Adjust the rotation of the tank based on the player's input.
        // Lấy vị trí của chuột
        Vector3 mousePos = Input.mousePosition;

        // Chuyển vị trí của chuột từ màn hình sang thế giới trong game
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.transform.position.y));

        // Xác định hướng quay của khẩu đại bác
        Vector3 direction = mouseWorldPos - transform.position;
        direction.y = 0; // Giữ cho khẩu đại bác chỉ quay theo trục X và Z

        // Xác định hướng quay mới
        Quaternion rotation = Quaternion.LookRotation(direction);

        // Áp dụng việc quay mềm dần (sử dụng Slerp để tránh việc quay đột ngột)
        cannon.transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 100 * Time.deltaTime);
    }
}