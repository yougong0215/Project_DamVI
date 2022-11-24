using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteraction : MonoBehaviour
{
    [Header("플레이어 스텟")]
    [SerializeField] int HP = 30;
    [SerializeField] float ATK = 1;
    [SerializeField] int arrmor = 30;
    [SerializeField] bool _superArrmor = false;
    [SerializeField] float MP = 0;

    [Header("가장 가까운 물체")]
    [SerializeField] BaseInteraction MatchingObject = null;
    [SerializeField] EnemyBase EnemyMatchingObject = null;


    [Header("범위네 있는 물체")]
    [SerializeField] Collider[] hit;
    [SerializeField] List<float> _length = new List<float>();
    [SerializeField] List<EnemyBase> _enemy = new List<EnemyBase>();

    [Header("몰?루")]
    [SerializeField] LayerMask layer;

    

    Coroutine hiting;
    bool _nuckback = false;
    Coroutine nuck;

    private void OnEnable()
    {
        //hit[0].transform.position - transform.position < hit[i].transform.position - transform.position

        HP = ShopState.Instance.HPAdd;
        ATK = ShopState.Instance.AttackAdd;
        arrmor = ShopState.Instance.ShiedlAdd;
        MP = ShopState.Instance.MPAdd;

        StartCoroutine(InteractionCheck());
    }

    public void PlusArrmor(int value)
    {
        arrmor += value;
    }
    public void arrmorBlack(int value, Transform enemy)
    {
        if(_nuckback == false)
        {
            arrmor -= value;

            if (nuck != null)
            {
                StopCoroutine(nuck);
            }
            nuck = StartCoroutine(Nakback(enemy));
            arrmor = ShopState.Instance.ShiedlAdd/10;
        }
        

    }

    public void Damaged(int dam)
    {
        HP -= dam;
        GetComponent<PlayerMove>().LookObject.GetComponent<CameraCollision>().shaking(0.1f, 0.2f, 1);
        if(_superArrmor == false)
        {
            arrmor -= dam * Random.Range(1, 11);
        }
    }

    IEnumerator Nakback(Transform Enemy)
    {
        _nuckback = true;
        if (hiting != null)
        {
            StopCoroutine(hiting);
        }
        

        PlayerAttackManager.Instance._ani.SetTrigger("Hit");
        PlayerAttackManager.Instance.PlayerP = PlayerPripoty.hit;
        transform.rotation = Quaternion.LookRotation(Enemy.position);

        GetComponent<Rigidbody>().AddForce((transform.position - Enemy.position).normalized * 5, ForceMode.VelocityChange);

        yield return new WaitForSeconds(0.7f);
        PlayerAttackManager.Instance.PlayerP = PlayerPripoty.none;
        _nuckback = false;
    }

    IEnumerator hitStat()
    {
        PlayerAttackManager.Instance._ani.SetTrigger("Block");
        PlayerAttackManager.Instance.PlayerP = PlayerPripoty.hit;
        yield return new WaitForSeconds(0.7f);
        PlayerAttackManager.Instance.PlayerP = PlayerPripoty.none;

    }

    public float CalcDamage()
    {
        return ATK;
    }
    public Transform DistannsEnemy()
    {
        if (EnemyMatchingObject != null)
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
        while (true)
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
            if (_length != null)
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

    IEnumerator Arrmor()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            
                        arrmor+=5;
        
        }
    }

    private void Update()
    {
        if (MatchingObject != null && Input.GetKeyDown(KeyCode.F))
        {
            MatchingObject.Interaction();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            SceneManager.LoadScene(1);
        }

        if(arrmor >= ShopState.Instance.ShiedlAdd)
        {
            arrmor = ShopState.Instance.ShiedlAdd;
        }


        if (arrmor < 0)
        {
            if (hiting != null)
            {
                StopCoroutine(hiting);
            }
            hiting = StartCoroutine(hitStat());
            arrmor = ShopState.Instance.ShiedlAdd / 4;
        }

    }

}
