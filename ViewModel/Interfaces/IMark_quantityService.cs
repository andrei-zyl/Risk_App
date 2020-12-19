using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Models;

namespace ViewModel.Interfaces
{
    public interface IMark_quantityService
    {
        ObservableCollection<Mark_quantityViewModel> GetAll();
        Mark_quantityViewModel Get(int id);
        void AddIncidentToMark_quantity(int Mark_quantityId, IncidentViewModel incident);
        void RemoveIncidentFromMark_quantity(int Mark_quantityId, int incidentId);
        void CreateMark_quantity(Mark_quantityViewModel Mark_quantity);
        void DeleteMark_quantity(int Mark_quantityId);
        void UpdateMark_quantity(Mark_quantityViewModel Mark_quantity);
    }
}
