using System;
using Core.Domain.Exceptions;

namespace Core.Domain.Models {
    public class UserPointClassification {
        public string UserName { get; set; }

        public int UserId { get; set; }

        public int VCPC { get; set; }

        public int VCUPC { get; set; }
        
        public int VCPE { get; set; }

        public int ECPC { get; set; }

        public int ECPE { get; set; }

        public int RE { get; set; }

        public int Total { get; set; }

        public int Position { get; set; }
    }
}