using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Lab3.Models
{
    public class QuizModel
    {

        public double Number1 { get; set; }
        public double Number2 { get; set; }
        public string Operation { get; set; }
        public double Solution { get; set; }
        [Range(-100, 100)]
        [Required]
        public int UserAnswer { get; set; }

        public string Answer { get; set; }
        public List<string> AllAnswers;
        public int Count;
        public int CountOfRightAnswers;
        public static QuizModel Instance { get; set; } = new QuizModel(1);
        public QuizModel()
        {

        }
        private QuizModel(int i)
        {

        }
        public void Reset()
        {
            Count = 0;
            CountOfRightAnswers = 0;
            AllAnswers = new List<string>();
        }

        public void Start()
        {
            Random rand = new Random();
            Number1 = rand.Next(0, 10);
            Number2 = rand.Next(1, 10);
            Operation = rand.Next(3) switch
            {
                0 => "+",
                1 => "-",
                2 => "*",
                _ => throw new Exception("Random number is too big")
            };
            Count++;

        }




        public void Questions()
        {

            if (Operation == "+")
            {
                Solution = Number1 + Number2;
                if (Solution == UserAnswer)
                    CountOfRightAnswers++;


                AllAnswers.Add("" + Number1 + " + " + Number2 + " = " + UserAnswer);
            }
            else if (Operation == "-")
            {
                Solution = Number1 - Number2;
                if (Solution == UserAnswer)
                    CountOfRightAnswers++;

                AllAnswers.Add("" + Number1 + " - " + Number2 + " = " + UserAnswer);
            }
            else if (Operation == "*")
            {
                Solution = Number1 * Number2;
                if (Solution == UserAnswer)
                    CountOfRightAnswers++;

                AllAnswers.Add("" + Number1 + " * " + Number2 + " = " + UserAnswer);
            }
        }

        public class QuizOperationAttribute : ValidationAttribute
        {

            string[] _names;

            public QuizOperationAttribute(string[] names)
            {
                _names = names;
            }
            public override bool IsValid(object value)
            {
                if (value != null && _names.Contains(value.ToString()))
                    return true;

                return false;
            }
        }

    }
}
