﻿using Domain;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class NotificationRepository
    {
        DbContextEntities db;
        public NotificationRepository()
        {
            db = new DbContextEntities();
        }

        public IEnumerable<Notification> GetAllNotification()
        {
            return db.Notifications;
        }

        public Notification GetNotificationById(int idNotification)
        {
            return db.Notifications.FirstOrDefault(n => n.idNotification == idNotification);
        }

        public IEnumerable<Notification> GetNotificationByRead(bool read)
        {
            return db.Notifications.Where(n => n.read == read);
        }

        public IEnumerable<Notification> GetNotificationByCow(int idCow)
        {
            return db.Notifications.Where(n => n.lactation.idCow == idCow);
        }

        public void AddNotification(Notification notification)
        {
            db.Notifications.Add(notification);
            db.SaveChanges();
        }

        public void UpdateNotification(Notification notification)
        {
            Notification notificationUpdate = db.Notifications.FirstOrDefault(n=> n.idNotification == notification.idNotification);
            notificationUpdate.description = notification.description;
            notificationUpdate.read = notification.read;
            db.SaveChanges();
        }
    }
}