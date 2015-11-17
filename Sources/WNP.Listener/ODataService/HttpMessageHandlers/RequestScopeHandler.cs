// <copyright file="RequestScopeHandler.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.HttpMessageHandlers
{
    using System.Net.Http;
    using ApplicationService;
    using ApplicationService.Implementations;
    using Core;
    using Persistence.WNP;
    using Repository.WNP;

    /// <summary>
    /// Custom delegating handler to extract UnitOfWork if it is present in request.
    /// </summary>
    public class RequestScopeHandler : DelegatingHandler
    {
        private const string RequestScope = "Batch_RequestScope";

        /// <inheritdoc/>
        protected async override System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            var requestScope = ApplicationIntegration.DependencyResolver.ResolveType<ICurrentRequestScope>();

            object existingRequestScope;
            if (request.Properties.TryGetValue(RequestScope, out existingRequestScope))
            {
                requestScope = (CurrentRequestScope)existingRequestScope;
                return await base.SendAsync(request, cancellationToken);
            }
            else
            {
                using (var unitOfWork = ApplicationIntegration.DependencyResolver.ResolveType<IWNPUnitOfWork>())
                {
                    ((WNPUnitOfWork)unitOfWork).SetOperatingCompany = 0;
                    ((CurrentRequestScope)requestScope).OperatingCompany = 0;
                    ((CurrentRequestScope)requestScope).UnitOfWork = unitOfWork;
                    ((CurrentRequestScope)requestScope).User = "petras";

                    var response = await base.SendAsync(request, cancellationToken);

                    if (response.IsSuccessStatusCode)
                    {
                        unitOfWork.Commit();
                    }
                    else
                    {
                        unitOfWork.Rollback();
                    }

                    return response;
                }
            }
        }
    }
}