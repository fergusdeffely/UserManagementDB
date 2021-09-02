using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementDB
{
    public class User
    {
        string email;
        string name;
        string phone;
        bool admin;
        string password;
        string group;
        string image;

        public User(string email, string name, string phone, bool admin, string password, string group, string image)
        {
            this.email = email;
            this.name = name;
            this.phone = phone;
            this.admin = admin;
            this.password = password;
            this.group = group;
            this.image = image;
        }

        public string Email { get => email; set => email = value; }
        public string Name { get => name; set => name = value; }
        public string Phone { get => phone; set => phone = value; }
        public bool IsAdmin { get => admin; set => admin = value; }
        public string Password { get => password; set => password = value; }
        public string PasswordHidden
        { 
            get
            {
                string hidden = "";
                for(int i = 0;i<this.password.Length;i++)
                {
                    hidden += "*";
                }
                return hidden;
            }
        }
        public string Group { get => group; set => group = value; }
        public string Image { get => image; set => group = image; }

        public override string ToString()
        {
            return $"Email: {this.email}, Name: {this.name}, Phone: {this.phone}, Admin: {this.admin}, Password: {this.PasswordHidden}, Group: { this.group}, Image: {this.image}";
        }
    }
}
