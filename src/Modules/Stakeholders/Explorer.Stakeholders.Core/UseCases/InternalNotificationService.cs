﻿using AutoMapper;
using Explorer.BuildingBlocks.Core.UseCases;
using Explorer.Stakeholders.API.Dtos;
using Explorer.Stakeholders.API.Internal;
using Explorer.Stakeholders.Core.Domain;
using Explorer.Stakeholders.Core.Domain.RepositoryInterfaces;

namespace Explorer.Stakeholders.Core.UseCases
{
    public class InternalNotificationService : BaseService<NotificationDto, Notification>, IInternalNotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public InternalNotificationService(IMapper mapper, INotificationRepository notificationRepository) : base(mapper)
        {
            _notificationRepository = notificationRepository;
        }
        public NotificationDto CreateNotification(string description, long userId)
        {
            try
            {
                NotificationDto notification = MapToDto(_notificationRepository.CreateNotification(description, userId));
                return notification;
            }
            catch (KeyNotFoundException e)
            {
                return null;
            }
        }
        public NotificationDto CreateReportedIssueNotification(string description, long userId, long? foreignId)
        {
            try
            {
                NotificationDto notification = MapToDto(_notificationRepository.CreateReportedIssueNotification(description, userId, foreignId));
                return notification;
            }
            catch (KeyNotFoundException e)
            {
                return null;
            }
        }

        public NotificationDto CreatePaymentNotification(string description, long userId)
        {
            var notification = new Notification(description, userId, null, NotificationType.PAYMENT);
            var result = _notificationRepository.CreatePaymentNotification(notification);
            return MapToDto(result);
        }
    }
}
