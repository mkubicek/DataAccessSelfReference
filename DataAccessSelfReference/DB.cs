
namespace DataAccessSelfReference
{
    using Telerik.OpenAccess;
    using Telerik.OpenAccess.Metadata;

    public partial class Db : OpenAccessContext
    {
        private object contextLock = new object();

        private static readonly BackendConfiguration Backend = GetBackendConfiguration();

        private static readonly MetadataContainer metadataContainer = new DbMetadataSource().GetModel();

        public static string connectionStringName;

        public Db()
            : base(connectionStringName, Backend, metadataContainer)
        {
        }

        public Db(string connection)
            : base(connection, Backend, metadataContainer)
        {
        }

        public Db(BackendConfiguration backendConfiguration)
            : base(connectionStringName, backendConfiguration, metadataContainer)
        {
        }

        public Db(string connection, MetadataSource metadataSource)
            : base(connection, Backend, metadataSource)
        {
        }

        public Db(string connection, BackendConfiguration backendConfiguration, MetadataSource metadataSource)
            : base(connection, backendConfiguration, metadataSource)
        {
        }

        public static BackendConfiguration GetBackendConfiguration()
        {
            BackendConfiguration backend = new BackendConfiguration();
            backend.Backend = "MsSql";
            backend.ProviderName = "System.Data.SqlClient";
#if DEBUG
            backend.Logging.LogEventsToSysOut = true;
#endif
            backend.SecondLevelCache.Enabled = true;
            backend.SecondLevelCache.CacheQueryResults = true;
            backend.SecondLevelCache.NumberOfObjects = 2000000;

            //backend.Logging.LogEvents = LoggingLevel.All;

            backend.SecondLevelCache.Strategy = CacheStrategy.No;
            backend.Runtime.CompiledQueryCacheSize = 2000000;
            backend.Runtime.UseUTCForReadValues = true;
            backend.Runtime.UseUTCForAutoSetValues = true;
            CustomizeBackendConfiguration(ref backend);

            return backend;
        }

        /// <summary>
        /// Allows you to customize the BackendConfiguration of Db.
        /// </summary>
        /// <param name="config">The BackendConfiguration of Db.</param>
        static partial void CustomizeBackendConfiguration(ref BackendConfiguration config);
    }
}
