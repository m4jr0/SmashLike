using System.Collections.Generic;

public class PlayerFighterInputManager : FighterInputManager {
    protected override void Initialize() {
        this.InputIdDict = new Dictionary<string, string> {
            { "fight_horizontal", "p{0}_fight_horizontal" },
            { "fight_vertical", "p{0}_fight_vertical" }
        };

        // we append the player id to each key
        List<string> keys = new List<string>(this.InputIdDict.Keys);

        foreach (string key in keys) {
            this.InputIdDict[key] = string.Format(
                this.InputIdDict[key],
                this.PlayerId
            );
        }
    }
}
