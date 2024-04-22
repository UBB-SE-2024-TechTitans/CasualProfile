using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using District_3_App.ProfileSocialNetworkInfoStuff.ProfileNetworkInfo_Service;
using District_3_App.Statistics;

namespace District_3_App.ExtraInfo
{
    public class ExtraInfoService
    {
        private StatisticsService statisticsService;
        private ProfileNetworkInfoService profileNetworkInfoService;

        public ExtraInfoService(StatisticsService statisticsService, ProfileNetworkInfoService profileNetworkInfoService)
        {
            this.statisticsService = statisticsService;
            this.profileNetworkInfoService = profileNetworkInfoService;
        }
        public StatisticsService GetStatisticsService()
        {
            return this.statisticsService;
        }
        public ProfileNetworkInfoService GetProfileNetworkInfoService()
        {
            return this.profileNetworkInfoService;
        }
    }
}
