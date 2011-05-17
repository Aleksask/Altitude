using System;
using Chat.Main.Services;
using Chat.Main.Services.Factories;

namespace Chat.Main.App
{
    public abstract class ChatAppFactoryBase : IChatAppFactory
    {
        public IChatApp CreateChatApp()
        {
            var serviceLocator = new ServiceLocator();
            var app = new ChatApp(serviceLocator);

            OnBeforeRegisterServices(serviceLocator);

            app.ServiceLocator.RegisterService<IUserFactoryService>(CreateUserFactoryService(serviceLocator));
            app.ServiceLocator.RegisterService<ICategoryFactoryService>(CreateCategoryFactoryService(serviceLocator));
            app.ServiceLocator.RegisterService<IMessageFactoryService>(CreateMessageFactoryService(serviceLocator));
            app.ServiceLocator.RegisterService<ILogService>(CreateLogService(serviceLocator));
            app.ServiceLocator.RegisterService<ICategoryService>(CreateCategoryService(serviceLocator));
            app.ServiceLocator.RegisterService<IUserService>(CreateUserService(serviceLocator));
            app.ServiceLocator.RegisterService<IMessageService>(CreateMessageService(serviceLocator));

            OnAfterRegisterServices(serviceLocator);

            return app;
        }

        protected virtual void OnBeforeRegisterServices(IServiceLocator serviceLocator)
        {
        }

        protected virtual void OnAfterRegisterServices(IServiceLocator serviceLocator)
        {
        }

        protected abstract IUserFactoryService CreateUserFactoryService(IServiceLocator serviceLocator);

        protected abstract IMessageFactoryService CreateMessageFactoryService(IServiceLocator serviceLocator);

        protected abstract ICategoryFactoryService CreateCategoryFactoryService(IServiceLocator serviceLocator);

        protected abstract ILogService CreateLogService(IServiceLocator serviceLocator);

        protected abstract ICategoryService CreateCategoryService(IServiceLocator serviceLocator);

        protected abstract IUserService CreateUserService(IServiceLocator serviceLocator);

        protected abstract IMessageService CreateMessageService(IServiceLocator serviceLocator);
    }
}