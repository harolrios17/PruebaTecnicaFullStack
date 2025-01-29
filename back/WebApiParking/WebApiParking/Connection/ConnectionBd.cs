namespace WebApiParking.Connection
{
    
    public class ConnectionBd
    {
        private string ConnectionString = string.Empty;
        public ConnectionBd()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
            ConnectionString = builder.GetSection("ConnectionStrings:ConnectionSql").Value;
        }

        public string ConnSql()
        {
            return ConnectionString;
        }
    }
}
