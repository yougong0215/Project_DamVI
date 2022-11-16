using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [Header("�÷��̾� ����")]
    [SerializeField] int HP = 30;
    [SerializeField] float ATK = 1;

    [Header("���� ����� ��ü")]
    [SerializeField] BaseInteraction MatchingObject = null;


    [Header("������ �ִ� ��ü")]
    [SerializeField] Collider[] hit;
    [SerializeField] List<float> _length = new List<float>();

    private void OnEnable()
    {
        //hit[0].transform.position - transform.position < hit[i].transform.position - transform.position
        StartCoroutine(InteractionCheck());
    }

    public void Damaged(int dam)
    {
        HP -= dam;
    }

    public float CalcDamage()
    {
        return ATK;
    }



    IEnumerator InteractionCheck()
    {
        while(true)
        {
            hit = Physics.OverlapBox(transform.position, new Vector3(4, 4, 4), Quaternion.identity, 1 << (LayerMask.NameToLayer("InterectionObj")));
            MatchingObject = null;
            if (hit != null)
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
                    if(_length[0] < _length[i])
                    {
                        float temp = _length[0];
                        _length[0] = _length[i];
                        _length[i] = temp;

                        Collider tempcol = hit[0];
                        hit[0] = hit[i];
                        hit[i] = tempcol;
                    }
                    MatchingObject = hit[0].gameObject.GetComponent<BaseInteraction>();
                }
            }
            yield return new WaitForSeconds(0.1f);
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
