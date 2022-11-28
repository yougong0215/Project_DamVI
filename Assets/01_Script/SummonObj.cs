using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class SummonObj : MonoBehaviour
{
    [Header("Ŭ����")]
    [SerializeField] StageClear Clear = null;

    [Header("��ġ")]
    [SerializeField] List<std> enemy = new List<std>();


    [Header("�� ����Ʈ")]
    [SerializeField] List<EnemyBase> obj = new List<EnemyBase>();

    [Header("��������")]
    [SerializeField] List<BoxCollider> pos = new List<BoxCollider>();

    private void OnEnable()
    {
        ClearPase = enemy.Count;
    }

    public int Pase = 0;
    int ClearPase = 2;

    private void OnTriggerEnter(Collider other)
    {
        obj.Clear();
        StartCoroutine(Sommon());
        StartCoroutine(Som());
        GetComponent<BoxCollider>().enabled = false;

        
    }
    
    IEnumerator Som() 
    {
        for(int i =0; i < pos.Count; i++)
        {
            yield return null;
            pos[i].gameObject.SetActive(true);
        }


        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            for (int i = 0; i < obj.Count; i++)
            {
                if (obj[i] == null)
                {
                    obj.Remove(obj[i]);
                }
            }
            if(obj.Count == 0)
            {
                StartCoroutine(Sommon());
                if (Pase == ClearPase)
                {

                    break;
                }
            }

        }
    }

    public IEnumerator Sommon()
    {
        if(Pase == ClearPase)
        {
            for (int i = 0; i < pos.Count; i++)
            {
                yield return null;
                pos[i].gameObject.SetActive(false);
            }
            if (Clear != null)
              Clear.OnClear();
            
            
           Destroy(this);
        }
        else
        {
            for (int i = 0; i < enemy[Pase].enemy.Count; i++)
            {
                yield return null;
                EnemyBase ene = Instantiate(enemy[Pase].enemy[i].GameObject);
                obj.Add(ene);
                obj[i].transform.position = enemy[Pase].enemy[i].SummonPos.position;
                ene.ATK = enemy[Pase].enemy[i].ATK;
                ene.MaxHP = enemy[Pase].enemy[i].HP;
                ene.HP = enemy[Pase].enemy[i].HP;
                ene.Score = enemy[Pase].enemy[i].Score;
                ene.MinScore = enemy[Pase].enemy[i].MinScore;
                if(enemy[Pase].enemy[i].name != "Enemy")
                {
                    ene.nameing = enemy[Pase].enemy[i].name;
                }
            }
        }
      

        Pase++;
    }
}

[System.Serializable]
public struct std
{
    public List<Enemys> enemy;
}

[System.Serializable]
public class Enemys
{

    public EnemyBase GameObject = null;
    public Transform SummonPos;
    public string name = "Enemy";
    public int ATK;
    public int HP;
    public int Score;
    public int MinScore;

}
