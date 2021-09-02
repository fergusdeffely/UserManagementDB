using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace UserManagementDB
{
    public class UsersDB
    {
        public List<User> GetUsers(DBConnection db)
        {
            string sql = "SELECT * FROM users;";
            using var cmd = new MySqlCommand(sql, db.Connection);

            MySqlDataReader rdr = null;
            List<User> userList = new List<User>();

            try
            {
                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    // First four columns (email, name, phone, admin) are not NULL in the database
                    string email = rdr.GetString(0);
                    string name = rdr.GetString(1);
                    string phone = rdr.GetString(2);
                    int admin = rdr.GetInt32(3);

                    // password, group and image can be NULL
                    string password = GetNullableString(rdr, 4);
                    string group = GetNullableString(rdr, 5);
                    string image = GetNullableString(rdr, 6);

                    userList.Add(new User(email, name, phone, admin > 0, password, group, image));
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Exception found: " + ex.Message);
            }

            rdr.Close();

            return userList;
        }

        public bool Insert(DBConnection db, User user)
        {
            var sql = "INSERT INTO users (email, name, phone, administrator, password, org_group, image) ";
            sql +=    "VALUES(@email, @name, @phone, @administrator, @password, @group, @image);";
            using var cmd = new MySqlCommand(sql, db.Connection);

            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.Parameters.AddWithValue("@name", user.Name);
            cmd.Parameters.AddWithValue("@phone", user.Phone);
            cmd.Parameters.AddWithValue("@administrator", user.IsAdmin);
            cmd.Parameters.AddWithValue("@password", user.Password);
            cmd.Parameters.AddWithValue("@group", user.Group);
            cmd.Parameters.AddWithValue("@image", user.Image);

            try
            {
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Exception found inserting record in table 'users':\n\n" + ex.Message, "Database exception", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }

        public bool Update(DBConnection db, string email, User user)
        {
            string sql = "UPDATE users SET email = @email, name = @name, phone = @phone, administrator = @administrator, password = @password, org_group = @group, image = @image WHERE email = @originalEmail;";

            MySqlCommand cmd = new MySqlCommand(sql, db.Connection);

            cmd.Parameters.AddWithValue("@email", user.Email);
            cmd.Parameters.AddWithValue("@name", user.Name);
            cmd.Parameters.AddWithValue("@phone", user.Phone);
            cmd.Parameters.AddWithValue("@administrator", user.IsAdmin);
            cmd.Parameters.AddWithValue("@password", user.Password);
            cmd.Parameters.AddWithValue("@group", user.Group);
            cmd.Parameters.AddWithValue("@image", user.Image);
            cmd.Parameters.AddWithValue("@originalEmail", email);

            try
            {
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Exception found updating record in table 'users':\n\n" + ex.Message, "Database exception", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                return false;
            }

            return true;
        }

        public bool Delete(DBConnection db, string email)
        {
            string sql = "DELETE FROM users WHERE email = @email;";

            MySqlCommand cmd = new MySqlCommand(sql, db.Connection);

            cmd.Parameters.AddWithValue("@email", email);
            
            try
            {
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Exception found deleting record in table 'users':\n\n" + ex.Message, "Database exception", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                return false;
            }

            return true;
        }

        private string GetNullableString(MySqlDataReader rdr, int ordinal)
        {
            string columnText = null;

            if(!rdr.IsDBNull(ordinal))
            {
                columnText = rdr.GetString(ordinal);
            }

            return columnText;
        }
    }
}
