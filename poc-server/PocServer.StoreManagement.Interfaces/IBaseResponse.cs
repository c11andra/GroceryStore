using System;
using Newtonsoft.Json;

namespace PocServer.StoreManagement.Interfaces
{
    public interface IBaseResponse
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        string Error { get;}
    }
}
