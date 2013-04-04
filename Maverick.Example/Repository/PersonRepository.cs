using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MaverickExample.Models;
using System.Data.SqlClient;

namespace Maverick.Example.Repository
{
    public class PersonRepository : Maverick.Repository.IRepository<Person>
    {
        private SqlConnection Connection;

        public PersonRepository ()
        {
            Connection = new SqlConnection(@"Server=localhost;Database=Maverick.Example;Trusted_Connection=True;");            
        }

        public Person Get (long ID)
        {
            Connection.Open();
            string Query = string.Format("select * from Person where ID = {0}", ID);
            var Reader = new SqlCommand(Query, Connection).ExecuteReader();
            var Person = new Person();
            while (Reader.Read())
            {
                Person.ID = (long)Reader["ID"];
                Person.FirstName = Reader["FirstName"].ToString();
                Person.LastName = Reader["LastName"].ToString();
            }
            Connection.Close();
            return Person;
        }

        public bool Add (Person Entity)
        {
            Connection.Open();
            string Query = string.Format("insert into Person (FirstName, LastName) output inserted.ID values ('{0}', '{1}')", Entity.FirstName, Entity.LastName);
            Entity.ID = (long)new SqlCommand(Query, Connection).ExecuteScalar();
            bool StatusAdded = true;
            if (Entity.Status != null)
            {
                StatusAdded = AddStatus(Entity);
            }
            Connection.Close();
            return Entity.ID != 0 && StatusAdded;
        }

        private bool AddStatus (Person Entity)
        {
            string Query = string.Format("insert into Person_SocialStatus (Person_ID, SocialStatus_ID) values ('{0}', '{1}')", Entity.ID, Entity.Status.ID);
            int RowsAffected = new SqlCommand(Query, Connection).ExecuteNonQuery();
            return RowsAffected != 0;
        }

        public bool Update (Person Entity)
        {
            Connection.Open();
            string Query = string.Format("update Person set FirstName = '{0}', LastName = '{1}' where ID = {2}", Entity.FirstName, Entity.LastName, Entity.ID);
            int RowsAffected = new SqlCommand(Query, Connection).ExecuteNonQuery();
            Connection.Close();
            return RowsAffected != 0;
        }

        public bool Delete (long ID)
        {
            Connection.Open();
            string Query = string.Format("delete from Person where ID = {0}", ID);
            int RowsAffected = new SqlCommand(Query, Connection).ExecuteNonQuery();
            return RowsAffected != 0;
        }

        public IEnumerable<Person> All ()
        {
            Connection.Open();
            var Persons = new List<Person>();
            string Query = string.Format("select * from Person");
            var Reader = new SqlCommand(Query, Connection).ExecuteReader();
            while (Reader.Read())
            {
                Persons.Add(new Person
                {
                    ID = (long)Reader["ID"],
                    FirstName = Reader["FirstName"].ToString(),
                    LastName = Reader["LastName"].ToString()
                });
            }
            Connection.Close();
            return Persons;
        }
    }
}