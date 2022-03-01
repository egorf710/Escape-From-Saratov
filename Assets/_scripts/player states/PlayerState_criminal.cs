using UnityEngine;

[CreateAssetMenu(fileName = "player_criminal", menuName = "Game/player state/player criminal")]
public class PlayerState_criminal : Player_state
{
    public override void Init()
    {
        state_name = "criminal";
        _player.sprite.color = _player.spriteColorInCriminal;
    }

    public override void Run()
    {
        return;
    }
}
