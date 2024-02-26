using PranicAhmedbad.Lib.Models;
using PranicAhmedbad.Lib.ViewModels;
using System.Collections.Generic;
using System.Data;

namespace PranicAhmedbad.Lib.Repository.General
{
    public interface IMasterRepository
    {
        List<Entity_Type_Master> Select_EntityTypeList(int intGlCode);
    }    
}
