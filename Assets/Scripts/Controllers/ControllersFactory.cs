using UnityEngine;

public class ControllersFactory
{
    public AgentCharacterPointToMoveController CreateAgentCharacterPointToMoveController(
        AgentCharacter character,
        IPointToMoveInput moveInput,
        ISelectedPositionView positionView)
    {
        return new AgentCharacterPointToMoveController(character, moveInput, positionView);
    }

    public AgentСharacterWanderingMoveController CreateAgentСharacterWanderingMoveController(
        AgentCharacter character,
        float timeToWanderStart,
        float wanderingAreaRadius)
    {
        return new AgentСharacterWanderingMoveController(character, timeToWanderStart, wanderingAreaRadius);
    }

    public AlongMovableVelocityRotatableController CreateAlongMovableVelocityRotatableController(
        IDirectionalMovable movable,
        IDirectionalRotatable rotatable,
        AgentCharacter character)
    {
        return new AlongMovableVelocityRotatableController(movable, rotatable, character);
    }

    public MovementBehaviorStateController CreateMovementBehaviorStateController(
        AgentCharacterPointToMoveController pointToMoveController, 
        Controller controller)
    {
        return new MovementBehaviorStateController(pointToMoveController, controller);
    }

    public CompositeController CreateMainHeroController(
        AgentCharacter character,
        float timeToWanderStart,
        float wanderingAreaRarius,
        IPointToMoveInput moveInput,
        ISelectedPositionView positionView)
    {
        return new CompositeController(
            CreateMovementBehaviorStateController(
                CreateAgentCharacterPointToMoveController(character, moveInput, positionView), 
                CreateAgentСharacterWanderingMoveController(character, timeToWanderStart, wanderingAreaRarius)),
            CreateAlongMovableVelocityRotatableController(character, character, character));
    }
}
