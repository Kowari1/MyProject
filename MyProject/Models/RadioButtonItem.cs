﻿using System;

namespace MyProject.Models
{
    [Serializable]
    public class RadioButtonItem : IItem
    {
        private string text;
        public string Text
        {
            get => text;
            set => text = value;
        }

        private bool correctAnswer = false;
        public bool CorrectAnswer
        {
            get => correctAnswer;
            set => correctAnswer = value;
        }

        public override bool Equals(object obj)
        {
            if (obj is RadioButtonItem objectType)
            {
                return this.CorrectAnswer == objectType.CorrectAnswer;
            }
            return false;
        }
    }
}
