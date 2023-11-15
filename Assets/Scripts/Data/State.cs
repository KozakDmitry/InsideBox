using System;

[Serializable]
public class State
{
    public float CurrentHP;
    public float MaxHp;

    public void ResetHp() => CurrentHP = MaxHp;
}