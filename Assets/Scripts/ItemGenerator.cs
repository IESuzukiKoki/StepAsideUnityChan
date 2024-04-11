using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    //carPrefabを入れる
    public GameObject carPrefab;
    //coinPrefabを入れる
    public GameObject coinPrefab;
    //conePrefabを入れる
    public GameObject conePrefab;
    //スタート地点
    private int startPos = 30;
    //ゴール地点
    private int goalPos = 360;
    //アイテムを出すx方向の範囲
    private float posRange = 3.4f;

    private GameOBJ _gameOBJ = new GameOBJ();

    [SerializeField] private GameObject _unityChan;
    private Transform _unityChanTransform;
    private UnityChanController _unityChanController;
    private int _length = 1;
    //オブジェクト管理用クラス
    public class GameOBJ
    {
        public List<CornOBJ> _cornOBJList = new List<CornOBJ>();
        public List<CoinOBJ> _coinOBJList = new List<CoinOBJ>();
        public List<CarOBJ> _carOBJList = new List<CarOBJ>();
    }

    public class CoinOBJ
    {
        public CoinController _coinController;
        public GameObject _coinObj;

        public CoinOBJ(GameObject gameObject, CoinController coinController)
        {
            _coinObj = gameObject;
            _coinController = coinController;
        }
    }

    public class CornOBJ
    {
        public CornController _cornController;
        public GameObject _cornObj;

        public CornOBJ(GameObject gameObject, CornController cornController)
        {
            _cornObj = gameObject;
            _cornController = cornController;

        }

    }

    public class CarOBJ
    {
        public CarController _carController;
        public GameObject _carObj;

        public CarOBJ(GameObject gameObject, CarController carController)
        {
            _carObj = gameObject;
            _carController = carController;
        }
    }
    private void Awake()
    {
        _unityChanController = _unityChan.GetComponent<UnityChanController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _unityChanTransform = _unityChan.transform;

        //一定の距離ごとにアイテムを生成
        for (int i = startPos; i < startPos+50; i += 15)
        {
            //どのアイテムを出すのかをランダムに設定
            int num = Random.Range(1, 11);
            if (num <= 2)
            {
                //コーンをx軸方向に一直線に生成
                for (float j = -1; j <= 1; j += 0.4f)
                {
                    GameObject corn = Instantiate(conePrefab);
                    CornController cornController = corn.transform.GetComponent<CornController>();

                    _gameOBJ._cornOBJList.Add(new CornOBJ(corn, cornController));
                    _gameOBJ._cornOBJList[_gameOBJ._cornOBJList.Count - 1]._cornObj.transform.position = new Vector3(4 * j, _gameOBJ._cornOBJList[_gameOBJ._cornOBJList.Count - 1]._cornObj.transform.position.y, i);
                    _gameOBJ._cornOBJList[_gameOBJ._cornOBJList.Count - 1]._cornController.IsLive = true;
                }
            }
            else
            {

                //レーンごとにアイテムを生成
                for (int j = -1; j <= 1; j++)
                {
                    //アイテムの種類を決める
                    int item = Random.Range(1, 11);
                    //アイテムを置くZ座標のオフセットをランダムに設定
                    int offsetZ = Random.Range(-5, 6);
                    //60%コイン配置:30%車配置:10%何もなし
                    if (1 <= item && item <= 6)
                    {
                        //コインを生成
                        GameObject coin = Instantiate(coinPrefab);
                        CoinController coinController = coin.GetComponent<CoinController>();

                        _gameOBJ._coinOBJList.Add(new CoinOBJ(coin, coinController));
                        _gameOBJ._coinOBJList[_gameOBJ._coinOBJList.Count - 1]._coinController.IsLive = true;
                        _gameOBJ._coinOBJList[_gameOBJ._coinOBJList.Count - 1]._coinObj.transform.position = new Vector3(posRange * j, _gameOBJ._coinOBJList[_gameOBJ._coinOBJList.Count - 1]._coinObj.transform.position.y, i + offsetZ);

                    }
                    else if (7 <= item && item <= 9)
                    {
                        //車を生成
                        GameObject car = Instantiate(carPrefab);
                        CarController carController = car.GetComponent<CarController>();

                        _gameOBJ._carOBJList.Add(new CarOBJ(car, carController));
                        _gameOBJ._carOBJList[_gameOBJ._carOBJList.Count - 1]._carController.IsLive = true;
                        _gameOBJ._carOBJList[_gameOBJ._carOBJList.Count - 1]._carObj.transform.position = new Vector3(posRange * j, _gameOBJ._carOBJList[_gameOBJ._carOBJList.Count - 1]._carObj.transform.position.y, i + offsetZ);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {


        if(_unityChanTransform.position.z>50*_length)
        {
            _length++;

            //一定の距離ごとにアイテムを生成
            for (int i = (int)_unityChanTransform.position.z+50; i < (int)_unityChanTransform.position.z + 100; i += 15)
            {

                //どのアイテムを出すのかをランダムに設定
                int num = Random.Range(1, 11);
                if (num <= 2)
                {
                    //コーンをx軸方向に一直線に生成
                    for (float j = -1; j <= 1; j += 0.4f)
                    {
                        GameObject corn = Instantiate(conePrefab);
                        CornController cornController = corn.transform.GetComponent<CornController>();

                        _gameOBJ._cornOBJList.Add(new CornOBJ(corn, cornController));
                        _gameOBJ._cornOBJList[_gameOBJ._cornOBJList.Count - 1]._cornObj.transform.position = new Vector3(4 * j, _gameOBJ._cornOBJList[_gameOBJ._cornOBJList.Count - 1]._cornObj.transform.position.y, i);
                        _gameOBJ._cornOBJList[_gameOBJ._cornOBJList.Count - 1]._cornController.IsLive = true;
                    }
                }
                else
                {

                    //レーンごとにアイテムを生成
                    for (int j = -1; j <= 1; j++)
                    {
                        //アイテムの種類を決める
                        int item = Random.Range(1, 11);
                        //アイテムを置くZ座標のオフセットをランダムに設定
                        int offsetZ = Random.Range(-5, 6);
                        //60%コイン配置:30%車配置:10%何もなし
                        if (1 <= item && item <= 6)
                        {
                            //コインを生成
                            GameObject coin = Instantiate(coinPrefab);
                            CoinController coinController = coin.GetComponent<CoinController>();

                            _gameOBJ._coinOBJList.Add(new CoinOBJ(coin, coinController));
                            _gameOBJ._coinOBJList[_gameOBJ._coinOBJList.Count - 1]._coinController.IsLive = true;
                            _gameOBJ._coinOBJList[_gameOBJ._coinOBJList.Count - 1]._coinObj.transform.position = new Vector3(posRange * j, _gameOBJ._coinOBJList[_gameOBJ._coinOBJList.Count - 1]._coinObj.transform.position.y, i + offsetZ);

                        }
                        else if (7 <= item && item <= 9)
                        {
                            //車を生成
                            GameObject car = Instantiate(carPrefab);
                            CarController carController = car.GetComponent<CarController>();

                            _gameOBJ._carOBJList.Add(new CarOBJ(car, carController));
                            _gameOBJ._carOBJList[_gameOBJ._carOBJList.Count - 1]._carController.IsLive = true;
                            _gameOBJ._carOBJList[_gameOBJ._carOBJList.Count - 1]._carObj.transform.position = new Vector3(posRange * j, _gameOBJ._carOBJList[_gameOBJ._carOBJList.Count - 1]._carObj.transform.position.y, i + offsetZ);
                        }
                    }
                }
            }
        }


        if(_unityChanController._DestroyCoin!=null)
        {
            for(int i= _gameOBJ._coinOBJList.Count - 1; i >= 0; i--)
            {
                GameObject coin= _gameOBJ._coinOBJList[i]._coinObj;
                if(_unityChanController._DestroyCoin==coin)
                {
                    _gameOBJ._coinOBJList.RemoveAt(i);
                    Destroy(coin);
                }
            }
        }

      for(int i=_gameOBJ._coinOBJList.Count-1;i>=0;i--)
        {
            if(_gameOBJ._coinOBJList[i]._coinObj.transform.position.z<_unityChanTransform.position.z-10.0f)
            {
                GameObject coin = _gameOBJ._coinOBJList[i]._coinObj;
                _gameOBJ._coinOBJList.RemoveAt(i);
                Destroy(coin);
                
            }
        }

      for(int i=_gameOBJ._cornOBJList.Count-1;i>=0;i--)
        {
            if (_gameOBJ._cornOBJList[i]._cornObj.transform.position.z < _unityChanTransform.position.z - 10.0f)
            {
                GameObject corn = _gameOBJ._cornOBJList[i]._cornObj;
                _gameOBJ._cornOBJList.RemoveAt(i);
                Destroy(corn);
            }
        }

      for(int i=_gameOBJ._carOBJList.Count-1;i>=0;i--)
        {
            if (_gameOBJ._carOBJList[i]._carObj.transform.position.z < _unityChanTransform.position.z - 10.0f)
            {
                GameObject car = _gameOBJ._carOBJList[i]._carObj;
                _gameOBJ._carOBJList.RemoveAt(i);
                Destroy(car);
            }
        }
    }
}
