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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadAllData();
            ToDoListEntities entities = new ToDoListEntities();
            var user = entities.User.FirstOrDefault(x => x.Id == Program.LoginedUserId);
            lblLoginedUser.Text = user.Name;
        }
        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            txtDescription.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            chkIsCompleted.Checked = (bool)dataGridView1.CurrentRow.Cells[3].Value;
        }

        private void LoadAllData()
        {
            ToDoListEntities entities = new ToDoListEntities();
            var myToDo = entities.ToDo.ToList();
            dataGridView1.DataSource = myToDo;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDescription.Text))
            {
                ToDoListEntities entities = new ToDoListEntities();
                ToDo toDo = new ToDo()
                {
                    Description = txtDescription.Text,
                    IsCompleted = chkIsCompleted.Checked,
                    CreateDate = DateTime.Now,
                    CreatedUserId = Program.LoginedUserId,

                };

                entities.ToDo.Add(toDo);
                int addedCount = entities.SaveChanges();
                if
                    (addedCount > 0)
                {
                    MessageBox.Show("EKLENDİ");
                    LoadAllData();
                }

            }
            else
            {
                MessageBox.Show("Açıklama boş olamaz");
            }

        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDescription.Text))
            {
                ToDoListEntities entities = new ToDoListEntities();
                int selectedId = (int)dataGridView1.CurrentRow.Cells[0].Value;
                var toDo = entities.ToDo.FirstOrDefault(x => x.Id == selectedId);
                if (toDo != null)
                {
                    toDo.Description = txtDescription.Text;
                    toDo.IsCompleted = chkIsCompleted.Checked;
                    toDo.UpdatedUserId = Program.LoginedUserId;
                    toDo.UpdateDate = DateTime.Now;
                    int updatedCount = entities.SaveChanges();
                    if
                    (updatedCount > 0)
                    {
                        MessageBox.Show("GÜNCELLENDİ");
                        LoadAllData();
                    }
                }
            }
            else
            {
                MessageBox.Show("Açıklama boş olamaz");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                ToDoListEntities entities = new ToDoListEntities();
                int selectedId = (int)dataGridView1.CurrentRow.Cells[0].Value;
                var toDo = entities.ToDo.FirstOrDefault(x => x.Id == selectedId);
                if (toDo != null)
                {
                    entities.ToDo.Remove(toDo);
                    int updatedCount = entities.SaveChanges();
                    if
                    (updatedCount > 0)
                    {
                        MessageBox.Show("SİLİNDİ");
                        LoadAllData();
                    }
                }
            }
            else
            {
                MessageBox.Show("Lütfen seçiniz");
            }
        }
    }
}
