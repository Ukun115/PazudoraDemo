using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generator : MonoBehaviour
{
    //�݌v�}�����锠
    public GameObject D, L;

    // Start is called before the first frame update
    void Start()
    {
        //�u���b�N�v30�����B

        //�c5�����B
        for(int i= 0; i < 5; i++)
        {
            GameObject l = Instantiate(L) as GameObject;
            l.transform.SetParent(transform);
            l.transform.localScale = Vector3.one;
            //��6�����B
            for(int j = 0; j < 6; j++)
            {
                GameObject d = Instantiate(D) as GameObject;
                d.transform.SetParent(l.transform);
                //type��1�`6�������_���ɑ���B
                int type = Random.Range(0, 6);
                //�摜�����֐����ĂԁB
                d.GetComponent<DropCnt>().Set(type);
                d.GetComponent<DropCnt>().ID1 = i;
                d.GetComponent<DropCnt>().ID2 = j;
                GameObject.Find("D").GetComponent<Director>().Obj[i, j] = d;
                GameObject.Find("D").GetComponent<Director>().Field[i, j] = type;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}