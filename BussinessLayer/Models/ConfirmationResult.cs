using System;
using System.Collections.Generic;
using System.Text;

namespace BussinessLayer.Models
{
    public class ConfirmationResult
    {
        public Guid? UserId { get; set; }
        public bool IsSuccessful { get; set; }
    }
}
