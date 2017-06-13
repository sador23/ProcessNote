using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProcessNote
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            processes.ScrollAlwaysVisible = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<Process> processlist = Process.GetProcesses().ToList();
            foreach(Process processelement in processlist){
                processes.Items.Add(processelement.Id + "  " + processelement.ProcessName);
            }

        }

        private void processes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void processes_Click(object sender, EventArgs e)
        {

        }

        private void processes_DoubleClick(object sender, EventArgs e)
        {

            if (processes.SelectedItem != null)
            {
                String id = processes.SelectedItem.ToString().Split(' ')[0];
                foreach (Process processelement in Process.GetProcesses().ToList())
                {
                    
                    if(processelement.Id == int.Parse(id))
                    {
                        details.Items.Clear();
                        try
                        {

                        details.Items.Add("Processor details:");
                        details.Items.Add(string.Format("Total       {0}",
                        processelement.TotalProcessorTime));
                        details.Items.Add(string.Format("User        {0}",
                        processelement.UserProcessorTime));
                        details.Items.Add(string.Format("Privileged  {0}",
                        processelement.PrivilegedProcessorTime));
                        details.Items.Add("Memory usage:");
                        details.Items.Add(string.Format("Current     {0:N0} B", processelement.WorkingSet64));
                        details.Items.Add(string.Format("Peak        {0:N0} B", processelement.PeakWorkingSet64));
                        details.Items.Add(string.Format("Active threads      {0:N0}", processelement.Threads.Count));
                        }
                        catch (Win32Exception) { MessageBox.Show("Unable to access the process! Try another one"); }
                        }
                }
            }
        }
    }
}
