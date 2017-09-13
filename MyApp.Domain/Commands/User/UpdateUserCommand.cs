﻿using MyApp.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyApp.Domain.Commands
{
    public class UpdateUserCommand : UserCommand
    {
        public UpdateUserCommand(Guid id, string name, bool active, string email)
        {
            this.Id = id;
            this.Name = name;
            this.Active = active;
            this.Email = email;
        }

        public override bool IsValid()
        {
            var validationResult = new UpdateUserCommandValidation().Validate(this);
            return validationResult.IsValid;
        }
    }
}