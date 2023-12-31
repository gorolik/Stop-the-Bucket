﻿using Sources.Services.Localization;
using Zenject;

namespace Sources.Infrastructure.Installers.GlobalInstallers
{
    public class LocalizationInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindLocalizationStorage();
            BindLocalizationLoader();
            BindLocalizator();
        }

        private void BindLocalizationStorage() =>
            Container.Bind<LocalizationStorage>()
                .AsSingle();

        private void BindLocalizationLoader() =>
            Container.Bind<LocalizationLoader>()
                .AsSingle();

        private void BindLocalizator() =>
            Container.Bind<Localizator>()
                .AsSingle();
    }
}