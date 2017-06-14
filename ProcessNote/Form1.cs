using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Speech.Recognition;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProcessNote
{
    public partial class Form1 : Form
    {
        //SpeechSynthesizer synthesizer;
        System.Windows.Forms.Timer t;

        public Form1()
        {
            InitializeComponent();
            //processes.ScrollAlwaysVisible = true;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            List<Process> processlist = Process.GetProcesses().ToList();
            foreach(Process processelement in processlist.OrderBy(n => n.ProcessName))
            {
                processes.Items.Add(processelement.Id + "  " + processelement.ProcessName);
                Timer_Shown(sender, e);

                //SpeechRecognizer sr = new SpeechRecognizer();
                //synthesizer = new SpeechSynthesizer();
                //Choices commands = new Choices(new string[] { "start", "exit" });
                //GrammarBuilder gb = new GrammarBuilder();
                //gb.Append(commands);
                //Grammar grammar = new Grammar(gb);
                //sr.SpeechRecognized +=
                //new EventHandler<SpeechRecognizedEventArgs>(sre_SpeechRecognized);
                //sr.LoadGrammar(grammar);
            }

        }

        private void Timer_Shown(object sender, EventArgs e)
        {
            t = new System.Windows.Forms.Timer();
            t.Interval = 5000;
            t.Tick += new EventHandler(t_Tick);
            t.Start();
        }

        void t_Tick(object sender, EventArgs e)
        {
            UpdateDetails();
            UpdateProcess();
            


        }

        public void UpdateProcess()
        {
            String index = processes.SelectedItem.ToString();
            List<Process> processlist = Process.GetProcesses().ToList();
            processes.BeginUpdate();
            processes.Items.Clear();
            int counter = 0;
            foreach (Process processelement in processlist.OrderBy(n => n.ProcessName))
            {
                String code = processelement.Id + "  " + processelement.ProcessName;
                processes.Items.Add(code);
                if (code.Equals(index)) processes.SelectedIndex = counter;
                counter++;

            }

            
            processes.EndUpdate();
        }

        void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            //MessageBox.Show("Speech recognized: " + e.Result.Text);
            //synthesizer.Speak("Speech recognized: " + e.Result.Text);
        }

        private void processes_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void processes_Click(object sender, EventArgs e)
        {

        }

        public void UpdateDetails()
        {
            if (processes.SelectedItem != null)
            {
                String id = processes.SelectedItem.ToString().Split(' ')[0];
                foreach (Process processelement in Process.GetProcesses().ToList())
                {

                    if (processelement.Id == int.Parse(id))
                    {
                        //synthesizer.Speak("Process selected with name " + processelement.ProcessName+ "and id " +processelement.Id);
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

        private void processes_DoubleClick(object sender, EventArgs e)
        {

            UpdateDetails();
        }
    }
}
