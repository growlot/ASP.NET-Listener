namespace AMSLLC.Listener.ODataService.Services
{
    using AMSLLC.Listener.ODataService.Services.Impl.ODataQueryHandler;

    /// <summary>
    /// OData query handler factory interface.
    /// </summary>
    public interface IODataQueryHandlerFactory
    {
        IODataSingleResultQueryHandler NewSingleResultQuery();
    }
}