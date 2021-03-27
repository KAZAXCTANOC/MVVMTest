//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MVVMTest.Date
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class User : IValidatableObject
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Nullable<int> Type { get; set;} = 1;

        public virtual TypeUser TypeUser { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (Login == "")
            {
                this.Login = null;
            }

            if (Name == "")
            {
                this.Name = null;
            }

            if (Surname == "")
            {
                this.Surname = null;
            }

            if (Password == "")
            {
                this.Password = null;
            }

            if (Login == null)
            {
                errors.Add(new ValidationResult("Поле логина не может быть пустым"));
            }

            if (Password == null)
            {
                errors.Add(new ValidationResult("Поле пароля не может быть пустым"));
            }

            return errors;
        }
    }
}