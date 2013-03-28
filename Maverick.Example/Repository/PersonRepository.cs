using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MaverickExample.Models;

namespace MaverickExample.Repository
{
    public class PersonRepository : Maverick.Repository.IRepository<Person>
    {
        internal static Dictionary<long, Person> Db = new Dictionary<long, Person>
        {
            {
                1, 
                new Person 
                {
                    ID = 1,
                    FirstName = "Rebecca",
                    LastName = "Watson",
                    Status = SocialStatusRepository.Db[1]
                }
            },
            {
                2, 
                new Person 
                {
                    ID = 2,
                    FirstName = "Adria",
                    LastName = "Richards",
                    Status = SocialStatusRepository.Db[1]
                }
            }
        };

        public PersonRepository ()
        {
        }

        public Person Get (long Id)
        {
            return Db[Id];
        }

        public bool Add (Person Entity)
        {
            Entity.ID = Db.Keys.Max() + 1;
            Db.Add(Entity.ID, Entity);
            return true;
        }

        public bool Update (Person Entity)
        {
            var DbEntry = Db[Entity.ID];
            DbEntry.FirstName = Entity.FirstName;
            DbEntry.LastName = Entity.LastName;
            DbEntry.Status = Entity.Status;
            return true;
        }

        public bool Delete (long Id)
        {
            return Db.Remove(Id);
        }

        public IEnumerable<Person> All ()
        {
            return Db.Values;
        }
    }
}