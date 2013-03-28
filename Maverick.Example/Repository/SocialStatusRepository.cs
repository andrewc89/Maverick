using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MaverickExample.Models;

namespace MaverickExample.Repository
{
    public class SocialStatusRepository : Maverick.Repository.IRepository<SocialStatus>
    {
        internal static Dictionary<long, SocialStatus> Db = new Dictionary<long, SocialStatus>
        {
            {1, new SocialStatus 
            {
                ID = 1,
                Description = "Too cool for school"
            }}
        };

        public SocialStatusRepository ()
        {
        }

        public SocialStatus Get (long Id)
        {
            return Db[Id];
        }

        public bool Add (SocialStatus Entity)
        {
            Entity.ID = Db.Keys.Max() + 1;
            Db.Add(Entity.ID, Entity);
            return true;
        }

        public bool Update (SocialStatus Entity)
        {
            var DbEntry = Db[Entity.ID];
            DbEntry.Description = Entity.Description;
            return true;
        }

        public bool Delete (long Id)
        {
            return Db.Remove(Id);
        }

        public IEnumerable<SocialStatus> All ()
        {
            return Db.Values;
        }
    }
}