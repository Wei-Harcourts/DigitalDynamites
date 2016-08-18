using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Harcourts.eOpen.Web.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace Harcourts.eOpen.Web
{
    public class VisitorRepository
    {
        private readonly string _databasePath;
        private readonly JsonSerializerSettings _jsonSerializerSettings;
        private static readonly object LockSmith = new object(); 

        public VisitorRepository(string appDataPath)
        {
            _databasePath = Path.Combine(appDataPath, "mongodb.txt");
            _jsonSerializerSettings =
                new JsonSerializerSettings
                {
                    ContractResolver =
                        new CamelCasePropertyNamesContractResolver(),
                    Formatting = Formatting.Indented
                };
            _jsonSerializerSettings.Converters.Add(new BinaryConverter());
            _jsonSerializerSettings.Converters.Add(new StringConverter());
        }

        protected Dictionary<VisitorKey, Visitor> Read()
        {
            var jsonString = File.ReadAllText(_databasePath, Encoding.UTF8);
            var objArray = (JArray) JsonConvert.DeserializeObject(jsonString, _jsonSerializerSettings);

            var visitors = objArray.Select(VisitorHelper.Create)
                .ToList()
                .ToDictionary(x => new VisitorKey {Name = x.Name, Email = x.EmailAddress}, x => x);
            return visitors;
        }

        public IEnumerable<Visitor> All()
        {
            var visitors = Read();
            return visitors.Values.ToList().AsReadOnly();
        }

        public void Create(Visitor obj)
        {
            lock (LockSmith)
            {
                var visitors = Read();
                var objKey = new VisitorKey {Name = obj.Name, Email = obj.EmailAddress};
                List<Visitor> newList;
                if (visitors.ContainsKey(objKey))
                {
                    visitors[objKey] = obj;
                    newList = new List<Visitor>(visitors.Values);
                }
                else
                {
                    newList = new List<Visitor>(visitors.Values) {obj};
                }
                var jsonString = JsonConvert.SerializeObject(newList, _jsonSerializerSettings);
                File.WriteAllText(_databasePath, jsonString, Encoding.UTF8);
            }
        }

        protected struct VisitorKey
        {
            public string Name { get; set; }
            public string Email { get; set; }
        }
    }
}