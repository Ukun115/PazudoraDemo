using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

public class DropCnt : MonoBehaviour
{
    [SerializeField] Sprite[] sp;
    public int ID1
    {
        get;
        set;
    }
    public int ID2
    {
        get;
        set;
    }
    bool touchFlag;
    Vector2 m;
    RectTransform r, r2;
    public Vector2 P1
    {
        get;
        set;
    }
    public Vector2 P2
    {
        get;
        set;
    }
    Director d;

    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<RectTransform>();
        r2 = transform.parent.GetComponent<RectTransform>();
        d = GameObject.Find("D").GetComponent<Director>();
    }

    // Update is called once per frame
    void Update()
    {
        if (touchFlag)
        {
            var pos = Vector2.zero;
            //マウスのクリックと画面のタッチは同じ！
            m = Input.mousePosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle
                (r2, m, Camera.main, out pos);
            r.localPosition = pos;
        }
        if(P1.x == 0)
        {
            P1 = GetComponent<RectTransform>().position;
            P2 = RectTransformUtility.WorldToScreenPoint(Camera.main, P1);
        }
        else
        {
            if(!touchFlag)
            {
                GetComponent<RectTransform>().position = P1;
            }
        }
    }

    //画像読み込み関数。
    public void Set(int n)
    {
        GetComponent<SpriteRenderer>().sprite = sp[n];
    }
    public void  GetDrop()
    {
        touchFlag = true;
    }
    public void SetDrop()
    {
        touchFlag = false;

        Delete();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (touchFlag)
        {
            if (d.CheckPos(m, collision.gameObject.GetComponent<DropCnt>().P2))
            {
                d.ChangePos(gameObject, collision.gameObject);
            }
        }
    }

    private async void Delete()
    {
        while (true)
        {
            d.DeleteDrop();

            if (d.Check())
            {
                break;
            }
            //0.5秒待つ。
            await Task.Delay(500);
            //上のドロップを下に下げる。(落ちコン)
            d.DownDrop();
            //0.1秒待つ。
            await Task.Delay(100);
            d.ResetDrop();
            //0.1秒待つ。
            await Task.Delay(100);
        }
    }
}