using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAndRemember.Models
{
    public class QuestionSet
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public long OwnerUserId { get; set; }
    }
}
