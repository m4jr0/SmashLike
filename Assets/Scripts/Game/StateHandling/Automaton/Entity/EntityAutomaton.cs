/* EntityAutomaton is an object that contains all the FSMs and other useful
 * attributes/methods necessary for it. */

public class EntityAutomaton : Automaton
{
    public EntityInputManager inputManager;
    public EntityPhysics physics;
    public EntityStateMachine stateMachine;

    void Awake()
    {
        stateMachine = gameObject.GetComponent<EntityStateMachine>();
    }
}
