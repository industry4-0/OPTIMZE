using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vimachem.Hackathon
{

    public class Machine
    {
        private System.Timers.Timer runningTimer;
        public event EventHandler<string> Error;
        public event EventHandler<string> Message;

        public event EventHandler Started;
        public event EventHandler Stopped;
        public event EventHandler TimeElapsed;
        public bool BearingsActive { get; set; }
        public bool MachineActive { get; set; }
        public int EncoderValue { get; set; }

        public static Machine Current { get; set; }
        static Machine()
        {
            Current = new Machine();
        }

        public Machine()
        {
            BearingsActive = false;
            runningTimer = new System.Timers.Timer(1000);
            runningTimer.Elapsed += RunningTimer_Elapsed;
            EncoderValue = 0;
        }

        private void RunningTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            EncoderValue++;

            TimeElapsed?.Invoke(this, EventArgs.Empty);
        }

        public void StartMachine()
        {
            if (!BearingsActive)
            {
                Error?.Invoke(this, "Cannot start Engine when Bearings are not started!!");
                return;
            }
            if (!MachineActive)
            {

                Task.Run(() =>
                {
                    MachineActive = true;
                    runningTimer.Enabled = true;
                    Started?.Invoke(this, EventArgs.Empty);
                    Message?.Invoke(this, "Machine Running");

                });
            }

        }
        public void StopMachine()
        {
            if (MachineActive)
            {
                Task.Run(() =>
                {
                    MachineActive = false;
                    runningTimer.Enabled = false;
                    Stopped?.Invoke(this, EventArgs.Empty);
                    Message?.Invoke(this, "Machine Stopped");

                });
            }
        }
        public void ResetMachine()
        {
            StopMachine();
            EncoderValue = 0;
        }

        public void StartBearings()
        {
            BearingsActive = true;
            Message?.Invoke(this, "Bearings Running");
        }
        public void StopBearings()
        {
            BearingsActive = false;
            StopMachine();
            Message?.Invoke(this, "Bearings Stopped");


        }
        public void ResetBearings()
        {
            BearingsActive = false;
            StopMachine();
        }
    }
}
