// <copyright file="UnitOfWorkHandler.cs" company="Advanced Metering Services LLC">
//     Copyright (c) Advanced Metering Services LLC. All rights reserved.
// </copyright>

namespace AMSLLC.Listener.ODataService.HttpMessageHandlers
{
    using System.Net.Http;
    using Core;
    using Repository.WNP;

    /// <summary>
    /// Custom delegating handler to extract UnitOfWork if it is present in request.
    /// </summary>
    public class UnitOfWorkHandler : DelegatingHandler
    {
        private const string UnitOfWork = "Batch_UnitOfWork";

        /// <inheritdoc/>
        protected async override System.Threading.Tasks.Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, System.Threading.CancellationToken cancellationToken)
        {
            var scope = request.GetDependencyScope();
            var currentUnitOfWork = ApplicationIntegration.DependencyResolver.ResolveType<CurrentUnitOfWork>();

            object existingUnitOfWork;
            if (request.Properties.TryGetValue(UnitOfWork, out existingUnitOfWork))
            {
                currentUnitOfWork.UnitOfWork = (IWNPUnitOfWork)existingUnitOfWork;
                return await base.SendAsync(request, cancellationToken);
            }
            else
            {
                using (var unitOfWork = ApplicationIntegration.DependencyResolver.ResolveType<IWNPUnitOfWork>())
                {
                    currentUnitOfWork.UnitOfWork = unitOfWork;

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