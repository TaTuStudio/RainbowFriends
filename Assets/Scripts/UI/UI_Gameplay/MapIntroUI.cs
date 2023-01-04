using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class MapIntroUI : MonoBehaviour
{
    [System.Serializable]
    public class MapIntroInfo
    {
        public string mapSceneName = "";

        public List<string> contents = new List<string>();
    }

    public TextMeshProUGUI contentText;

    float speed = 60f;

    public List<MapIntroInfo> mapIntroInfos = new List<MapIntroInfo>();

    string sellectedSceneMap = "";

    public List<string> selectedContents = new List<string>();

    float curDeactiveTime = 0f;

    bool setup = false;

    private void OnEnable()
    {
        setup = true;
    }

    private void Update()
    {
        _Setup();
    }

    void _Setup()
    {
        if (setup)
        {
            setup = false;

            contentText.text = "";

            sellectedSceneMap = "";

            _GetContents();

            PlayTweenText();

            curDeactiveTime = 2f;
        }
    }

    void _GetContents()
    {
        selectedContents.Clear();

        foreach (MapIntroInfo info in mapIntroInfos)
        {
            if (info.mapSceneName == MapManager.instance.selectedMap)
            {
                sellectedSceneMap = info.mapSceneName;

                selectedContents.AddRange(info.contents);

                break;
            }
        }
    }

    private void PlayTweenText()
    {
        if (selectedContents.Count < 1)
        {
            if (sellectedSceneMap == "")
            {
                gameObject.SetActive(false);
            }
            //else if (sellectedSceneMap != "" && curDeactiveTime >= 0f)
            //{
            //    curDeactiveTime -= Time.deltaTime;

            //    if (curDeactiveTime < 0f)
            //    {
            //        gameObject.SetActive(false);
            //    }
            //}

            return;
        }

        string showText = selectedContents[0];

        selectedContents.RemoveAt(0);

        string text = "";

        //Tween customTween = DOTween.To(() => text, x => text = x, showText, showText.Length / speed).OnUpdate(()=> 
        //{

        //    contentText.text = text;

        //}).OnComplete(()=> 
        //{
        //    PlayTweenText();
        //}) ;

        contentText.DOText(showText, 5f, true, ScrambleMode.None);
    }

    public void _CloseButton()
    {
        gameObject.SetActive(false);
    }
}
