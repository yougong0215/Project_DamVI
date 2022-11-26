using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class SummonObj : MonoBehaviour
{
    [Header("클리어")]
    [SerializeField] UnityEvent Clear = null;

    [Header("위치")]
    [SerializeField] List<std> enemy = new List<std>();


    [Header("적 리스트")]
    [SerializeField] List<EnemyBase> obj = new List<EnemyBase>();



    public int Pase = 0;
    public int ClearPase = 3;

    private void OnTriggerEnter(Collider other)
    {
        obj.Clear();
        StartCoroutine(Sommon());
        StartCoroutine(Som());
        GetComponent<BoxCollider>().enabled = false;

        
    }


    int enemycount = 0;
    IEnumerator Som() 
    {

        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            for (int i = 0; i < obj.Count; i++)
            {
                enemycount = 0;
                if (obj[i] == null)
                {
                    obj.Remove(obj[i]);
                }
                else
                {
                    enemycount++;
                }
            }
            if(obj.Count == 0 && Pase != ClearPase)
            {
                StartCoroutine(Sommon());
            }

        }
    }

    public IEnumerator Sommon()
    {
        if(Pase == ClearPase)
        {
            if(Clear != null)
            {
                Clear?.Invoke();
            }

            Destroy(this);
        }

        for(int i =0; i < enemy[Pase].enemy.Count; i++)
        {
            yield return null;
            EnemyBase ene = Instantiate(enemy[Pase].enemy[i].GameObject);
            obj.Add(ene);
            obj[i].transform.position = enemy[Pase].enemy[i].SummonPos.position;
            ene.ATK = enemy[Pase].enemy[i].ATK;
            ene.MaxHP = enemy[Pase].enemy[i].HP;
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
    public int ATK;
    public int HP;
    public int Score;

}
