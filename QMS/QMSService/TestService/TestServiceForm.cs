using QMSService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QMSService
{
    public partial class TestServiceForm : Form
    {
        System.Timers.Timer objTimer = new System.Timers.Timer();
        public TestServiceForm()
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
            objTimer.Interval = 1000 * 60 * 60 * 24;
            objTimer.Elapsed += new System.Timers.ElapsedEventHandler(objTimer_Elapsed);
            objTimer.Start();
        }

        protected void objTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                Task _task = new Task(() => new Scheduler().RunSchedule());
                _task.Start();
            }
            catch (Exception)
            {

            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
