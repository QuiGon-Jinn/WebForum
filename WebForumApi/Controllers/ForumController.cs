using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebForumApi.Database;
using WebForumApi.Database.Models;
using WebForumApi.Models;

namespace WebForumApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ForumController : ControllerBase
    {
        private ForumRepo _forumRepo;

        public ForumController(ForumRepo forumRepo)
        {
            _forumRepo = forumRepo;
        }

        [Route("ViewPosts")]
        [HttpGet]
        [ProducesResponseType(typeof(Post), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<Post>> ViewPosts()
        {
            try
            {
                var result = await _forumRepo.Get();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("CreatePost")]
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(Post), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        public async Task<ActionResult<Post>> CreatePost([FromBody] PostRequestModel postRequest)
        {
            try
            {
                if (string.IsNullOrEmpty(User.Identity?.Name))
                {
                    return Unauthorized("Current user not found");
                }
                else
                {
                    var result = await _forumRepo.Add(User.Identity.Name, postRequest.Text);
                    return Ok(result);
                }
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
