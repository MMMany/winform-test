using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TestWinformApp.Internal;

namespace TestWinformApp
{
    public enum TestState
    {
        None,
        Ready,
        Pause,
        Setup,
        Running,
    }

    public sealed class TestManager
    {
        public static TestManager Instance => _instance.Value;

        //public TestState State { get; private set; } = TestState.Ready;
        public TestState State
        {
            get => _state;
            set
            {
                if (Monitor.IsEntered(_stateLock)) return;
                lock (_stateLock)
                {
                    //PrevState = _state;
                    _state = value;
                }
            }
        }
        public TestState PrevState { get; private set; }

        public event EventHandler<string> TestProgress;
        public event EventHandler<TestResult> TestComplete;

        public async Task Start()
        {
            _cts = new CancellationTokenSource();

            try
            {
                // Prevent start again
                if (State > TestState.Pause)
                {
                    Debug("Warn: Test still running...");
                    return;
                }

                // Check Setup
                if (State == TestState.Ready || State == TestState.Pause)
                {
                    if (!_setupComplete)
                    {
                        // Start new or Resume setup
                        await Setup();
                    }
                    else
                    {
                        // Resume
                    }
                }

                // Run
                Debug("Test Start");
                State = TestState.Running;
                while (_stack.Count > 0)
                {
                    var tc = _stack.Peek();

                    Debug($"Run TC: {tc.Id} | {tc.Guid}");
                    await tc.Run(_cts.Token);

                    _stack.Pop();
                }

                var failList = from tc in _tcList
                               where tc.Result != TestResult.Pass
                               select tc;
                var result = failList.Count() > 0 ? TestResult.Fail : TestResult.Pass;
                Debug($"Test Result : {result} ({_tcList.Count() - failList.Count()} / {_tcList.Count()})");
                Debug($"Test Done : {result}");
                TestComplete?.Invoke(this, result);
                State = TestState.Ready;
                _setupComplete = false;
            }
            catch (Exception ex)
            {
                if (ex is OperationCanceledException)
                {
                    if (State == TestState.Pause)
                    {
                        Debug("Test Paused");
                        TestComplete?.Invoke(this, TestResult.Pause);
                    }
                    else
                    {
                        Debug("Test Cancelled");
                        TestComplete?.Invoke(this, TestResult.Cancel);
                    }
                }
                else
                {
                    TestComplete?.Invoke(this, TestResult.Error);
                }
                throw ex;
            }
            finally
            {
                _cts?.Dispose();
                _cts = null;
            }
        }

        public void Pause()
        {
            if (State <= TestState.Pause) return;

            Debug("Received Pause Request...");
            State = TestState.Pause;
            _cts?.Cancel();
        }

        public void Reset()
        {
            if (State <= TestState.Ready) return;

            Debug("Received Reset Request...");
            State = TestState.Ready;
            _setupComplete = false;
            _cts?.Cancel();
        }

        #region Private
        private static Lazy<TestManager> _instance = new Lazy<TestManager>(() => new TestManager());

        private TestManager()
        {
            State = TestState.Ready;
        }

        private CancellationTokenSource _cts;
        private Stack<TestCase> _stack = new Stack<TestCase>();
        private List<TestCase> _tcList = new List<TestCase>();
        private TestState _state;
        private bool _setupComplete;
        private readonly object _stateLock = new object();

        private async Task Setup()
        {
            State = TestState.Setup;
            Debug("Test Setup");

            // Reset old TC List
            foreach (var tc in _tcList)
            {
                tc.Progress -= this.TcProgress;
            }
            _tcList.Clear();
            //GC.Collect();

            // Generate TC List
            Debug("Generate TC List");
            var supportedList = new[] { "HF2-1", "HF2-2", "HF2-3", "HF2-4", "HF2-5" };
            foreach (var id in supportedList)
            {
                var tc = new TestCase(id);
                tc.Progress += this.TcProgress;
                _tcList.Add(tc);
            }

            _stack.Clear();
            foreach (var tc in _tcList.ToArray().Reverse())
            {
                Debug($"Add TC: {tc.Id} | {tc.Guid}");
                _stack.Push(tc);
                await Task.Delay(3000, _cts.Token);
            }

            _setupComplete = true;
        }

        private void Debug(string message)
        {
            Logging.Logger.Debug(message);
            TestProgress?.Invoke(this, message);
        }

        private void TcProgress(object sender, string message)
        {
            Debug(message);
        }
        #endregion
    }
}
