using System;
using System.Collections.Generic;
using UnityEngine;

public class EntityInputManager : PlayerInputManager
{
    public EntityPhysics physics;
    public int dashCounterThreshold = 3;

    public float slowWalkThreshold = .2f;
    public float dashThreshold = .8f;

    public float movePos
    {
        get; protected set;
    }

    public int moveDir
    {
        get; protected set;
    } = -1;

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
        UpdateMovePos();
        UpdateMoveDir();
    }

    protected virtual void UpdateMovePos()
    {
        movePos = -Input.GetAxis(inputIdDict["fight_horizontal"]);
        Debug.Log(movePos);
    }

    protected virtual void UpdateMoveDir()
    {
        int sign = Math.Sign(movePos);
        moveDir = sign == 0 ? moveDir : sign;
    }

    public virtual bool IsMove()
    {
        return Mathf.Abs(movePos) > 0;
    }

    public virtual bool IsWalk()
    {
        float absMovePos = Math.Abs(movePos);
        return absMovePos >= slowWalkThreshold && absMovePos < dashThreshold;
    }

    public virtual bool IsDash()
    {
        float absMovePos = Math.Abs(movePos);
        return absMovePos >= dashThreshold;
    }

    public virtual bool IsJump()
    {
        return physics.isGrounded && Input.GetButton(inputIdDict["fight_jump"]);
    }
}
