using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDied : MonoBehaviour
{
    public GameObject diedUi;
    public InventoryObject inventory;
    public InventoryObject equipment;
    public PlayerStats playerS;
    public GameObject player;
    public BossStatus boss;
    public TriggerBoss trigger;
    private Vector3 spawnCoordinates = new Vector3(4f, 0.2f, 9f);

    public bool died;
    
    void Start()
    {
        inventory.Save();
        equipment.Save();
    }

    void Update()
    {
        
        if(playerS.HP <= 0 && !died)
        {
            StartCoroutine(Died());
        }

    }

    private IEnumerator Died()
    {
        died = true;
        diedUi.SetActive(true);
        
        player.transform.position = spawnCoordinates;
        playerS.HP = playerS.MAX_HP;
        playerS.Mana = playerS.MAX_Mana;
        playerS.Stamina = playerS.MAX_Stamina;

        boss.HP = boss.MAX_HP;
        boss.display.UpdateBar(boss.HP, boss.MAX_HP);
        boss.SetIdleStatusTrue();
        trigger.ResetTrigger();
        boss.SetSpawn();
        

        yield return new WaitForSeconds(5f);

        diedUi.SetActive(false);
        inventory.Load();
        equipment.Load();
    
        died = false;   
    }
    
}
