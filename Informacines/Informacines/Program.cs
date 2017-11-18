using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Informacines
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            ProjectsRepository repository = new ProjectsRepository(context);
            List<Project> projects = repository.GetAll();
            //labas
            foreach (Project project in projects)
            {
                Console.WriteLine(project.Title);
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
