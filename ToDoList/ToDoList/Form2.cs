using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ToDoList
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void btnLogIn_Click(object sender, EventArgs e)
        {
            ToDoListEntities entities = new ToDoListEntities();
            var user = entities.User.FirstOrDefault(x => x.Name == txtUserName.Text && x.Password == txtPassword.Text);
            if (user != null) {
                Form1 form1 = new Form1();
                Program.LoginedUserId = user.Id;
                form1.Show();

            }
        }

        private void btnSignIn_Click(object sender, EventArgs e)
        {
            ToDoListEntities entities = new ToDoListEntities();
            User user = new User() 
            { 
                Name = txtUserName.Text,
                Password = txtPassword.Text,
            };
            entities.User.Add(user);
            entities.SaveChanges();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}

