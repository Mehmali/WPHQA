using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using QMSService;
using QMS;
using QMSService;
using TestService;

namespace WindowsServiceTestForm
{
    public partial class Form1 : Form
    {
        System.Timers.Timer objTimer = new System.Timers.Timer();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Task _task = new Task(() => new Scheduler().RunSchedule());
            _task.Start();

            //new WFMHandler().StartThreads();

            objTimer.Enabled = true;
            objTimer.AutoReset = true;
            objTimer.Interval = 1000 * 60 * 5;
            objTimer.Elapsed += new System.Timers.ElapsedEventHandler(objTimer_Elapsed);
            objTimer.Start();
        }

        protected void objTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                Task _task = new Task(() => new Scheduler().RunSchedule());
                _task.Start();

                //new WFMHandler().StartThreads();
            }
            catch (Exception)
            {

            }

        }
    }
}
