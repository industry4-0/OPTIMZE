using System;
using System.Collections.Generic;
using System.Threading;

namespace Vimachem.Hackathon
{
    public class Production
    {
        public static Random Random = new Random();

        public static Production Current { get; set; }
        static Production()
        {
            Current = new Production();

        }

        public event EventHandler Initialize;
        public event EventHandler Progress;
        public event EventHandler JobLoaded;
        public event EventHandler Cut;
        public event EventHandler Inspect;
        public event EventHandler Pass;
        public event EventHandler NewTube;

        public event EventHandler Reject;

        public int Total { get; set; }
        public int Count => Tubes.Count;
        public List<Tube> Tubes { get; set; }
        public Tube CurrentTube { get; set; }
        public bool Started { get; set; }
        private int Sleep = 1000;
        private void ExecuteOneStep()
        {
            CurrentTube = new Tube(Count + 1, 5 + Random.NextDouble() / 10,100 + Random.NextDouble(), Random.NextDouble() * (0.99 - 0.6) + 0.6);
            //CurrentTube = new Tube(Count + 1, 5 + Random.NextDouble() / 10);
            Tubes.Add(CurrentTube);
            NewTube?.Invoke(this, EventArgs.Empty);
            Thread.Sleep(Sleep);
            do
            {
                CurrentTube.Cut();
                Cut?.Invoke(this, EventArgs.Empty);
                Thread.Sleep(Sleep);
                Inspect?.Invoke(this, EventArgs.Empty);
                Thread.Sleep(Sleep);
            } while (!CurrentTube.Inspect() && CurrentTube.Cuts <= 3);

            if (CurrentTube.Cuts >= 3)
            {
                Reject?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                Pass?.Invoke(this, EventArgs.Empty);
            }
            Thread.Sleep(Sleep);
        }
        public void RunJob(int total)
        {
            if (Started)
            {
                return;
            }
            Total = total;
            Tubes = new List<Tube>();
            CurrentTube = null;

            JobLoaded?.Invoke(this, EventArgs.Empty);

            Started = true;
            Initialize?.Invoke(this, EventArgs.Empty);
            Thread.Sleep(1000);
            Progress?.Invoke(this, EventArgs.Empty);
            while (Count < Total)
            {
                ExecuteOneStep();
            }
            Started = false;
        }

    }
}
