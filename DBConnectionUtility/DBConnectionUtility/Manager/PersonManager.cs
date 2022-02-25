using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DBConnectionUtility.Gateway;
using DBConnectionUtility.Model;

namespace DBConnectionUtility.Manager
{
    class PersonManager
    {
        PersonGateway personGateway=new PersonGateway();

        public string SaveUser(Person person)
        {
            bool isNIDExist= personGateway.IsNIDExist(person);
            if (isNIDExist)
            {
                return "NID Already Exist";
            }
            int rowAffected= personGateway.SaveUser(person);
            if (rowAffected > 0)
            {
                return " Save Success";
            }
            return "Save Failed";

        }

        public List<Person> GetAllPerson()
        {
            return personGateway.GetAllPerson();
        }

        public string UpdatePerson(Person person)
        {
            if (personGateway.IsNIDExist(person))
            {
                return "NID Already Exist!";
            }
            int rowAffected = personGateway.UpdatePerson(person);
            if (rowAffected > 0)
            {
                return " Update SuccessFully";
            }
            return "Update Failed";
        }

        public string DeletePerson(int id)
        {
            int rowAffected = personGateway.DeletePerson(id);
            if (rowAffected > 0)
            {
                return " Delete SuccessFully";
            }
            return "Delete Failed";

        }
    }
}
