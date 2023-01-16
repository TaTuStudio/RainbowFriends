using UnityEngine;

public class TutManager : MonoBehaviour
{
    bool checkActive;

    public static TutManager instance;

    public TutController[] tutControllers;

    private void OnEnable()
    {
        checkActive = true;
    }

    private void Awake()
    {
        _MakeInstance();
    }

    // Update is called once per frame
    void Update()
    {
        if (checkActive)
        {
            checkActive = false;

            _CheckActive();
        }
    }

    void _MakeInstance()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    public void _CheckActive()
    {
        foreach(TutController tut in tutControllers)
        {
            if(tut.activeNum == PlayerStats.instance.currentTut)
            {
                tut.gameObject.SetActive(true);
            }
            else
            {
                tut.gameObject.SetActive(false);
            }
        }
    }
}
