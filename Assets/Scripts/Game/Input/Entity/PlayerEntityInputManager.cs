using System.Collections.Generic;

public class PlayerEntityInputManager : EntityInputManager {
    protected override void InitializeInputIdDicts() {
        base.InitializeInputIdDicts();

        this.InputIdDict = new Dictionary<string, string> {
            { "fight_horizontal", "p{0}_fight_horizontal" },
            { "fight_vertical", "p{0}_fight_vertical" }
        };
    }

    protected override void Initialize() {
        this.InitializeInputIdDicts();

        this.MapPlayerToInputDict(this.ButtonIdDict);
        this.MapPlayerToInputDict(this.InputIdDict);
    }
}
