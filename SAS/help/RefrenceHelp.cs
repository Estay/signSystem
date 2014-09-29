using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SAS.EstayMobileService;
namespace SAS.help
{
    public class RefrenceHelp
    {
        string u=string.Empty,p=string.Empty;
        public RefrenceHelp()
        {
            u=help.StringHelper.appSettings("WCFUserName");
            p=help.StringHelper.appSettings("WCFPassWord");
        }
        //public EstayMobileServiceTest.MobileContractClient GetMobileContractClient()
        //{
        //    EstayMobileServiceTest.MobileContractClient client = new EstayMobileServiceTest.MobileContractClient();
        //    client.ClientCredentials.UserName.UserName =u ;
        //    client.ClientCredentials.UserName.Password = p;
        //    return client;
        //}
        public EstayMobileService.MobileContractClient GetMobileContractClientTest()
        {
            EstayMobileService.MobileContractClient client = new EstayMobileService.MobileContractClient();
            client.ClientCredentials.UserName.UserName = u;
            client.ClientCredentials.UserName.Password = p;
            return client;
        }
        
    }
}