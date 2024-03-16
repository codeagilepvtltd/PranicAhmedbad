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

        List<User_Role_Mapping> TraineeList(int intGlCode = 0,string varRoleName="");
        List<Entity_Type_Master> GetEventTypeList(string varEventType);

        DataSet InsertUpdate_EventSlot(EventSlotDetailViewModel eventslotMasterView);

        #endregion
    }
}
