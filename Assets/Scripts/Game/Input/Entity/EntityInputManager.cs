using System;
using System.Collections.Generic;
using UnityEngine;

public class EntityInputManager : PlayerInputManager {
    public EntityPhysics Physics;
    public float MoveThreshold = 0.01f;
    public int DashCounterThreshold = 3;
    public float DashThreshold = .8f;

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

    public int MoveDir {
        get { return this._moveDir; }
        private set {
            if (this.MoveDir == value) return;
            this._moveDir = value;
        }
    }

    private int _moveDir = -1;

    private int _dashCounter = 0;
    private bool _isDashed = false;

    protected override void InitializeInputIdDicts() {
        base.InitializeInputIdDicts();

        this.InputIdDict = new Dictionary<string, string>() {
            {"fight_horizontal", "fight_horizontal"},
            {"fight_vertical", "fight_vertical"},
            {"fight_jump", "fight_jump"}
        };
    }

    protected override void Initialize() {
        this.InitializeInputIdDicts();
    }

    void FixedUpdate() {
        this.MovePos = this.GetMovePos();
        this.MoveVel = this.GetMoveVel();
        this.MoveAcc = this.GetMoveAcc();
        this.MoveDir = this.GetMoveDir();

        this.UpdateDash();
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

        int newDir = sign == 0 ? this.MoveDir : sign;

        if (newDir != this.MoveDir) this.ResetDash();

        return newDir;
    }

    public virtual void UpdateDash() {
        if (Mathf.Abs(this.MovePos) > this.DashThreshold) {
            this._isDashed = true;

            return;
        }

        if (Math.Abs(this.MovePos) < this.MoveThreshold) {
            this.ResetDash();

            return;
        }

        this._dashCounter++;
    }

    public virtual void ResetDash() {
        this._dashCounter = 0;
        this._isDashed = false;
    }

    public virtual bool IsMove() {
        return Mathf.Abs(this.MovePos) >= this.MoveThreshold;
    }

    public virtual bool IsWalk() {
        return this.IsMove() && !this.IsDash();
    }

    public virtual bool IsDash() {
        return this._isDashed && 
               this._dashCounter <= this.DashCounterThreshold;
    }

    public virtual bool IsJump() {
        return this.Physics.IsGrounded && 
               Input.GetButton(this.InputIdDict["fight_jump"]);
    }
}
