using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    private bool _isLive;

    public bool IsLive
    {
        get { return _isLive; }
        set { _isLive = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        //回転を開始する角度を設定
        transform.Rotate(0, Random.Range(0, 360), 0);
        _isLive = false;

    }

    // Update is called once per frame
    void Update()
    {
        //回転
        transform.Rotate(0, 3, 0);
    }

}
