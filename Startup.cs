using System;
using System.Threading.Tasks;
using F12020.Backend.Shared;
using F12020.Backend.Controllers;

namespace F12020.Backend
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await TelemetryDataListener.StartListener();
        }
    }
}
