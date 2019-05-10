using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VerTrans
{
    public partial class MsnUserList : Form
    {
        List<string> UserList = null;
        string FUser = "";

        public List<string> SetUserList
        {
            set
            {
                UserList = value;
            }            
        }
        public string GetUser
        {
            get 
            {
                return FUser; 
            }
        }
        public MsnUserList()
        {
            InitializeComponent();
        }

        private void MsnUserList_Load(object sender, EventArgs e)
        {
            lb_List.Items.Clear();
            foreach (string s in UserList)
            {
                lb_List.Items.Add(s);
            }
        }

        private void tp05_NO_Click(object sender, EventArgs e)
        {
            FUser = "";
        }

        private void tp05_OK_Click(object sender, EventArgs e)
        {
            FUser = lb_List.SelectedItem.ToString();
        }
    }
}
