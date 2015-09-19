using System;
using System.Data;
using System.Data.Common;
using AMSLLC.Listener.Persistence;
using NLog;
using StackExchange.Profiling;
using StackExchange.Profiling.Data;

namespace AMSLLC.Listener.ODataService
{
    public class WNPDBContext : Database
    {
        private readonly Logger _log = LogManager.GetCurrentClassLogger();

        public WNPDBContext(string connectionStringName)
            : base(connectionStringName)
        { }

        public override bool OnException(Exception x)
        {
            return base.OnException(x);
        }

        public override IDbConnection OnConnectionOpened(IDbConnection conn)
        {
            if (MiniProfiler.Current != null)
                return new ProfiledDbConnection((DbConnection)conn, MiniProfiler.Current);

            return base.OnConnectionOpened(conn);
        }

        public override void OnConnectionClosing(IDbConnection conn)
        {
            base.OnConnectionClosing(conn);
        }

        public override void OnExecutingCommand(IDbCommand cmd)
        {
            base.OnExecutingCommand(cmd);
        }

        public override void OnExecutedCommand(IDbCommand cmd)
        {
            base.OnExecutedCommand(cmd);
        }

        public override void OnBeginTransaction()
        {
            _log.Trace(() => "Transaction Begin");
        }

        public override void OnEndTransaction()
        {
            _log.Trace(() => "Transaction End");
        }
    }
}