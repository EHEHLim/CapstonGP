using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGizmo : MonoBehaviour
{
    [SerializeField]private Transform[] monsterSpawnPoints;
    

    private void Awake()
    {
        monsterSpawnPoints = GetComponentsInChildren<Transform>();
        foreach(var item in monsterSpawnPoints)
        {
            int index = Random.Range(0, GameManager.Instance.monsters.Length);
            Instantiate(GameManager.Instance.monsters[index], item.position, item.rotation);
        }
    }
    void OnDrawGizmos()
    {
        for(int i = 0; i < monsterSpawnPoints.Length; i++)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(monsterSpawnPoints[i].position, new Vector3(2, 2));
        }
        
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(this.transform.position, new Vector2(2, 2));
    }
}
