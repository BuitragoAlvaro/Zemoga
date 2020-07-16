using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Zemoga.App.Interfaces;
using Zemoga.App.ViewModels;

namespace Zemoga.UI.Controllers
{
    [Route("api/Blog")]
    [ApiController]
    public class ApiBlogController : ControllerBase
    {
        private IApiPostService _apiPostService;

        public ApiBlogController(IApiPostService apiPostService)
        {
            _apiPostService = apiPostService;
        }

        [Route("PendingPosts")]
        [HttpGet]
        public IActionResult GetPendingPosts()
        {
            var result = _apiPostService.GetPendingPost();
            if (result is null || result.Count<ApiQueryModel>() < 1)
            {
                return NotFound(Constants.NotPending);
            }
            else
            {
                return Ok(result);
            }
        }

        [Route("ApprovePost")]
        [HttpPut()]
        public IActionResult ApprovePost([FromBody]ApiUpdatePostModel updatePostModel)
        {
            if (updatePostModel == null)
            {
                return BadRequest(Constants.PostNull);
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(Constants.InvalidPost);
            }
            string result = _apiPostService.ApprovePostbyId(updatePostModel);
            if (result == Constants.ResultOk)
            {
                return Ok();
            }
            else
            {
                return NotFound(result);
            }
        }
    }
}