using System.Collections.Generic;
using UnityEngine;

public class PlayerInputManager : InputManager
{
    public int playerId = 0;
    public Dictionary<string, string> buttonIdDict;

    private bool m_isDPadXInUse = false;
    private bool m_isDPadYInUse = false;
    private float m_timer = 0f;
    private float m_previousTime = 0f;

    void Awake()
    {
        Initialize();
    }

    void Update()
    {
        m_timer += Time.realtimeSinceStartup - m_previousTime;
        m_previousTime = Time.realtimeSinceStartup;

        if (!(m_timer - Time.fixedDeltaTime > 0f) || !CheckDPadAxis())
        {
            return;
        }

        ResetDPadAxis();
        m_timer = 0f;
    }

    private bool CheckDPadAxis()
    {
        return m_isDPadXInUse || m_isDPadYInUse;
    }

    private void ResetDPadAxis()
    {
        m_isDPadXInUse = Mathf.Abs(Input.GetAxis(buttonIdDict["dpad_x"])) > 0f;
        m_isDPadYInUse = Mathf.Abs(Input.GetAxis(buttonIdDict["dpad_y"])) > 0f;
    }

    protected virtual void InitializeInputIdDicts()
    {
        inputIdDict = new Dictionary<string, string>();

        buttonIdDict = new Dictionary<string, string>()
        {
            { "dpad_x", "p{0}_dpad_x" },
            { "dpad_y", "p{0}_dpad_y" }
        };
    }

    protected override void Initialize()
    {
        InitializeInputIdDicts();
        MapPlayerToInputDict(buttonIdDict);
        MapPlayerToInputDict(inputIdDict);
    }

    protected virtual void MapPlayerToInputDict(
        Dictionary<string, string> inputDict)
    {
        // we append the player id to each key
        List<string> keys = new List<string>(inputDict.Keys);

        foreach (string key in keys)
        {
            inputDict[key] = string.Format(
                inputDict[key],
                playerId
            );
        }
    }

    public bool DPadRightDown()
    {
        if (m_isDPadXInUse)
        {
            return false;
        }

        m_isDPadXInUse = Input.GetAxis(buttonIdDict["dpad_x"]) > 0f;

        return m_isDPadXInUse;
    }

    public bool DPadDownDown()
    {
        if (m_isDPadYInUse)
        {
            return false;
        }

        m_isDPadYInUse = Input.GetAxis(buttonIdDict["dpad_y"]) < 0f;

        return m_isDPadYInUse;
    }
}
