using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class SwipeControl : MonoBehaviour
{

    public GameObject scrollbar;
    float scroll_Pos;
    float[] pos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pos = new float[transform.childCount];
        float distacne  = 1f /(pos.Length-1f);
        for (int i = 0; i < pos.Length; i++)
        {
            pos[i] = distacne * i;
        }
        if (Input.GetMouseButton(0))
        {
            scroll_Pos = scrollbar.GetComponent<Scrollbar>().value;
        }

        /*
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Ended)
            {
                scroll_Pos = scrollbar.GetComponent<Scrollbar>().value;
            }        
        }
        */
        else
        {
            for (int i = 0; i < pos.Length; i++)
            {
                if (scroll_Pos < pos[i] + (distacne / 2) && scroll_Pos > pos[i] - (distacne / 2))
                {
                    scrollbar.GetComponent<Scrollbar>().value = math.lerp(scrollbar.GetComponent<Scrollbar>().value, pos[i], 0.15f);
                }
            }
        }
    }
}
