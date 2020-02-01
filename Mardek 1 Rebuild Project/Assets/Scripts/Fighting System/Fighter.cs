using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter
{
    public float STR;
    public float VIT;
    public float SPR;
    public float AGI;
    public float DEF;
    public float MDEF;
    public int MHP;
    public int MMP;
    public int HP;
    public int MP;
    public int INT;
    public int LVL;
    public int EVA;
    public int ACC;
    public int ATK;



   
    Fighter(int  _STR, int _VIT, int _SPR, int _AGI, int _DEF, int _MDEF, int _INT, int _LVL, int _MHP=0, int _MMP=0, int _ATK = 25, int _EVA=10, int _ACC=90)
    {
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
            MHP = (int)(3 * VIT + 2 * VIT * LVL);
        else
            MHP = _MHP;

        if (_MMP == 0)
            MMP = (int)(SPR * 17 / 6 + SPR * LVL / 6);
        else
            MMP = _MMP;
        MP = MMP;
        HP = MHP;
    }
    
}
