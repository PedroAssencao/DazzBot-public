using Chatbot.Infrastructure.Meta.Repository.Interfaces;

public class VerificarAtendimentoService : IHostedService, IDisposable
{
    private Timer _timer;
    private bool _isRunning;
    private readonly IServiceProvider _serviceProvider;

    public VerificarAtendimentoService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _timer = new Timer(DoWorkWrapper, null, TimeSpan.Zero, TimeSpan.FromMinutes(9));
    }

    private async void DoWorkWrapper(object state)
    {
        if (_isRunning)
            return;

        _isRunning = true;

        try
        {
            await DoWork(state);
        }
        catch (Exception ex)
        {
            // Lida com exceções aqui
        }
        finally
        {
            _isRunning = false;
        }
    }

    private async Task DoWork(object state)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var repository = scope.ServiceProvider.GetRequiredService<IMetaClient>();
            await repository.CompararData();
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
