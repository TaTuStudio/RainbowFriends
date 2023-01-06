using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutController : MonoBehaviour
{
    public int activeNum = 0;

    int currentStep = 0;

    public GameObject[] steps;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        currentStep = 0;

        _NextStep();
    }

    public void _NextStep()
    {
        int count = 0;

        for(int i=0; i< steps.Length; i++)
        {
            if(i == currentStep)
            {
                steps[i].gameObject.SetActive(true);

                count++;
            }
            else
            {
                steps[i].gameObject.SetActive(false);
            }
        }

        currentStep++;

        if(count == 0)
        {
            PlayerStats.instance.currentTut++;

            PlayerStats.instance.save = true;

            gameObject.SetActive(false);
        }
    }
}
