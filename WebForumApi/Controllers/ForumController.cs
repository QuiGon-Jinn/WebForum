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
    public class ForumController(ForumRepo forumRepo) : ControllerBase
    {
        private readonly ForumRepo _forumRepo = forumRepo;

        [Route("ViewPosts")]
        [HttpGet]
        [ProducesResponseType(typeof(List<Post>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<List<Post>>> ViewPosts([FromQuery] ViewRequestModel viewRequestModel)
        {
            try
            {
                var result = await _forumRepo.Get
                (
                    viewRequestModel.FromDate, 
                    viewRequestModel.ToDate, 
                    viewRequestModel.Author ?? "", 
                    viewRequestModel.OrderBy ?? "", 
                    viewRequestModel.PageNo, 
                    viewRequestModel.PageSize
                );

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

        [Route("Comment")]
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(Post), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        public async Task<ActionResult<Post>> Comment([FromBody] CommentRequestModel commentRequest)
        {
            try
            {
                if (string.IsNullOrEmpty(User.Identity?.Name))
                {
                    return Unauthorized("Current user not found");
                }
                else
                {
                    var result = await _forumRepo.Comment(commentRequest.PostId, User.Identity.Name, commentRequest.Text);
                    return Ok(result);
                }
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("Like")]
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        public async Task<ActionResult<bool>> Like([FromBody] LikeRequestModel likeRequest)
        {
            try
            {
                if (string.IsNullOrEmpty(User.Identity?.Name))
                {
                    return Unauthorized("Current user not found");
                }
                else
                {
                    var result = await _forumRepo.Like(likeRequest.PostId, User.Identity.Name);
                    return Ok(result);
                }
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("RemoveLike")]
        [HttpPost]
        [Authorize]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        public async Task<ActionResult<bool>> RemoveLike([FromBody] LikeRequestModel likeRequest)
        {
            try
            {
                if (string.IsNullOrEmpty(User.Identity?.Name))
                {
                    return Unauthorized("Current user not found");
                }
                else
                {
                    var result = await _forumRepo.Like(likeRequest.PostId, User.Identity.Name, true);
                    return Ok(result);
                }
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }

        [Route("Tag")]
        [HttpPost]
        [Authorize(Roles = "Moderator")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.Unauthorized)]
        public async Task<ActionResult<Post>> Tag([FromBody] TagRequestModel tagRequest)
        {
            try
            {
                if (string.IsNullOrEmpty(User.Identity?.Name))
                {
                    return Unauthorized("Current user not found");
                }
                else
                {
                    var result = await _forumRepo.Tag(tagRequest.PostId, tagRequest.Text);
                    return Ok(result);
                }
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }
        }
    }
}
