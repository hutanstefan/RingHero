using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int HP;
    public int Mana;
    public int Stamina;
    public int MAX_HP = 100;
    public int MAX_Mana = 100;
    public int MAX_Stamina = 5000;
    public int DMG;
    public int Arrmor;

    public HPDisplay hp_display;
    public ManaDisplay mana_display;
    public SprintDisplay sprint_display;
    public GameObject inv;

    void Start()
    {
        HP = MAX_HP;
        Mana = MAX_Mana;
        Stamina = MAX_Stamina;

        hp_display.UpdateBar(MAX_HP, HP);
        mana_display.UpdateBar(MAX_Mana, Mana);
    }


    void FixedUpdate()
    {
        if (Mana + 10 < MAX_Mana)
            Mana += 10;

        if (HP < 50 && HP > -100)
        {
            StaticInterface stinv = inv.GetComponent<StaticInterface>();
            stinv.UseItem();
        }

        mana_display.SetMana(Mana);
        hp_display.SetHealth(HP);
        sprint_display.SetStamina(Stamina);
    }


    public void TakeDMG(int dmg)
    {
        HP -= dmg;
        hp_display.SetHealth(HP);
        Debug.Log(HP);
    }

    public void CastSpell(int mana)
    {
        Mana -= mana;
        mana_display.SetMana(Mana);
    }

    public bool EnoughMana(int mana)
    {
        if ( mana > Mana)
            return false;
        return true;
    }

    public void StaminaUse()
    {
        if(Stamina > 0)
            Stamina-=5;
    }

    public void Heal(int heal)
    {
        if (HP + heal > MAX_HP) HP = MAX_HP;
        else HP += heal;
        hp_display.SetHealth(HP);
    }

}
