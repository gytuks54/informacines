﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Informacines
{
    public partial class MainFormUI : Form
    {
        ApplicationDbContext context = new ApplicationDbContext();
        List<Project> projects = new List<Project>();
      //  List<AspNetUsers> users = new List<AspNetUsers>();
        List<ProjectMembers> projectMembers = new List<ProjectMembers>();
        LogInForm LoginForm = new LogInForm();
        public AspNetUsers currentUser { get; set; }

        public MainFormUI()
        {
            InitializeComponent();

            ProjectsRepository ProjectRepository = new ProjectsRepository(context);
       //     AspNetUsersRepository UserRepository = new AspNetUsersRepository(context);
            ProjectMembersRepository ProjectMemberRepository = new ProjectMembersRepository(context);

            projects = ProjectRepository.GetAll();
      //      users = UserRepository.GetAll();
            projectMembers = ProjectMemberRepository.GetAll();

            currentUser = LoginForm.GetUser();

        }

        private void MyProjectsBtn_Click(object sender, EventArgs e)
        {

        }
    }
}
