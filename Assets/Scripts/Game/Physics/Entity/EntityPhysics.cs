using UnityEngine;

public class EntityPhysics : MonoBehaviour {
    public CharacterController CharacterController;

    public int Direction {
        get { return this._direction; }
        set {
            if (value > 0) {
                // prevent from rotating the object if the direction remains
                // the same
                if (this._direction == 1) return;

                this._direction = 1;
            } else {
                // prevent from rotating the object if the direction remains
                // the same
                if (this._direction == -1) return;

                this._direction = -1;
            }

            this.gameObject.transform.eulerAngles = new Vector3(
                this.gameObject.transform.eulerAngles.x,
                this.gameObject.transform.eulerAngles.y -
                    this._direction * 180,
                this.gameObject.transform.eulerAngles.z
            );
        }
    }

    private int _direction;

    public float FallSpeed = 2f;
    public float WalkSpeed = 2f;
    public float DashInitialSpeed = 5f;
    public float RunSpeed = 4f;
    public float Velocity = 0f;

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
}
