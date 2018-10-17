using System.ServiceProcess;
using System.Threading;
using Timer = System.Timers.Timer;
using System.Configuration;
using System;
using NLog;
using System.Threading.Tasks;
using System.Timers;
using Integrator.Service.Helpers;
using System.Linq;

namespace Integrator.Service
{
    public delegate void Trigger();

    public partial class Service1 : ServiceBase
    {
        private readonly string _cnnStr = ConfigurationManager.ConnectionStrings["expDbCnnStr"].ToString();
        private readonly int _timerInterval = int.Parse(ConfigurationManager.AppSettings["timerInterval"].ToString());
        private readonly Timer _timer = new Timer();
        private const string BaseAddress = "Integrator.Service";
        public static Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly CancellationTokenSource _cts = new CancellationTokenSource();
        private readonly CancellationToken _cancellationToken;
        public  SourceDatabaseHelper dbHelper;
        protected Trigger _worker;
        public Service1()
        {
            InitializeComponent();
            dbHelper = new SourceDatabaseHelper();
            Logger.Info("Service is started!", "Successfull");
            _cancellationToken = _cts.Token;
            _worker = Worker;
        }

        public void DebugStart(bool immediate)
        {
            OnStart(immediate ? new[] { "immediate" } : null);
        }

        protected override void OnStart(string[] args)
        {
            if (args == null || !args.Contains("immediate")) return;
            _timer.Enabled = true;
            _timer.Interval = _timerInterval;
            _timer.Elapsed += timer_Elapsed;
            
        }

        protected override void OnStop()
        {
            _timer.Stop();
            _timer.Dispose();
        }

        
        private void Worker()
        {
            _timer.Stop();
            // Call all helpers as they are different works and run them asyncly. 
            Parallel.Invoke(() => dbHelper.FirstWorkHelper(), () =>  dbHelper.SecondWorkHelper(), () =>  dbHelper.ThirdWorkHelper());
            _timer.Start();
        }


        private async void timer_Elapsed(object sender, EventArgs e)
        {

            // Test them via diagnostic

            await Task.Factory.StartNew(_worker.Invoke, _cancellationToken, TaskCreationOptions.LongRunning,
                    TaskScheduler.Current)
                .ConfigureAwait(false);

            //Starter wrkr = Worker;

            //await Task.Run(() => wrkr.Invoke());

            // wrkr();

            //Task mainTask = new Task(() => { Worker(); });
            //mainTask.Start();
        }
    }
}
