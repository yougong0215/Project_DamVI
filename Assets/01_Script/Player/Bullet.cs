using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : PoolAble
{
    [SerializeField] int damage = 0;
    [SerializeField] float stun = 0;
    [SerializeField] Vector3 NuckBack = Vector3.zero;
    [SerializeField] bool Grab;
    [SerializeField] float DelayTime = 0;

    Collider[] col;
    Vector3 dir;
    private void OnEnable()
    {
        dir = GameManager.Instance.Player.localRotation * Vector3.forward;
        dir.y = 0;
        dir.Normalize();
    }

    private void Update()
    {
        transform.position += dir* 10 * Time.deltaTime;
        Debug.Log(dir);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(1.5f, 1.5f, 1.5f));
    }

    private void OnTriggerEnter(Collider other)
    {
            dir = Vector3.zero;

            col = Physics.OverlapBox(new Vector3(GameManager.Instance.Player.position.x, GameManager.Instance.Player.position.y + 1f, GameManager.Instance.Player.position.z + 1f)
           , new Vector3(1.5f, 1.5f, 1.5f), Quaternion.identity, 1 << (LayerMask.NameToLayer("InterectionObj"))); ;

            for (int i = 0; i < col.Length; i++)
            {
                if (col[i].GetComponent<EnemyBase>())
                {
                    col[i].GetComponent<EnemyBase>().DamagedForPlayer(damage, stun, NuckBack, Grab, DelayTime);
                }
            }
        
    }
    
}
