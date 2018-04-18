using System;
using Core.Domain.Enums;
using Core.Domain.Exceptions;

namespace Core.Domain.Models {
    public class UserPoint {
        public int UserId { get; set; }

        public int OficialGameId { get; set; }

        public PointTypeEnum PointType { get; set; }
    }
}