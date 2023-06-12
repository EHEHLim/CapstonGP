using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGizmo : MonoBehaviour
{
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(this.transform.position, new Vector2(2, 2));
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawCube(this.transform.position, new Vector2(2, 2));
    }
}
