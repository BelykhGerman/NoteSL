﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace NotesWebApi.Controllers {
    [ApiController]
    [Route ( "api/[controller]/[action]" )]
    public abstract class BaseController : ControllerBase {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator> ();
        internal Guid UserId => !User.Identity.IsAuthenticated
            ? Guid.Empty
            : Guid.Parse ( User.FindFirst ( ClaimTypes.NameIdentifier ).Value );
    }
}
