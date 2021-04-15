using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebStore.Infrastructure.Filters
{
    public class AddHeaderAttribute : ResultFilterAttribute
    {
        private readonly string _name;
        private readonly string _value;

        public AddHeaderAttribute(string name, string value)
        {
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _value = value;
        }
        public override void OnResultExecuting(ResultExecutingContext context)
        {
            if (_value != null)
                context.HttpContext.Response.Headers.Add(_name, new StringValues(_value));
            base.OnResultExecuting(context);
        }
    }
}
