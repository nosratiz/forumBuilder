using System.Threading.Tasks;
using FourmBuilder.Api.Core.Application.Files.Command;
using FourmBuilder.Common.Helper.systemMessage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FourmBuilder.Api.Controllers
{
    public class FileController : BaseController
    {
        /// <summary>
        /// uploading files
        /// </summary>
        /// <remarks>
        /// 
        /// Types can be one of this: Avatar, Image, Video, Music, Document, Other
        /// count validate with [X-MultiSelect] Header can between 1 to 20
        /// 
        /// </remarks>
        /// <param name="createFileCommand"></param>
        /// <returns></returns>
        /// <response code="200">Get Files list Uploaded</response>
        /// <response code="400">If validation failure.</response>
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesErrorResponseType(typeof(ApiMessage))]
        [HttpPost("Upload")]
        [Consumes("multipart/form-data")]
        
        public async Task<IActionResult> Upload([FromForm]CreateFileCommand createFileCommand)
        {
            var result = await Mediator.Send(createFileCommand);

            return result.ApiResult;
        }
    }
}
