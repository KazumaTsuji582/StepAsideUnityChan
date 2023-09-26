using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGenerator : MonoBehaviour
{
    //carPrefab������
    public GameObject carPrefab;
    //coinPrefab������
    public GameObject coinPrefab;
    //cornPrefab������
    public GameObject conePrefab;
    //unitychan������
    private GameObject unitychan;
    //�X�^�[�g�n�_
    private int startPos = 80;
    //�S�[���n�_
    private int goalPos = 360;
    //�A�C�e�����o��x�����͈̔�
    private float posRange = 3.4f;
    //�������򂪎w��������ň�x�������s�����悤�ɂ��邽�߁Abool�^�̕ϐ���p��
    bool condition = false;

    // Start is called before the first frame update
    void Start()
    {
        //Unity�����̃I�u�W�F�N�g���擾
        this.unitychan = GameObject.Find("unitychan");
    }

    // Update is called once per frame
    void Update()
    {
        //unity�����25m�i�ނ��т�25~50m���炢��ɐ��������� 1�b�w��Ői�ދ���25m�ȉ�
        if (!condition && this.unitychan.gameObject.transform.position.z % 25 <= 1)
        {
            //unity������z���W���擾
            float uniposz = unitychan.gameObject.transform.position.z;
            //���s�񐔂̊m�F�p���O
            Debug.Log(uniposz);
            //��x�������s�����邽�ߐ^��
            condition = true;
            //��x���s���ꂽ�̂��A�w��b���o�߂ŋU�ɍĐݒ肵�ăt���O�����Z�b�g���邱�Ƃōēx����\�ɂ���
            StartCoroutine(ResetFlagAfterSeconds(1));   //�w��b���̂̂��Ƀ��Z�b�g

            //���̋������ƂɃA�C�e���𐶐�
            //for (int i = startPos; i < goalPos; i += 15)
            //for (float i =50f; i < uniposz + 75f; i +=15) //���s���뒆
            for (float i = uniposz + 25; i < uniposz + 50 && startPos-5 <=i && i < goalPos; i += 15) //25~50m���炢�̊ԂɃI�u�W�F�N�g�����̏���
            {
                //�ǂ̃A�C�e�����o���̂��������_���ɐݒ�
                int num = Random.Range(1, 11);
                if (num <= 2)
                {
                    //�R�[����x�������Ɉ꒼���ɐ���
                    for (float j = -1; j <= 1; j += 0.4f)
                    {
                        GameObject cone = Instantiate(conePrefab);
                        cone.transform.position = new Vector3(4 * j, cone.transform.position.y, i);
                    }
                }
                else
                {

                    //���[�����ƂɃA�C�e���𐶐�
                    for (int j = -1; j <= 1; j++)
                    {
                        //�A�C�e���̎�ނ����߂�
                        int item = Random.Range(1, 11);
                        //�A�C�e����u��Z���W�̃I�t�Z�b�g�������_���ɐݒ�
                        int offsetZ = Random.Range(-5, 6);
                        //60%�R�C���z�u:30%�Ԕz�u:10%�����Ȃ�
                        if (1 <= item && item <= 6)
                        {
                            //�R�C���𐶐�
                            GameObject coin = Instantiate(coinPrefab);
                            coin.transform.position = new Vector3(posRange * j, coin.transform.position.y, i + offsetZ);
                        }
                        else if (7 <= item && item <= 9)
                        {
                            //�Ԃ𐶐�
                            GameObject car = Instantiate(carPrefab);
                            car.transform.position = new Vector3(posRange * j, car.transform.position.y, i + offsetZ);
                        }
                    }
                }
            }
        }

    }
    IEnumerator ResetFlagAfterSeconds(int seconds)  //�w��b�����Z�b�g�p�@�����𖞂������Ƃ��ɕ�������s����邱�Ƃ�h������
    {
        yield return new WaitForSeconds(seconds);
        condition = false;
    }
}