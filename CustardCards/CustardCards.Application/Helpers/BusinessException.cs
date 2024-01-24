using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustardCards.Application.Helpers
{
    public class BusinessException : Exception
    {
        public BusinessException(int statusCode, object? value = null) =>
        (StatusCode, Value) = (statusCode, value);

        public int StatusCode { get; }

        public object? Value { get; }
    }
}
