using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using TestWinformApp.Internal;

namespace TestWinformApp
{
    internal sealed class Dummy
    {
        public string Id { get; private set; }

        public Dummy(string id, int time, int interval = 1000)
        {
            Id = id;
            _time = time;
            _interval = interval;
        }

        public bool Run(CancellationToken token)
        {
            try
            {
                return Task<bool>.Run(async () => await RunAsync(token), token).Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> RunAsync(CancellationToken token)
        {
            try
            {
                for (int i = 0; i < _time; i++)
                {
                    token.ThrowIfCancellationRequested();
                    Logging.Logger.Debug($"Do it ({Id}) - {i}");
                    await Task.Delay(_interval);
                }
            }
            catch (OperationCanceledException oce)
            {
                Logging.Logger.Debug($"Dummy ({Id}): Cancelled");
                throw oce;
            }
            catch (Exception ex)
            {
                Logging.Logger.Error($"Error [Dummy ({Id})]: {ex.Message}");
                throw ex;
            }
            return true;
        }

        private int _time;
        private int _interval;
    }
}
