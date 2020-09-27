using System.Collections.Generic;

public class PlayerEntityInputManager : EntityInputManager
{
    protected override void InitializeInputIdDicts()
    {
        base.InitializeInputIdDicts();

        inputIdDict = new Dictionary<string, string>
        {
            { "fight_horizontal", "p{0}_fight_horizontal" },
            { "fight_vertical", "p{0}_fight_vertical" },
            { "fight_jump", "p{0}_fight_jump" }
        };
    }

    protected override void Initialize()
    {
        InitializeInputIdDicts();

        MapPlayerToInputDict(buttonIdDict);
        MapPlayerToInputDict(inputIdDict);
    }
}
