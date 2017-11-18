using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Informacines
{
    public partial class Form1 : Form
    {
        ApplicationDbContext context = new ApplicationDbContext();
        List<Project> projects = new List<Project>();
        List<AspNetUsers> users = new List<AspNetUsers>();

        public Form1()
        {
            InitializeComponent();

            ProjectsRepository ProjectRepository = new ProjectsRepository(context);
            AspNetUsersRepository UserRepository = new AspNetUsersRepository(context);

            projects = ProjectRepository.GetAll();
            users = UserRepository.GetAll();
            pwdTextbox.PasswordChar = '*';
        }

        private void logInButton_Click(object sender, EventArgs e)
        {
           MessageBox.Show(VerifyUser().ToString());
        }

        public static bool VerifyHashedPassword(string hashedPassword, string password)
        {
            byte[] buffer4;
            if (hashedPassword == null)
            {
                return false;
            }
            if (password == null)
            {
                throw new ArgumentNullException("password");
            }
            byte[] src = Convert.FromBase64String(hashedPassword);
            if ((src.Length != 0x3D) || (src[1] != 0))
            {
                return false;
            }
            byte[] dst = new byte[0x10];
            Buffer.BlockCopy(src, 1, dst, 0, 0x10);
            byte[] buffer3 = new byte[0x20];
            Buffer.BlockCopy(src, 0x11, buffer3, 0, 0x20);
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, dst, 0x3e8))
            {
                buffer4 = bytes.GetBytes(0x20);
            }
            return ByteArraysEqual(buffer3, buffer4);
        }

        public static bool ByteArraysEqual(byte[] b1, byte[] b2)
        {
            if (b1 == b2) return true;
            if (b1 == null || b2 == null) return false;
            if (b1.Length != b2.Length) return false;
            for (int i = 0; i < b1.Length; i++)
            {
                if (b1[i] != b2[i]) return false;
            }
            return true;
        }

        public bool VerifyUser()
        {
            var user = users.Find(x => emailTextbox.Text.Equals(x.Email));
            if (user == null)
            {
                MessageBox.Show("User does not exist");
                return false;
            }

            //   PasswordHash.ValidatePassword(pwdTextbox.Text, user.PasswordHash);
            //  PasswordVerificationResult passwordVerRes = new PasswordHasher().VerifyHashedPassword(user.PasswordHash, pwdTextbox.Text);
              return VerifyHashedPassword(user.PasswordHash, pwdTextbox.Text);
        }
    }
}
