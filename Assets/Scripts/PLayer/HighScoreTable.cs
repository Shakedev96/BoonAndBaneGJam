using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTable : MonoBehaviour
{

    private Transform entryContainer;
    private Transform entryTemplate;
    private List<HighScoreEntry> highScoreEntriesList;
    private List<Transform> highSoreEntryTransformList;
    

    private void Awake()
    {
        entryContainer = transform.Find("HighScoreContainer");
        entryTemplate = entryContainer.Find("HighScoreEntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        /*highScoreEntriesList = new List<HighScoreEntry>()
        {
            new HighScoreEntry {score = 6421, name = "AAAA"},
            new HighScoreEntry {score = 3000, name = "BBB"},
            new HighScoreEntry {score = 4231, name = "CaA"},
            new HighScoreEntry {score = 4234, name = "baA"},
            new HighScoreEntry {score = 5223, name = "mA"}
        };*/

        string jsonString = PlayerPrefs.GetString("HighScoreTable");
        HighScore highscores = JsonUtility.FromJson<HighScore>(jsonString);
        //sort entry by score
        for(int i = 0; i < highScoreEntriesList.Count; i++)
        {
            for(int j = 0; j < highScoreEntriesList.Count; j++)
            {
                if(highScoreEntriesList[j].score > highScoreEntriesList[i].score)
                {
                    //swap
                    HighScoreEntry tmp = highScoreEntriesList[i];
                    highScoreEntriesList[i] = highScoreEntriesList[j];
                    highScoreEntriesList[j] = tmp;
                }
            }
        }

        highSoreEntryTransformList = new List<Transform>();
        foreach(HighScoreEntry highScoreEntry in highScoreEntriesList)
        {
            CreateHighScoreEntryTransform(highScoreEntry, entryContainer, highSoreEntryTransformList);
        }

        /* HighScore highScore = new HighScore {highScoreEntriesList = highScoreEntriesList};
        string json = JsonUtility.ToJson(highScore);
        PlayerPrefs.SetString("HighScoreTable", json);
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetString("HighScoreTable")); */
        
    }
    
    private void CreateHighScoreEntryTransform(HighScoreEntry highScoreEntry, Transform container, List<Transform> transformlist)
    {       
            float templateHeight = 50f;
            Transform entryTransform = Instantiate(entryTemplate, container);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformlist.Count);
            entryTransform.gameObject.SetActive(true);

            int rank = transformlist.Count + 1;
            string rankString;
            switch(rank)
            {
                default: rankString = rank + "TH"; break;

                case 1: rankString = "1st" ; break;
                case 2: rankString = "2nd" ; break;
                case 3: rankString = "3rd" ; break;
            }

            entryTransform.Find("PositionText").GetComponent<TextMeshProUGUI>().text = rankString;

            int score = highScoreEntry.score;

            entryTransform.Find("TimeText").GetComponent<TextMeshProUGUI>().text = score.ToString();

            string name = highScoreEntry.name;
            entryTransform.Find("NameText").GetComponent<TextMeshProUGUI>().text = name;

            transformlist.Add(entryTransform);
    }

    private class HighScore
    {
        public List<HighScoreEntry> highScoreEntriesList;
    }
    /* represents a single highScore */

    [System.Serializable]
    private class HighScoreEntry
    {
        public int score;
        public string name;
    }
}
