using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletReload : MonoBehaviour
{
    [SerializeField] public int _bulletcount;
    [SerializeField] Image Bullet;

    Coroutine bul;

    public void UseingShoot()
    {
        _bulletcount--;
        _bulletcount = Mathf.Clamp(_bulletcount, 0, ShopState.Instance.BulletAdd);
        if (bul != null)
        {
            StopCoroutine(bul);
        }
        bul = StartCoroutine(GageUp());
    }

    public int CanUse()
    {

        if (_bulletcount > 0)
        {
            return 1;
        }
        else if(_bulletcount == 0)
        {
            return 0;
        }
        else
        {
            return -1;
        }
    }
    

    [SerializeField] List<Image> _obj = new List<Image>();
    void Start()
    {
        _bulletcount = ShopState.Instance.BulletAdd;
        for(int i =0; i < _bulletcount; i++)
        {
            _obj.Add(Instantiate(Bullet, transform));
            if (gameObject.name == "Reload")
            {
                _obj[i].color = Color.red;
                //_obj[i].GetComponent<Sprite>().layer
            }
        }
        StartCoroutine(Use());
    }
    
    IEnumerator GageUp()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            _bulletcount++;
            if(_bulletcount >= ShopState.Instance.BulletAdd)
            {
                break;
            }
        }
    }

    IEnumerator Use()
    {
        while (true)
        {
            yield return null;
            _bulletcount = Mathf.Clamp(_bulletcount, 0, ShopState.Instance.BulletAdd);

            for (int i = 0; i < ShopState.Instance.BulletAdd; i++)
            {
                _obj[i].color = Color.black;
            }

            if (gameObject.name == "Reload")
            {
                //yield return null;
                for (int i = 0; i < _bulletcount; i++)
                {
                    _obj[i].color = Color.red;
                }
            }
        }
       
    }
}
