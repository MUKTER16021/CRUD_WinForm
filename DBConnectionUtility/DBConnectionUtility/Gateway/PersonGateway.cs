using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DBConnectionUtility.Model;

namespace DBConnectionUtility.Gateway
{
    class PersonGateway
    {
       private string connectionString = ConfigurationManager.ConnectionStrings["connectionUtility"].ConnectionString;
       private SqlCommand command;
       SqlConnection connection;
       private string query;
       private int rowAffected;

        public int SaveUser(Person person)
        {

            connection = new SqlConnection(connectionString);
            connection.Open();
            query ="INSERT INTO UserInfo_t(Name,Father_Name,Mother_Name,Contact_Number,NID_Number,Email_Address,Address)" +
                    " VALUES('" + person.Name + "','" + person.FatherName + "','" + person.MotherName + "'" +
                    ",'" + person.ContactNumber + "','" + person.NIDNumber + "','" + person.EmailAddress + "','" +
                    person.Address + "')";
            command = new SqlCommand(query, connection);
            rowAffected = command.ExecuteNonQuery();
            return rowAffected;
        }

        public bool IsNIDExist(Person person)
        {
            connection =new SqlConnection(connectionString);
            connection.Open();
            query = "SELECT * FROM UserInfo_t WHERE NID_Number='" + person.NIDNumber+"' AND  Id <>'"+person.Id+"' ";
            command=new SqlCommand(query,connection);
            SqlDataReader reader = command.ExecuteReader();
            bool isExist = reader.HasRows;
            reader.Close();
            connection.Close();
            return isExist;

        }

        public List<Person> GetAllPerson()
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            query = "SELECT * FROM UserInfo_t ";
            command = new SqlCommand(query, connection);
            SqlDataReader reader = command.ExecuteReader();

            List<Person> persons=new List<Person>();
            while (reader.Read())
            {
                Person person =new Person();
                person.Name = reader["Name"].ToString();
                person.FatherName = reader["Father_Name"].ToString();
                person.MotherName = reader["Mother_Name"].ToString();
                person.ContactNumber = reader["Contact_Number"].ToString();
                person.NIDNumber = reader["NID_Number"].ToString();
                person.EmailAddress = reader["Email_Address"].ToString();
                person.Address = reader["Address"].ToString();
                person.Id = Convert.ToInt32(reader["Id"]);
                persons.Add(person);
            }
            reader.Close();
            connection.Close();
            return persons;

        }

        public int UpdatePerson(Person person)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            query = "UPDATE UserInfo_t SET Name='"+person.Name+ "', Father_Name='"+person.FatherName+ "'," +
                           " Mother_Name='"+person.MotherName+ "', Contact_Number='"+person.ContactNumber+ "'," +
                           " NID_Number='"+person.NIDNumber+ "', Email_Address='"+person.EmailAddress+ "', " +
                           "Address='"+person.Address+ "' WHERE Id='"+person.Id+"' ";

            command = new SqlCommand(query, connection);
            rowAffected = command.ExecuteNonQuery();
            connection.Close();
            return rowAffected;
        }

        public int DeletePerson(int id)
        {
            int rowAffected;
            connection = new SqlConnection(connectionString);
            connection.Open();
            query = "DELETE FROM UserInfo_t WHERE Id='"+id+"' ";
            command = new SqlCommand(query, connection);
            rowAffected = command.ExecuteNonQuery();
            return rowAffected;
        }


    }
}
