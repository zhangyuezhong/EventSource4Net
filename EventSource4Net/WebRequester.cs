using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace EventSource4Net
{
    class WebRequester : IWebRequester
    {
        public WebRequest WebRequest { get; set; }
        public Task<IServerResponse> Get(Uri url)
        {
            var wreq = (HttpWebRequest)WebRequest.Create(url);
            wreq.Method = "GET";
            wreq.Proxy = null;
            this.WebRequest = wreq;
            var taskResp = Task.Factory.FromAsync<WebResponse>(wreq.BeginGetResponse,
                                                            wreq.EndGetResponse,
                                                            null).ContinueWith<IServerResponse>(t => new ServerResponse(t.Result),TaskContinuationOptions.NotOnFaulted);
            return taskResp;

        }
    }
}
