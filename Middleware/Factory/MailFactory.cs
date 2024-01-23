﻿using Middleware.Interfaces;
using Middleware.Services;

namespace Middleware.Factory
{
    public class MailFactory
    {
        private readonly IServiceProvider serviceProvider;

        public MailFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public IMailService GetMailService(string userSelection)
        {
            if (userSelection == "localMail")
                return (IMailService)serviceProvider.GetService(typeof(LocalMailService));

            return (IMailService)serviceProvider.GetService(typeof(CloudMailService));
        }
    }
}