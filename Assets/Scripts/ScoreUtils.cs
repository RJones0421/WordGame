using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ScoreUtils : MonoBehaviour
{
    static string highscore_keyname = "highscore";
    static List<Tuple<string,int>> wordsCollected = new List<Tuple<string,int>>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void clearCollectedWords(){
        wordsCollected.Clear();
    }

    public static int updateAndGetHighsScore(int score){
        try{
            int prevHighscore = PlayerPrefs.GetInt(highscore_keyname);
            if(prevHighscore == null || prevHighscore < score ){
                PlayerPrefs.SetInt(highscore_keyname,score);
                prevHighscore = score;
            }
            return prevHighscore;
        } catch(Exception e){
            Debug.Log("Exception occurred in ScoreUtils class's updateAndGetHighsScore method: "
        +e.Message);
        }
        return 0;
    }

    public static void addWordToCollection(string word,int score){
        try{
            Tuple<string,int> tuple = new Tuple<string,int>(word, score);
            wordsCollected.Add(tuple);
        } catch(Exception e){
            Debug.Log("Exception occurred in ScoreUtils class's addWordToCollection method: "
        +e.Message);
        }
    }

    public static string getTopKwordsCollected(int k){
        string retVal = System.Environment.NewLine + "Top words";
        try{
            wordsCollected.Sort((x, y) => y.Item2.CompareTo(x.Item2));
            for (var i = 0; i < k; i++)
            {
                retVal += System.Environment.NewLine + wordsCollected[i].Item1 + " - " + wordsCollected[i].Item2;
            }
        } catch(Exception e){
            Debug.Log("Exception occurred in ScoreUtils class's getTopKwordsCollected method: "
        +e.Message);
        }
        Debug.Log(retVal);
        return retVal;
    }

    public static void unhideGameObjects(bool toHide){
        try{
            //hide other gameobjects
            GameObject inputFieldGo = GameObject.Find("Player");
            inputFieldGo.SetActive(toHide);
            //inputFieldGo = GameObject.Find("Base Floor");
            //inputFieldGo.SetActive(toHide);
            inputFieldGo = GameObject.Find("Canvas_Gameplay");
            inputFieldGo.SetActive(toHide);
            inputFieldGo = GameObject.Find("Letter Platform(Clone)");
            inputFieldGo.SetActive(toHide);
        } catch(Exception e){
            Debug.Log("Exception occurred in ScoreUtils class's hideGameObjects method: "
        +e.Message);
        }
    }

    public static int GetCollectedWordListSize()
    {
        return wordsCollected.Count;
    }
}
