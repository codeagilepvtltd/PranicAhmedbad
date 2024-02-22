using PranicAhmedbad.Lib.Models;
using PranicAhmedbad.Lib.ViewModels;
using System.Collections.Generic;
using System.Data;

namespace PranicAhmedbad.Lib.Repository.Account
{
    public interface IEventRepository
    {
        #region Event Master
        DataSet InsertUpdate_EventMaster(EventMasterViewModel customerMasterViewModel);
        EventMasterViewModel GetEventlist(long CityId = 0);

        #endregion
    }
}
