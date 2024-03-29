﻿using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Tours.API.Dtos;
using Explorer.Tours.API.Public;
using Explorer.Tours.API.Public.Administration;
using Explorer.Tours.Core.UseCases.Administration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Explorer.API.Controllers.Tourist
{
    [Authorize(Policy = "touristPolicy")]
    [Route("api/tourist/reportingIssue")]
    public class ReportingIssueController : BaseApiController
    {
        private readonly IReportingIssueService _reportingIssueService;

        public ReportingIssueController(IReportingIssueService reportingIssueService)
        {
            _reportingIssueService = reportingIssueService;
        }

        [HttpPost("{category}/{description}/{priority}/{tourId}/{touristId}")]

        public ActionResult<ReportedIssueDto> Create(string category, string description, int priority, int tourId, int touristId)
        {
            ReportedIssueDto reportedIssue = new ReportedIssueDto();
            reportedIssue.Category = category;
            reportedIssue.Description = description;
            reportedIssue.Priority = priority;
            reportedIssue.TourId = tourId;
            reportedIssue.TouristId = touristId;
            reportedIssue.Time= DateTime.UtcNow;
            reportedIssue.Deadline = null;
            reportedIssue.Closed = false;
            reportedIssue.Resolved = false;
            reportedIssue.Comments = new List<ReportedIssueCommentDto>();
            reportedIssue.Tour = null;
            if(reportedIssue.Category.IsNullOrEmpty() || reportedIssue.Priority==0 ||reportedIssue.TourId==0 || reportedIssue.TouristId == 0)
            {
                return BadRequest("Fill all the fields.");
            }
            var result = _reportingIssueService.Create(reportedIssue);
            return CreateResponse(result); // na frontu povezati opet sa turom.
        }
        [HttpPut("resolve/{id:int}")]
        public ActionResult<ReportedIssueDto> Resolve(int id)
        {
            var result = _reportingIssueService.Resolve(id);
            return CreateResponse(result);
        }
        [HttpPost("comment/{id:int}")]
        public ActionResult<ReportedIssueDto> AddComment([FromBody] ReportedIssueCommentDto ric, int id)
        {
            var result = _reportingIssueService.AddComment(id,ric);
            return CreateResponse(result);
        }
        [HttpGet("{id:int}")]
        public ActionResult<PagedResult<ReportedIssueDto>> GetAllByTourist(int id, [FromQuery] int page, [FromQuery] int pageSize)
        {
            var result = _reportingIssueService.GetPagedByTourist(id, page, pageSize);
            return CreateResponse(result);
        }
    }
}
