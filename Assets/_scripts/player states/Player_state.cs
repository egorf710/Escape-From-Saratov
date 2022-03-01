using UnityEngine;

public abstract class Player_state : ScriptableObject
{
    /*
     * «ачем € решил использовать эту систему?
     * Ќе нужно будет создавать лишние bool-ы дл€ проверки состо€ни€
     * Ѕудет проще реализовать логику не засор€€ основной скрипт
     * 
     *  ак здесь писать логику?
     * смена параметров у игрока происходит вот так(дл€ примера): _player.sprite.color = _player.spriteColorInCriminal главное чтобы переменные були публичными у игрока
     * так же можно использовать функции у игрока: _player.SomeFunction() главное чтобы метод был публичным
     * (надеюсь пон€тно, если нет то можешь чекнуть скрипты PlayerState_criminal или PlayerState_police)
    */

    [HideInInspector] public PlayerController _player; // нужно дл€ взаимодействи€ со скриптом игрока
    public string state_name; // нужно дл€ проверки какое конкретно состо€ние у игрока. ѕример: if(state_current.state_name == "criminal") { <some logic> }                                          

    public virtual void Init() { } // работает как Start
    public abstract void Run(); // работает как Update
}
