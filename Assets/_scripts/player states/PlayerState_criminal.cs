using UnityEngine;

[CreateAssetMenu(fileName = "player_criminal", menuName = "Game/player state/player criminal")]
public class PlayerState_criminal : Player_state
{
    public override void Init()
    {
        state_name = "criminal";
    }

    public override void Run()
    {
        return;
    }
}
