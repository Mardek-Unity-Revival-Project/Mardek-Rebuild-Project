using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Fighter
{
    public float STR;
    public float VIT;
    public float SPR;
    public float AGI;
    public float DEF;
    public float MDEF;
    public float EVA; //in %
    public float ACC; //in %
    public float ATK;
    public string nombre;
    public string sprite;
    public int MHP;
    public int MMP;
    public int HP;
    public int MP;
    public int INT;
    public int LVL;
    public bool lock_HP;
    public bool lock_MP;
    public bool alive;

    public Fighter()
    {
        alive = false;
    }
    public Fighter(string _nombre, string _sprite, int _STR, int _VIT, int _SPR, int _AGI, int _DEF, int _MDEF, int _INT, int _LVL, int _MHP = 0, int _MMP = 0, float _ATK = 25, float _EVA = 10, float _ACC = 90)
    {
        nombre = _nombre;
        sprite = _sprite;
        STR = _STR;
        VIT = _VIT;
        SPR = _SPR;
        AGI = _AGI;
        DEF = _DEF;
        MDEF = _MDEF;
        INT = _INT;
        LVL = _LVL;
        ACC = _ACC;
        EVA = _EVA;
        ATK = _ATK;
        if (_MHP == 0)
        {
            MHP = (int)(3 * VIT + 2 * VIT * LVL);
            lock_HP = false;
        }

        else
        {
            MHP = _MHP;
            lock_HP = true;
        }

        if (_MMP == 0)
        {
            MMP = (int)(SPR * 17 / 6 + SPR * LVL / 6);
            lock_MP = false;
        }
        else
        {
            MMP = _MMP;
            lock_MP = true;
        }
        MP = MMP;
        HP = MHP;
        alive = true;
    }
   

    public int calculate_damage(float pierce, float power, Fighter attacker, char type)
    {
        Random rnd = new Random();
        int random = rnd.Next(-20, 21);
        double damage = 0;
        if (type == 'M')
        {
            damage=((pierce - MDEF) * power*attacker.SPR * Math.Pow(1.07177346, attacker.LVL-LVL) *(1+random/100)/ 50); //2=1.07177346^10
        }
        if (type == 'P')
        {
            damage = ((pierce - DEF) * power * attacker.STR * Math.Pow(1.07177346, attacker.LVL - LVL) * (1 + random / 100) / 50);
        }
        if (damage < 0)
            return 0;
        return (int)damage;
    }

}
