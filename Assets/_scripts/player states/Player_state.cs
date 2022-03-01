using UnityEngine;

public abstract class Player_state : ScriptableObject
{
    /*
     * ����� � ����� ������������ ��� �������?
     * �� ����� ����� ��������� ������ bool-� ��� �������� ���������
     * ����� ����� ����������� ������ �� ������� �������� ������
     * 
     * ��� ����� ������ ������?
     * ����� ���������� � ������ ���������� ��� ���(��� �������): _player.sprite.color = _player.spriteColorInCriminal ������� ����� ���������� ���� ���������� � ������
     * ��� �� ����� ������������ ������� � ������: _player.SomeFunction() ������� ����� ����� ��� ���������
     * (������� �������, ���� ��� �� ������ ������� ������� PlayerState_criminal ��� PlayerState_police)
    */

    [HideInInspector] public PlayerController _player; // ����� ��� �������������� �� �������� ������
    public string state_name; // ����� ��� �������� ����� ��������� ��������� � ������. ������: if(state_current.state_name == "criminal") { <some logic> }                                          

    public virtual void Init() { } // �������� ��� Start
    public abstract void Run(); // �������� ��� Update
}
