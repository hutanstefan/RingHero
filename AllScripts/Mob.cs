using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mob : MonoBehaviour
{
   public int HP;
   public int MAX_HP;
   public int attack;
   public float speed;
   public GameObject player;
   public PlayerStats playerStats;
   public bool inRange = false;
   public Animator animator;

    public Mob(int _hp,int _attack) 
    {
        MAX_HP = _hp;
        HP = _hp;
        attack = _attack;
    }

    public abstract void Attack();
    public abstract void TakeDMG(int DMG);
    public abstract void Die();
}
