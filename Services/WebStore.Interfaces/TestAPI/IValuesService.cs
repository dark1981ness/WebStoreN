using System;
using System.Collections.Generic;
using System.Net;

namespace WebStore.Interfaces.TestAPI
{
    public interface IValuesService
    {
        IEnumerable<string> Get();

        string Get(int id);

        Uri Create(string value);

        HttpStatusCode Edit(int id, string value);

        HttpStatusCode Remove(int id);
    }
}
