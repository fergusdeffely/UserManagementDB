using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MySql.Data.MySqlClient;

namespace UserManagementDB
{
    public partial class MainForm : Form
    {
        DBConnection db = null;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void buttonConnect_Click(object sender, EventArgs e)
        {
            this.db = new DBConnection("localhost", "usermanagement", "csharp", "password");

            if (this.db.OpenConnection())
            {
                MessageBox.Show("Connected to database.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Problem connecting to database.", "Connection failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonQuery_Click(object sender, EventArgs e)
        {
            UsersDB usersDB = new UsersDB();

            List<User> userList = usersDB.GetUsers(this.db);

            string userRecordsMessage = "The users in the database are:\n\n";

            foreach(User user in userList)
            {
                userRecordsMessage += $"{user.ToString()}\n\n";
            }

            MessageBox.Show(userRecordsMessage);
        }

        private void buttonInsert_Click(object sender, EventArgs e)
        {
            User user = new User("aaaa@aaa.com", "aaaa", "12345", false, "aaaa", "aaaa", string.Empty);

            UsersDB usersDB = new UsersDB();

            if(usersDB.Insert(this.db, user))
            {
                MessageBox.Show($"Successfully inserted record in users table:\n\n{user.ToString()}", "Insert Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            User user = new User("bbbb@bbb.com", "aaaa", "12345", false, "aaaa", "aaaa", null);

            UsersDB usersDB = new UsersDB();

            if (usersDB.Update(this.db, "aaaa@aaa.com", user))
            {
                MessageBox.Show($"Successfully updated record in users table:\n\n{user.ToString()}", "Update Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            UsersDB usersDB = new UsersDB();

            if (usersDB.Delete(this.db, "bbbb@bbb.com"))
            {
                MessageBox.Show($"Successfully deleted record in users table:\n\n", "Delete Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.db.CloseConnection();
        }
    }
}
