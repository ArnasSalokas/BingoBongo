using System;
using Microsoft.Extensions.Logging;
using Moq;
using Template.Repositories.Base.Contracts;

namespace Template.Tests.Helper
{
    public static class MockDependencyServiceHelper
    {
        public static Mock<IServiceProvider> BaseServiceMockedProvider(this Mock<IServiceProvider> serviceProvider)
        {
            var logger = new Mock<ILoggerFactory>();
            var logging = new Mock<ILogger>();
            var sqlTransaction = new Mock<ITransactionStore>();

            serviceProvider.Setup(x => x.GetService(typeof(ITransactionStore))).Returns(sqlTransaction.Object);
            serviceProvider.Setup(x => x.GetService(typeof(ILoggerFactory))).Returns(logger.Object);

            logger.Setup(x => x.CreateLogger(It.IsAny<string>())).Returns(logging.Object);
            serviceProvider.Setup(x => x.GetService(typeof(ILogger))).Returns(logging.Object);

            return serviceProvider;
        }
    }
}
