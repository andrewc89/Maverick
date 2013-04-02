using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MaverickExample.Models;

namespace Maverick.Example.Database
{
    internal class MockDb
    {
        internal Dictionary<long, SocialStatus> SocialStatuses;
        internal Dictionary<long, Person> Persons;
     
        internal MockDb ()
        {
            SocialStatuses = new Dictionary<long, SocialStatus>
            {
                {
                    1,
                    new SocialStatus
                    {
                        ID = 1,
                        Description = "Too cool for school",
                    }
                }
            };
            Persons = new Dictionary<long, Person>
            {
                {
                    1, 
                    new Person 
                    {
                        ID = 1,
                        FirstName = "Rebecca",
                        LastName = "Watson",
                        Status = SocialStatuses[1]
                    }
                },
                {
                    2, 
                    new Person 
                    {
                        ID = 2,
                        FirstName = "Adria",
                        LastName = "Richards",
                        Status = SocialStatuses[1]
                    }
                }
            };
        }        
    }
}