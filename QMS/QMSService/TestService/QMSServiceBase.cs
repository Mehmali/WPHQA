using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace QMSService
{
    public partial class QMSService : ServiceBase
    {
        System.Timers.Timer objTimer = new System.Timers.Timer();
        public QMSService()
        {
            InitializeComponent();
        }

        //private void InitializeComponent()
        //{
        //    throw new NotImplementedException();
        //}

        protected override void OnStart(string[] args)
        {

            Task _task = new Task(() => new Scheduler().RunSchedule());
            _task.Start();

            //new WFMHandler().StartThreads();

            objTimer.Enabled = true;
            objTimer.AutoReset = true;
            objTimer.Interval = 1000 * 60 * 5;
            objTimer.Elapsed += new System.Timers.ElapsedEventHandler(objTimer_Elapsed);
            objTimer.Start();

            //EventLog.WriteEntry("WFM application service started.");
            //Debug.WriteLine("WFM application service started.");

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

        protected override void OnStop()
        {
            objTimer.Stop();
            objTimer.Enabled = false;

            //EventLog.WriteEntry("WFM application service stopped.");
            //Debug.WriteLine("WFM application service stopped.");
        }
    }
}
