using System;
using System.Diagnostics;
using System.Timers;

namespace DependencyInjectionScopeTesting.Shared
{
    public abstract class Services : IDisposable
    {
        protected Services()
        {
            Debug.WriteLine($"{GetType()} was created.");
        }

        private bool _disposed;

        protected Action ExtraDispose;

        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                ExtraDispose?.Invoke();
                Debug.WriteLine($"{GetType()} was disposed.");
            }

            _disposed = true;
        }

        ~Services()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public int Counter { get; set; } = 3;
    }

    public class ServiceOne : Services
    {
    }

    public class ServiceTwo : Services
    {
        public ServiceTwo()
        {
            Timer timer = new Timer(1000);
            timer.Elapsed += TimerElapsed;
            timer.Start();

            ExtraDispose = () => { timer.Stop(); };
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            Counter++;
        }
    }
}
