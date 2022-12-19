using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Labb.Infrastructure.ValidationAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class NameTestAttribute : ValidationAttribute, IClientModelValidator
    {
        public NameTestAttribute(string allowedName, string errorMessage)
        {
            AllowedName = allowedName;
            ErrorMessage = errorMessage;
        }

        public string AllowedName { get; private set; }
        public string ErrorMessage { get; private set; }
        //public override bool IsValid(object? value)
        //{
        //    return value != null && value.ToString() == _allowedName;
        //}

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var allowedNameInfo = validationContext.ObjectType.GetRuntimeProperty(AllowedName);
            if(allowedNameInfo == null)
            {
                return ValidationResult.Success;
            }


            //object otherPropertyValue = allowedNameInfo.GetValue(validationContext.ObjectInstance, null);
            //if (otherPropertyValue == null) return ValidationResult.Success;
            //if (((bool)otherPropertyValue) == false) return ValidationResult.Success;

            //if (value == null) return new ValidationResult("value cannot be null");
            //if (value.GetType() != typeof(bool)) throw new InvalidOperationException("can only be used on boolean properties.");
            //if ((bool)value == true)
            //{
            //    return ValidationResult.Success;
            //}

            return new ValidationResult(validationContext.DisplayName);

            //var validationResult = ValidationResult.Success;
            //string name = "";

            //if (value != null)
            //{
            //    name = (string)value;
            //}

            //if (name != "Tom")
            //{
            //    validationResult = new ValidationResult("Wrong namesss" + false);
            //    return validationResult;
            //}

            //return validationResult;
        }

        public void AddValidation(ClientModelValidationContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            context.Attributes.Add("data-val", "true");
            context.Attributes.Add("data-val-other", "#" + AllowedName);
            context.Attributes.Add("data-val-name", ErrorMessage);


            //var errorMessage = context.ModelMetadata.GetDisplayName();
            //MergeAttribute(context.Attributes, "data-val-enforcetrue", errorMessage);
            //MergeAttribute(context.Attributes, "data-val", "true");
            //MergeAttribute(context.Attributes, "data-val-other", "#" + AllowedName);
            //MergeAttribute(context.Attributes, "data-val-name", ErrorMessage);

            //context.Attributes.Add("data-val", "true");
            //context.Attributes.Add("data-val-name", "Wrong name");
            //MergeAttribute(context.Attributes, "data-val", "true");
            //MergeAttribute(context.Attributes, "data-val-name", "Wrong name");
        }

        private bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
        {
            if (attributes.ContainsKey(key))
            {
                return false;
            }
            attributes.Add(key, value);
            return true;
        }
    }
}
