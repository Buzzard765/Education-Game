using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SceneryCase : ScriptableObject
{
    [System.Serializable]
    public class ImgQuestion
    {        
        [Header("Soal")]
        [TextArea] public string QuestionContent;
        
        public int answerIndex;
    }

    public Image Scenery;

    public List<ImgQuestion> QuestionList = new List<ImgQuestion>();
    public List<Answer> answers = new List<Answer>();
    [System.Serializable]
    public class Answer
    {
        public string content;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
