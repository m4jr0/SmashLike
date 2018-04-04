using UnityEngine;

public class EntityPhysics : MonoBehaviour {
    public CharacterController CharacterController;

    public bool IsGrounded { get; protected set; }
    public float GroundDistance = 0.2f;
    public Transform GroundChecker;
    public LayerMask Ground;

    public int Direction {
        get { return this._direction; }
        set {
            this._direction = value;
            this.transform.forward = new Vector3(this._direction, 0, 0);
        }
    }

    private int _direction;

    public float FallingSpeed = 2f;
    public float WalkSpeed = 2f;
    public float DashInitialSpeed = 5f;
    public float RunSpeed = 4f;
    public Vector3 Velocity = Vector3.zero;

    public float CurrentSpeed {
        get { return this._currentSpeed; }
        set { this._currentSpeed = Mathf.Clamp(0, value, this.RunSpeed); }
    }

    public virtual void SwapDirection() {
        this.Direction = -this.Direction;
    }

    private float _currentSpeed;

    void Start() {
        this.Initialize();
    }

    void Update() {
        this.CheckIfGrounded();
        this.UpdateFallingSpeed();
        this.UpdateVelocity();
    }

    public virtual void CheckIfGrounded() {
        this.IsGrounded = Physics.CheckSphere(
            GroundChecker.position,
            GroundDistance,
            Ground,
            QueryTriggerInteraction.Ignore
        );

        if (this.IsGrounded && this.Velocity.y < 0) this.Velocity.y = 0f;
    }

    public virtual void UpdateFallingSpeed() {
        this.Velocity.y -= this.FallingSpeed * Time.deltaTime;
    }

    public virtual void UpdateVelocity() {
        this.CharacterController.Move(this.Velocity * Time.deltaTime);
    }

    public virtual void Initialize() {
        if (this.gameObject.transform.eulerAngles.y > 0) {
            this._direction = -1;
        } else {
            this._direction = 1;
        }
    }

    public virtual void Walk(float factor = 1f) {
        this.Move(this.WalkSpeed * factor);
    }

    public virtual void Run(float factor = 1f) {
        this.Move(this.RunSpeed * factor);
    }

    public virtual void Move(float speed = 1.0f) {
        Vector3 toMove = new Vector3(this.Direction, 0, 0) * 
                         Time.deltaTime * speed;

        this.CharacterController.Move(toMove);
    }

    public virtual void Accelerate(float acceleration = 1.0f) {
        this.CurrentSpeed += acceleration;

        this.Move(this.CurrentSpeed);
    }

    public virtual void Jump() {
        this.Velocity.y += Mathf.Sqrt(1 * 2f * this.FallingSpeed);
    }
}
