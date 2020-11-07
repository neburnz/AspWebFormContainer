using Microsoft.Configuration.ConfigurationBuilders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;

namespace WebApp
{
    public class SessionStateSectionHandler : SectionHandler<SessionStateSection>
    {
        public override IEnumerator<KeyValuePair<string, object>> GetEnumerator()
        {
            string[] keys = new string[] 
            {
                nameof(ConfigSection.SqlConnectionString)
            };
            foreach (string key in keys)
                yield return new KeyValuePair<string, object>(key, key);
        }

        public override void InsertOrUpdate(string newKey, string newValue, string oldKey = null, object oldItem = null)
        {
            if (newValue != null)
            {
                if (nameof(ConfigSection.SqlConnectionString) == newKey)
                    ConfigSection.SqlConnectionString = newValue;
            }
        }
    }
}