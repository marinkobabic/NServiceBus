namespace NServiceBus.Features
{
    using System;
    using NServiceBus.DataBus;

    class DataBusFileBased : Feature
    {
        public DataBusFileBased()
        {
            DependsOn<DataBus>();
        }

        /// <summary>
        /// See <see cref="Feature.Setup"/>
        /// </summary>
        protected internal override void Setup(FeatureConfigurationContext context)
        {
            // We still doing this check here eventhough we now have an explicit API
            // to register custom IDataBus implementations for backwards compatibility
            Type dataBusDefinitionType;
            var customDataBusComponentRegistered = context.Container.HasComponent<IDataBus>();

            if (!context.Settings.TryGet("dataBusDefinitionType", out dataBusDefinitionType) && !customDataBusComponentRegistered)
            {
                string basePath;
                if (!context.Settings.TryGet("FileShareDataBusPath", out basePath))
                {
                    throw new InvalidOperationException("Messages containing databus properties found, please configure a databus using the ConfigureFileShareDataBus.FileShareDataBus extension method for ConfigurationBuilder.");
                }
                var dataBus = new FileShareDataBusImplementation(basePath);

                context.Container.RegisterSingleton<IDataBus>(dataBus);
            }
        }
    }
}
