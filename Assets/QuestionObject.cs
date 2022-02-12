using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    [CreateAssetMenu(fileName = "Question Object", menuName = "Question/Text")]
    public class QuestionObject : ScriptableObject
    {
        [TextArea]public string QuestionContent;
        public List<QuizQuestion.Answer> answers = new List<QuizQuestion.Answer>();
        public int answerIndex;
    }
}