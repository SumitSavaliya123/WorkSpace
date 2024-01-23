using Common.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class ModelValidationException : Exception
    {
        public object? Errors { get; set; }

        public ModelValidationException() : base(MessageConstants.VALIDATION_ERROR)
        { }

        public ModelValidationException(params string[] errorList) : this()
        {
            Errors = errorList.ToList();
        }

        public ModelValidationException(object errors) : this() { Errors = errors; }
    }
}
