using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct player_Info
{
    private int _player_ID;
    public int Player_ID
    {
        get
        {
            return _player_ID;
        }

        set
        {
            _player_ID = value;
        }
    }

    private Color _color;
    public Color Color
    {
        get
        {
            return _color;
        }

        set
        {
            _color = value;
        }
    }

    private int _skill;
    public int Skill
    {
        get
        {
            return _skill;
        }

        set
        {
            _skill = value;
        }
    }

    public player_Info(int id, Color c, int num)
    {
        _player_ID = id;
        _color = c;
        _skill = num;
    }
}

public static class Info {

    public static int _people;

    public static player_Info[] _player_Infos;

}
