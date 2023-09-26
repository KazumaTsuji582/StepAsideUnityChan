using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    //carPrefabを入れる
    public GameObject carPrefab;
    //coinPrefabを入れる
    public GameObject coinPrefab;
    //cornPrefabを入れる
    public GameObject conePrefab;
    //unitychanを入れる
    private GameObject unitychan;
    //スタート地点
    private int startPos = 80;
    //ゴール地点
    private int goalPos = 360;
    //アイテムを出すx方向の範囲
    private float posRange = 3.4f;
    //条件分岐が指定条件下で一度だけ実行されるようにするため、bool型の変数を用意
    bool condition = false;

    // Start is called before the first frame update
    void Start()
    {
        //Unityちゃんのオブジェクトを取得
        this.unitychan = GameObject.Find("unitychan");
    }

    // Update is called once per frame
    void Update()
    {
        //unityちゃんが25m進むたびに25~50mくらい先に生成したい 1秒指定で進む距離25m以下
        if (!condition && this.unitychan.gameObject.transform.position.z % 25 <= 1)
        {
            //unityちゃんのz座標を取得
            float uniposz = unitychan.gameObject.transform.position.z;
            //実行回数の確認用ログ
            Debug.Log(uniposz);
            //一度だけ実行させるため真に
            condition = true;
            //一度実行されたのち、指定秒数経過で偽に再設定してフラグをリセットすることで再度分岐可能にする
            StartCoroutine(ResetFlagAfterSeconds(1));   //指定秒数ののちにリセット

            //一定の距離ごとにアイテムを生成
            //for (int i = startPos; i < goalPos; i += 15)
            //for (float i =50f; i < uniposz + 75f; i +=15) //試行錯誤中
            for (float i = uniposz + 25; i < uniposz + 50 && startPos-5 <=i && i < goalPos; i += 15) //25~50mくらいの間にオブジェクト生成の条件
            {
                //どのアイテムを出すのかをランダムに設定
                int num = Random.Range(1, 11);
                if (num <= 2)
                {
                    //コーンをx軸方向に一直線に生成
                    for (float j = -1; j <= 1; j += 0.4f)
                    {
                        GameObject cone = Instantiate(conePrefab);
                        cone.transform.position = new Vector3(4 * j, cone.transform.position.y, i);
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
                            coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, i + offsetZ);
                        }
                        else if (7 <= item && item <= 9)
                        {
                            //車を生成
                            GameObject car = Instantiate(carPrefab);
                            car.transform.position = new Vector3(posRange * j, car.transform.position.y, i + offsetZ);
                        }
                    }
                }
            }
        }

    }
    IEnumerator ResetFlagAfterSeconds(int seconds)  //指定秒数リセット用　条件を満たしたときに複数回実行されることを防ぐため
    {
        yield return new WaitForSeconds(seconds);
        condition = false;
    }
}