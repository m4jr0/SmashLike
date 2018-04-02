using System;
using System.Collections.Generic;
using UnityEngine;

public class FighterInputManager : MonoBehaviour {
    public int PlayerId = 0;
    public float MoveThreshold = 0.02f;
    public float DashThreshold = 1000f;
    public Dictionary<string, string> InputIdDict;

    public float MovePos {
        get { return this._movePos; }
        set {
            this._previousMovePos = this._movePos;
            this._movePos = value;
        }
    }

    private float _movePos;

    public float MoveVel {
        get { return this._moveVel; }
        set {
            this._previousMoveVel = this._moveVel;
            this._moveVel = value;
        }
    }

    private float _moveVel;

    public float MoveAcc {
        get { return this._moveAcc; }
        set {
            this._previousMoveAcc = this._moveAcc;
            this._moveAcc = value;
        }
    }

    private float _moveAcc;
    private float _previousMovePos = 0f;
    private float _previousMoveVel = 0f;
    private float _previousMoveAcc = 0f;

    public int MoveDirection {
        get { return this._direction; }
    }

    private int _direction = 1;

    void Awake() {
        this.Initialize();
    }

    protected virtual void Initialize() {
        this.InputIdDict = new Dictionary<string, string>() {
            {"fight_horizontal", "fight_horizontal"},
            {"fight_vertical", "fight_vertical"}
        };
    }

    void FixedUpdate() {
        this.MovePos = this.GetMovePos();
        this.MoveVel = this.GetMoveVel();
        this.MoveAcc = this.GetMoveAcc();
        this._direction = this.GetMoveDir();
    }

    public virtual float GetMovePos() {
        float movePos = 0f;
        movePos += Input.GetAxis(this.InputIdDict["fight_horizontal"]);

        return -movePos;
    }

    public virtual float GetMoveVel() {
        float distancePerFrame = Mathf.Abs(
            this.MovePos - this._previousMovePos
        );

        return distancePerFrame / Time.deltaTime;
    }

    public virtual float GetMoveAcc() {
        float velocityDeltaPerFrame = this.MoveVel - this._previousMoveVel;

        return velocityDeltaPerFrame / Time.deltaTime;
    }

    public virtual int GetMoveDir() {
        int sign = Math.Sign(this.MovePos);

        if (sign == 0) return this._direction;

        return sign;
    }

    public virtual bool IsMove() {
        return Mathf.Abs(this.MovePos) >= this.MoveThreshold;
    }

    public virtual bool IsWalk() {
        return this.IsMove() && !this.IsDash();
    }

    public virtual bool IsDash() {
        return Mathf.Abs(this.MoveAcc) >= this.DashThreshold;
    }
}
