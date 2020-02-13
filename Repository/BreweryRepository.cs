using System;
using System.Collections.Generic;
using Couchbase.Core;
using Couchbase.Configuration.Client;
using Couchbase.Authentication;
using Couchbase.Api.Models;
using Couchbase;


namespace Couchbase.Api.Repository
{
    public class BreweryRepository
    {
        private readonly IBucket _bucket;

        public BreweryRepository(ICouchbaserServerConfiguration couchbaseConfig)
        {
            ClusterHelper.Initialize(new ClientConfiguration
            {
                Servers = new List<Uri> { new Uri(couchbaseConfig.Host) }
            }, new PasswordAuthenticator(couchbaseConfig.Username, couchbaseConfig.Password));

            _bucket = ClusterHelper.GetBucket(couchbaseConfig.Bucket);
        }

        #region Read(s) 


        public Brewery GetBrewery(string id)
        {
            var result = _bucket.Get<Brewery>(id);
            return result.Value;
        }      

        #endregion

        #region Create(s)
        public Brewery AddBrewery(string id, Brewery brewery)
        {
            var result = _bucket.Insert(id, brewery);

            if (result.Success)
            {
                return _bucket.Get<Brewery>(result.Id).Value;
            }

            return null;
        }       

        #endregion

        #region Update(s)
        public Brewery EditBrewery(string id, Brewery brewery)
        {
            var result = _bucket.Upsert(id, brewery);

            if (result.Success)
            {
                return _bucket.Get<Brewery>(result.Id).Value;
            }

            return null;
        }

      

        #endregion

        #region Delete(s)
        public bool DeleteBrewery(string id)
        {
            var result = _bucket.Remove(id);

            return result.Success;
        }   

        #endregion
    }
}
