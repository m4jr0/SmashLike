using UnityEngine;

public class EntityPhysics : MonoBehaviour
{
    public CharacterController characterController;

    public bool isGrounded
    {
        get; protected set;
    }
    public float groundDistance = 0.2f;
    public Transform groundChecker;
    public LayerMask ground;

    public int direction
    {
        get
        {
            return m_direction;
        }
        set
        {
            m_direction = value;
            transform.forward = new Vector3(m_direction, 0, 0);
        }
    }

    private int m_direction;

    public float fallingSpeed = 60f;
    public float jumpSpeed = 3f;
    public float walkSpeed = 2f;
    public float dashInitialSpeed = 5f;
    public float runSpeed = 6f;
    public Vector3 velocity = Vector3.zero;

    public float currentSpeed
    {
        get
        {
            return m_currentSpeed;
        }
        set
        {
            m_currentSpeed = Mathf.Clamp(0, value, runSpeed);
        }
    }

    private float m_currentSpeed;

    public virtual void SwapDirection()
    {
        direction = -direction;
    }

    void Start()
    {
        Initialize();
    }

    void Update()
    {
        CheckIfGrounded();
        UpdateFallingSpeed();
        UpdateVelocity();
    }

    public virtual void CheckIfGrounded()
    {
        isGrounded = Physics.CheckSphere(
            groundChecker.position,
            groundDistance,
            ground,
            QueryTriggerInteraction.Ignore
        );

        if (isGrounded)
        {
            velocity.y = 0f;
        }
    }

    public virtual void UpdateFallingSpeed()
    {
        velocity.y -= fallingSpeed * Time.deltaTime;
    }

    public virtual void UpdateVelocity()
    {
        characterController.Move(velocity * Time.deltaTime);
    }

    public virtual void Initialize()
    {
        if (gameObject.transform.eulerAngles.y > 0)
        {
            m_direction = -1;
        }
        else
        {
            m_direction = 1;
        }
    }

    public virtual void Walk(float factor = 1f)
    {
        Move(walkSpeed * factor);
    }

    public virtual void Run(float factor = 1f)
    {
        Move(runSpeed * factor);
    }

    public virtual void Move(float speed = 1.0f)
    {
        Vector3 toMove = new Vector3(direction, 0, 0) * Time.deltaTime * speed;
        characterController.Move(toMove);
    }

    public virtual void Accelerate(float acceleration = 1.0f)
    {
        currentSpeed += acceleration;
        Move(currentSpeed);
    }

    public virtual void Jump()
    {
        velocity.y += Mathf.Sqrt(jumpSpeed * 2f * fallingSpeed);
    }
}
