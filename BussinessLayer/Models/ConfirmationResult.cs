﻿using System;

namespace BusinessLayer.Models
{
    public class ConfirmationResult
    {
        public Guid? UserId { get; set; }
        public bool IsSuccessful { get; set; }
    }
}
