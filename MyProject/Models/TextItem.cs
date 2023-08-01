using System;

namespace MyProject.Models
{
    [Serializable]
    public class TextItem : IItem
    {
        private string correctAnswer;
        public string CorrectAnswer
        {
            get => correctAnswer;
            set => correctAnswer = value;
        }

        public override bool Equals(object obj)
        {
            if (obj is TextItem objectType)
            {
                return this.CorrectAnswer == objectType.CorrectAnswer;
            }
            return false;
        }
    }
}
