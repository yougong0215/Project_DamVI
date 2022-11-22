using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("플레이어 스텟")]
    [SerializeField] int HP = 30;
    [SerializeField] float ATK = 1;

    [Header("가장 가까운 물체")]
    [SerializeField] BaseInteraction MatchingObject = null;
    [SerializeField] EnemyBase EnemyMatchingObject = null;


    [Header("범위네 있는 물체")]
    [SerializeField] Collider[] hit;
    [SerializeField] List<float> _length = new List<float>();
    [SerializeField] List<EnemyBase> _enemy = new List<EnemyBase>();

    [Header("몰?루")]
    [SerializeField] LayerMask layer;

    private void OnEnable()
    {
        //hit[0].transform.position - transform.position < hit[i].transform.position - transform.position
        StartCoroutine(InteractionCheck());
    }

    public void Damaged(int dam)
    {
        HP -= dam;
        PlayerAttackManager.Instance._ani.SetTrigger("Block");
    }

    public float CalcDamage()
    {
        return ATK;
    }
    public Transform DistannsEnemy()
    {
        if(EnemyMatchingObject != null)
        {
            return EnemyMatchingObject.GetComponent<Transform>();
        }
        else
        {
            return null;
        }
    }


    IEnumerator InteractionCheck()
    {
        while(true)
        {

            hit = Physics.OverlapBox(transform.position, new Vector3(30, 30, 30), Quaternion.identity, layer);
            MatchingObject = null;
            if (hit != null)
            {
                try
                {
                    //Debug.Log(hit.Length);
                    for (int i = 0; i < hit.Length; i++)
                    {
                        _length.Add(
                              Mathf.Sqrt(
                              Mathf.Pow(hit[i].transform.position.x - transform.position.x, 2)
                            + Mathf.Pow(hit[i].transform.position.y - transform.position.y, 2)
                            + Mathf.Pow(hit[i].transform.position.z - transform.position.z, 2)));
                    }
                    for (int i = 0; i < hit.Length; i++)
                    {
                        if (_length[0] < _length[i])
                        {
                            float temp = _length[0];
                            _length[0] = _length[i];
                            _length[i] = temp;

                            Collider tempcol = hit[0];
                            hit[0] = hit[i];
                            hit[i] = tempcol;
                        }
                        if (hit[0].GetComponent<BaseInteraction>())
                        {
                            MatchingObject = hit[0].gameObject.GetComponent<BaseInteraction>();
                        }
                    }
                }
                catch
                {
                    _length = null;
                }
               
            }
            yield return new WaitForSeconds(0.1f);
            if(_length != null)
            {
                try
                {
                    for (int i = 0; i < _length.Count; i++)
                    {
                        if (hit[i].GetComponent<EnemyBase>())
                        {
                            EnemyMatchingObject = hit[i].GetComponent<EnemyBase>();
                            break;
                        }
                        else
                        {
                            EnemyMatchingObject = null;
                        }
                    }
                }
                catch
                {
                    EnemyMatchingObject = null;
                }
            }

            List<float> clear = new List<float>();
            _length = clear;

            
        }


    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(3, 3, 3));
    }

    private void Update()
    {
        if (MatchingObject !=null && Input.GetKeyDown(KeyCode.F))
        {
           MatchingObject.Interaction();
        }
    }

}   
