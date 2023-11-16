using System;
using System.Collections.Generic;
using Unity.Services.Analytics;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using UnityEngine;

namespace Sources.Services.Analytics
{
    public class UnityAnalyticsService : IAnalyticsService
    {
        private const string _levelCompletedFirstEvent = "levelCompletedFirst";
        private const string _levelCompletedEvent = "levelCompleted";
        
        private bool _inted;
        
        public async void Init(AnalyticsEnvironment environment)
        {
            if(_inted)
                return;

            try
            {
                var options = new InitializationOptions();
                options.SetEnvironmentName(environment.ToString());
                await UnityServices.InitializeAsync(options);
                
                AnalyticsService.Instance.StartDataCollection();
                _inted = true;
                
                Debug.Log("Unity analytics inited");
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
        
        public void LevelPassedFirst(int level)
        {
            if(!_inted)
                return;
            
            Dictionary<string, object> data = new Dictionary<string, object> 
            {
                {"Level", level},
            };

            AnalyticsService.Instance.CustomData(_levelCompletedFirstEvent, data);
        }
        
        public void LevelPassed(int level, int stars)
        {
            if(!_inted)
                return;
            
            Dictionary<string, object> data = new Dictionary<string, object> 
            {
                {"Level", level},
                {"Stars", stars},
            };
            
            AnalyticsService.Instance.CustomData(_levelCompletedEvent, data);
        }
    }
}