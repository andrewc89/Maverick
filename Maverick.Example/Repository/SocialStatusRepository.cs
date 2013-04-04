using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MaverickExample.Models;
using System.Data.SqlClient;

namespace Maverick.Example.Repository
{
    public class SocialStatusRepository : Maverick.Repository.IRepository<SocialStatus>
    {
        private SqlConnection Connection;

        public SocialStatusRepository ()
        {
            Connection = new SqlConnection(@"Server=localhost;Database=Maverick.Example;Trusted_Connection=True;");            
        }

        public SocialStatus Get (long ID)
        {
            Connection.Open();
            string Query = string.Format("select * from SocialStatus where ID = {0}", ID);
            var Reader = new SqlCommand(Query, Connection).ExecuteReader();
            var SocialStatus = new SocialStatus();
            while (Reader.Read())
            {
                SocialStatus.ID = (long)Reader["ID"];
                SocialStatus.Description = Reader["Description"].ToString();
            }
            Connection.Close();
            return SocialStatus;
        }

        public bool Add (SocialStatus Entity)
        {
            Connection.Open();
            string Query = string.Format("insert into SocialStatus (Description) output inserted.ID values ('{0}')", Entity.Description);
            Entity.ID = (long)new SqlCommand(Query, Connection).ExecuteScalar();
            bool StatusAdded = true;
            if (Entity.Members != null)
            {
                StatusAdded = AddMembers(Entity);
            }
            Connection.Close();
            return Entity.ID != 0 && StatusAdded;
        }

        private bool AddMembers (SocialStatus Entity)
        {
            int RowsAffected = 0;
            foreach (var Member in Entity.Members)
            {
                string Query = string.Format("insert into Person_SocialStatus (SocialStatus_ID, Person_ID) values ('{0}', '{1}')", Entity.ID, Member.ID);
                RowsAffected += new SqlCommand(Query, Connection).ExecuteNonQuery();
            }
            return RowsAffected == Entity.Members.Count;
        }

        public bool Update (SocialStatus Entity)
        {
            Connection.Open();
            string Query = string.Format("update SocialStatus set Description = '{0}' where ID = {1}", Entity.Description, Entity.ID);
            int RowsAffected = new SqlCommand(Query, Connection).ExecuteNonQuery();
            Connection.Close();
            return RowsAffected != 0;
        }

        public bool Delete (long ID)
        {
            Connection.Open();
            string Query = string.Format("delete from SocialStatus where ID = {0}", ID);
            int RowsAffected = new SqlCommand(Query, Connection).ExecuteNonQuery();
            return RowsAffected != 0;
        }

        public IEnumerable<SocialStatus> All ()
        {
            Connection.Open();
            var SocialStatuss = new List<SocialStatus>();
            string Query = string.Format("select * from SocialStatus");
            var Reader = new SqlCommand(Query, Connection).ExecuteReader();
            while (Reader.Read())
            {
                SocialStatuss.Add(new SocialStatus
                {
                    ID = (long)Reader["ID"],
                    Description = Reader["Description"].ToString()
                });
            }
            Connection.Close();
            return SocialStatuss;
        }
    }
}