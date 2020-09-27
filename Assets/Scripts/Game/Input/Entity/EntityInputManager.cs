using System;
using System.Collections.Generic;
using UnityEngine;

public class EntityInputManager : PlayerInputManager
{
    public EntityPhysics physics;
    public float moveThreshold = 0.004f;
    public int dashCounterThreshold = 3;
    public float dashThreshold = .8f;

    public float movePos
    {
        get
        {
            return m_movePos;
        }
        set
        {
            m_previousMovePos = m_movePos;
            m_movePos = value;
        }
    }

    private float m_movePos;

    public float moveVel
    {
        get
        {
            return m_moveVel;
        }
        set
        {
            m_previousMoveVel = m_moveVel;
            m_moveVel = value;
        }
    }

    private float m_moveVel;

    public float moveAcc
    {
        get
        {
            return m_moveAcc;
        }
        set
        {
            m_previousMoveAcc = m_moveAcc;
            m_moveAcc = value;
        }
    }

    private float m_moveAcc;
    private float m_previousMovePos = 0f;
    private float m_previousMoveVel = 0f;
    private float m_previousMoveAcc = 0f;

    public int moveDir
    {
        get
        {
            return m_moveDir;
        }
        private set
        {
            if (moveDir != value)
            {
                m_moveDir = value;
            }
        }
    }

    private int m_moveDir = -1;

    private int m_dashCounter = 0;
    private bool m_isDashed = false;

    protected override void InitializeInputIdDicts()
    {
        base.InitializeInputIdDicts();

        inputIdDict = new Dictionary<string, string>() {
            {"fight_horizontal", "fight_horizontal"},
            {"fight_vertical", "fight_vertical"},
            {"fight_jump", "fight_jump"}
        };
    }

    protected override void Initialize()
    {
        InitializeInputIdDicts();
    }

    void FixedUpdate()
    {
        movePos = GetMovePos();
        moveVel = GetMoveVel();
        moveAcc = GetMoveAcc();
        moveDir = GetMoveDir();

        UpdateDash();
    }

    public virtual float GetMovePos()
    {
        float movePos = 0f;
        movePos += Input.GetAxis(inputIdDict["fight_horizontal"]);

        return -movePos;
    }

    public virtual float GetMoveVel()
    {
        float distancePerFrame = Mathf.Abs(
            movePos - m_previousMovePos
        );

        return distancePerFrame / Time.deltaTime;
    }

    public virtual float GetMoveAcc()
    {
        float velocityDeltaPerFrame = moveVel - m_previousMoveVel;
        return velocityDeltaPerFrame / Time.deltaTime;
    }

    public virtual int GetMoveDir()
    {
        int sign = Math.Sign(movePos);
        int newDir = sign == 0 ? moveDir : sign;

        if (newDir != moveDir)
        {
            ResetDash();
        }

        return newDir;
    }

    protected virtual void UpdateDash()
    {
        if (Mathf.Abs(movePos) > dashThreshold)
        {
            m_isDashed = true;

            return;
        }

        if (Math.Abs(movePos) < moveThreshold)
        {
            ResetDash();

            return;
        }

        m_dashCounter++;
    }

    protected virtual void ResetDash()
    {
        m_dashCounter = 0;
        m_isDashed = false;
    }

    public virtual bool IsMove()
    {
        return Mathf.Abs(movePos) >= moveThreshold;
    }

    public virtual bool IsWalk()
    {
        return IsMove() && !IsDash();
    }

    public virtual bool IsDash()
    {
        return m_isDashed && m_dashCounter <= dashCounterThreshold;
    }

    public virtual bool IsJump()
    {
        return physics.isGrounded && Input.GetButton(inputIdDict["fight_jump"]);
    }
}
