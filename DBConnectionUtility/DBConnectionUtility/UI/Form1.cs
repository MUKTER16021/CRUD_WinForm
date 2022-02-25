using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;
using DBConnectionUtility.Manager;
using DBConnectionUtility.Model;

namespace DBConnectionUtility.UI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        PersonManager personManager = new PersonManager();
        private void saveButton_Click(object sender, EventArgs e)
        {
            if (saveButton.Text == "Save")
            {
                Person person = Person();

                string message = personManager.SaveUser(person);
                MessageBox.Show(message);
            }
            else
            {
               
                Person person = Person();
                person.Id = Convert.ToInt32(hiddenIdLabel.Text);
                string message = personManager.UpdatePerson(person);
                MessageBox.Show(message);
                if (message == "NID Already Exist!")
                {
                    saveButton.Text = "Update";
                }
                else
                {
                    saveButton.Text = "Save";
                }
            }
            LoadAllPerson();

            TextBoxClear();

        }

        private Person Person()
        {
            Person person = new Person();
            person.Name = nameTextBox.Text;
            person.FatherName = fatherNameTextBox.Text;
            person.MotherName = MotherNameTextBox.Text;
            person.ContactNumber = contactNumberTextBox.Text;
            person.NIDNumber = nIDNumberTextBox.Text;
            person.EmailAddress = emailAddressTextBox.Text;
            person.Address = addressTextBox.Text;
            return person;
        }

        public void TextBoxClear()
        {
            nameTextBox.Text = "";
            fatherNameTextBox.Text = "";
            MotherNameTextBox.Text = "";
            contactNumberTextBox.Text = "";
            nIDNumberTextBox.Text = "";
            emailAddressTextBox.Text = "";
            addressTextBox.Text = "";
                
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadAllPerson();

        }

        private void LoadAllPerson()
        {
            showListView.Items.Clear();
            List<Person> allPerson = personManager.GetAllPerson();
           
            foreach (Person person in allPerson)
            {
                ListViewItem item = new ListViewItem();
                item.Text = person.Name;
                item.SubItems.Add(person.FatherName);
                item.SubItems.Add(person.MotherName);
                item.SubItems.Add(person.ContactNumber);
                item.SubItems.Add(person.NIDNumber);
                item.SubItems.Add(person.EmailAddress);
                item.SubItems.Add(person.Address);
                item.Tag = person;

                showListView.Items.Add(item);
            }
        }

        private void showListView_DoubleClick(object sender, EventArgs e)
        {
            ListViewItem selectecdItem = showListView.SelectedItems[0];
            Person person=selectecdItem.Tag as Person;
            nameTextBox.Text = person.Name;
            fatherNameTextBox.Text = person.FatherName;
            MotherNameTextBox.Text = person.MotherName;
            contactNumberTextBox.Text = person.ContactNumber;
            nIDNumberTextBox.Text = person.NIDNumber;
            emailAddressTextBox.Text = person.EmailAddress;
            addressTextBox.Text = person.Address;
            hiddenIdLabel.Text = person.Id.ToString();

            saveButton.Text = "Update";
            deleteButton.Enabled = true;

        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            int id =Convert.ToInt32(hiddenIdLabel.Text);
            DialogResult result = MessageBox.Show("Are you sure delete this person", "Delete Person",
                MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                string message = personManager.DeletePerson(id);
                MessageBox.Show(message);
                LoadAllPerson();
                saveButton.Text = "Save";
                deleteButton.Enabled = false;
                
            }
            TextBoxClear();
        }
    }
}
