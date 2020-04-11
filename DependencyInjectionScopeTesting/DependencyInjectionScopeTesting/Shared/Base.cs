using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Components;

namespace DependencyInjectionScopeTesting.Shared
{
    public class Base<T> : ComponentBase, IDisposable
    {
        [Inject]
        public T Service { get; set; }

        public Base()
        {
            Debug.WriteLine($"{GetType()} was created.");
        }

        private bool _disposed;

        private void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                Service = default;
                Debug.WriteLine($"{GetType()} was disposed.");
            }

            _disposed = true;
        }

        ~Base()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
