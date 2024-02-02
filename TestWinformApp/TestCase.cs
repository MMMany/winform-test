using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestWinformApp
{
    public enum TestResult
    {
        None,
        Error,
        Cancel,
        Pause,
        Skip,
        Fail,
        Pass,
    }
    public class TestCase
    {
        public string Id { get; private set; }
        public TestResult Result { get; private set; }
        public Guid Guid { get; private set; }

        public event EventHandler<string> Progress;

        public TestCase(string id)
        {
            Id = id;
            Guid = Guid.NewGuid();
        }

        public async Task Run(CancellationToken token)
        {
            _token = token;

            try
            {
                // TODO: Subscribe Log Monitor

                Progress?.Invoke(this, $"({Id}) Wait UI element");
                await WaitForUiElement(1000);

                Progress?.Invoke(this, $"({Id}) Capture Image");
                var targetPath = await CaptureImage();

                Progress?.Invoke(this, $"({Id}) Image Compare");
                Result = await ImageCompare(targetPath) ? TestResult.Pass : TestResult.Fail;

                Progress?.Invoke(this, $"({Id}) Done");
            }
            catch (Exception ex)
            {
                if (ex is OperationCanceledException)
                    Result = TestResult.Cancel;
                else
                    Result = TestResult.Error;

                throw ex;
            }
            finally
            {
                // TODO: Unsubscribe Log Monitor
            }
        }

        #region Private
        private CancellationToken _token;

        private async Task WaitForUiElement(int time)
        {
            await Task.Delay(time, _token);
        }

        private async Task<string> CaptureImage()
        {
            await Task.Delay(5000, _token);
            return "ImagePath";
        }

        private async Task<bool> ImageCompare(string targetPath)
        {
            await Task.Delay(10000, _token);
            return true;
        }
        #endregion
    }
}
