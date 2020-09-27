using System.Collections.Generic;
using UnityEngine;

public class DebugInputManager : PlayerInputManager
{
    protected override void InitializeInputIdDicts()
    {
        base.InitializeInputIdDicts();

        inputIdDict = new Dictionary<string, string>
        {
            { "frame_by_frame", "p{0}_frame_by_frame" },
            { "next_frame", "p{0}_next_frame" }
        };
    }

    public virtual bool IsFrameByFrame()
    {
        return DPadDownDown() || Input.GetButtonDown(inputIdDict["frame_by_frame"]);
    }

    public virtual bool IsNextFrame()
    {
        return DPadRightDown() || Input.GetButtonDown(inputIdDict["next_frame"]);
    }
}
