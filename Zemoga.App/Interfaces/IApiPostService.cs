using System;
using System.Collections.Generic;
using System.Text;

using Zemoga.App.ViewModels;

namespace Zemoga.App.Interfaces
{
    public interface IApiPostService
    {
        IEnumerable<ApiQueryModel> GetPendingPost();

        string ApprovePostbyId(ApiUpdatePostModel post);
    }
}
