using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Web.Http;
using static OwinSelfhostSample.Startup;

namespace OwinSelfhostSample
{
    public class ValuesController : ApiController
    {
        // GET api/values 
        public IEnumerable<string> Get()
        {
            return ValuesList.Values;
        }

        // GET api/values/<index of value> 
        public string Get(int id)
        {
            if (!ValuesList.ContainsKey(id))
            {
                return null;
            }
            return ValuesList[id];
        }

        // POST api/values 
        public void Post([FromBody]string value)
        {
            Data obj = JsonConvert.DeserializeObject<Data>(value);
            if (!ValuesList.ContainsValue(obj.Value))
            {
                if (ValuesList.ContainsKey(obj.ID))
                {
                    Random random = new Random();
                    ushort id = 0;
                    do
                    {
                        id = (ushort)random.Next(0, ushort.MaxValue);
                    } while (ValuesList.ContainsKey(id));
                    obj.ID = id;
                }
                
                 
                ValuesList.Add(obj.ID, obj.Value);
            }
        }

        // PUT api/values/5 
        public void Put(int id, [FromBody]string value)
        {
            Data obj = JsonConvert.DeserializeObject<Data>(value);
            if (ValuesList.ContainsKey(id))
            {
                ValuesList[id] = obj.Value;
            }
            else
            {
                Post(value);
            }
        }

        // DELETE api/values/5 
        public void Delete(int id)
        {
            if (ValuesList.ContainsKey(id))
            {
                ValuesList.Remove(id);
            }
        }
    }
}
