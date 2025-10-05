using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SabaRayane.Contract.Application.Commands.Customers.Notifications;

namespace SabaRayane.Contract.Infrastructure
{
    public class AgreementNotificationWorker : BackgroundService
    {
        private readonly ILogger<AgreementNotificationWorker> logger;
        private readonly IServiceProvider serviceProvider;

        public AgreementNotificationWorker(ILogger<AgreementNotificationWorker> logger, IServiceProvider serviceProvider)
        {
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    using var scope = serviceProvider.CreateScope();
                    var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

                    var createNotificationCommand = new CreatePossibleNotificationsCommand();
                    await mediator.Send(createNotificationCommand, cancellationToken);

                    await Task.Delay(TimeSpan.FromMinutes(1), cancellationToken);
                }
            }
            catch (OperationCanceledException)
            {
                
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "{Message}", ex.Message);

                Environment.Exit(1);
            }
        }
    }
}
