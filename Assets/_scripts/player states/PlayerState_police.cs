using UnityEngine;

[CreateAssetMenu(fileName = "player_police", menuName = "Game/player state/player police")]
public class PlayerState_police : Player_state
{
    public override void Init()
    {
        state_name = "police";
    }

    public override void Run()
    {
        return;
    }
}
