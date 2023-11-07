using Sources.Behaviour;
using Sources.Behaviour.UI;
using UnityEngine;
using Zenject;

namespace Sources.Infrastructure.Installers.GlobalInstallers
{
    public class AudioInstaller : MonoInstaller
    {
        [SerializeField] private MusicPlayer _musicPlayerPrefab;
        [SerializeField] private SoundPlayer _soundPlayerPrefab;

        public override void InstallBindings()
        {
            CreateAndBindMusicPlayer();
            CreateAndBindSoundPlayer();
        }

        private void CreateAndBindMusicPlayer()
        {
            MusicPlayer musicPlayer = Container
                .InstantiatePrefab(_musicPlayerPrefab)
                .GetComponent<MusicPlayer>();

            Container.Bind<MusicPlayer>()
                .FromInstance(musicPlayer);
        }

        private void CreateAndBindSoundPlayer()
        {
            SoundPlayer soundPlayer = Container
                .InstantiatePrefab(_soundPlayerPrefab)
                .GetComponent<SoundPlayer>();

            Container.Bind<SoundPlayer>()
                .FromInstance(soundPlayer);
        }
    }
}