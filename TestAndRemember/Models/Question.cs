using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAndRemember.Models
{
    public class Question
    {
        public long ID { get; set; }
        public QuestionSet QuestionSet { get; set; }
        public string Title { get; set; }
        public string ImagePath { get; set; }
        public string Answer { get; set; }
        public string Url { get; set; }
    }
}
