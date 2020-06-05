using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Eos.Abstracts.Bl;
using Eos.Bl;
using NUnit.Framework;

namespace Eos.Tests
{
    [TestFixture]
    public class ItemServiceTests
    {
        private readonly IItemService _itemService;

        public ItemServiceTests()
        {
            var container = Ioc.GetContainer();

            _itemService = container.Resolve<IItemService>();
        }

        [Test]
        public async Task InitialFilling()
        {
            await _itemService.InitialFilling();
        }

        [Test]
        public async Task StringCompareTest()
        {
            var items = await _itemService.GreaterTitle("A");
            
            Assert.IsTrue(items.Any());
        }
    }
}