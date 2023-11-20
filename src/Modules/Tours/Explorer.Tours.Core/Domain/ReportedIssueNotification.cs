﻿using Explorer.BuildingBlocks.Core.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace Explorer.Tours.Core.Domain
{
    public class ReportedIssueNotification : Entity
    {
        public string Description { get; init; }
        public DateTime CreationTime { get; init; }
        public bool IsRead { get; init; }
        public long UserId { get; init; }
        public long ReportedIssueId { get; init; }
        [ForeignKey("ReportedIssueId")]
        public ReportedIssue? ReportedIssue { get; set; }

        public ReportedIssueNotification() { }
        public ReportedIssueNotification(string description, DateTime creationTime, bool isRead, long userId, long reportedIssueId) 
        {
            if (string.IsNullOrWhiteSpace(description) || userId == 0 || reportedIssueId == 0) throw new ArgumentException("Invalid reported issue notification.");
            Description = description;
            CreationTime = creationTime;
            IsRead = isRead;
            UserId = userId;
            ReportedIssueId = reportedIssueId;
        }
    }
}