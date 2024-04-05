using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Security.Constants;
using Core.Security.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Core.Application.Pipelines.Authorization;

public class AuthorizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>, ISecuredRequest
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthorizationBehavior(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var userRoleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();

        if (userRoleClaims == null)
            throw new AuthorizationException("You are not authorized.");

        var isAnyMatchedUserRoleClaimsWithRequestRoles =
            userRoleClaims
                .Any(userRoleClaim =>
                    userRoleClaim == GeneralOperationClaims.Admin
                    || request.Roles.Any(role => role == userRoleClaim));

        if (isAnyMatchedUserRoleClaimsWithRequestRoles)
            throw new AuthorizationException("You are not authorized.");

        return await next();
    }
}
