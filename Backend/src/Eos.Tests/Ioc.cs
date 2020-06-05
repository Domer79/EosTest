using Autofac;
using Eos.Abstracts.Bl;
using Eos.Abstracts.Data;
using Eos.Bl;
using Eos.Data;
using Eos.Data.EF;
using Eos.Data.EF.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Eos.Tests
{
    public class Ioc
    {
        public static IContainer GetContainer()
        {
            var builder = new ContainerBuilder();
            builder.Register(EosContextRegister).As<EosContext>();
            builder.Register(ConfigurationRegister).As<IConfiguration>().SingleInstance();
            
            builder.RegisterType<ItemRepository>().As<IItemRepository>();
            builder.RegisterType<GlobalItemRepository>().As<IGlobalItemRepository>();

            builder.RegisterType<ItemService>().As<IItemService>();
            
            return builder.Build();
        }
        
        private static IConfigurationRoot ConfigurationRegister(IComponentContext context)
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }
        
        private static EosContext EosContextRegister(IComponentContext context)
        {
            var config = context.Resolve<IConfiguration>();
            var connectionString = config.GetConnectionString("common");
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlServer(connectionString, builder =>
            {
                builder.AddStringCompareSupport();
            });

            return new EosContext(optionsBuilder.Options);
        }
    }
}