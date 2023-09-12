using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxControl : MonoBehaviour
{
    // Start is called before the first frame update
    public void dustStart()
    {
        GetComponent<SpriteRenderer>().flipX = false;
        Debug.Log("dusts on");
        transform.position = GameManager.Instance.player.transform.position;
        if (GameManager.Instance.player.GetComponent<SpriteRenderer>().flipX)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            transform.position += new Vector3(0.5f, 0f);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void animEnd()
    {
        Debug.Log("fx end");
        gameObject.SetActive(false);
    }

    public void JumpDustStart()
    {
        transform.position = GameManager.Instance.player.transform.position;
        transform.position -= new Vector3(0, 0f);
    }
}
