using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Models;

namespace ViewModel.Interfaces
{
    public interface ILossService
    {
        ObservableCollection<LossViewModel> GetAll();
        LossViewModel Get(int id);
        void AddIncidentToLoss(int LossId, IncidentViewModel incident);
        void RemoveIncidentFromLoss(int LossId, int incidentId);
        void CreateLoss(LossViewModel Loss);
        void DeleteLoss(int LossId);
        void UpdateLoss(LossViewModel Loss);
    }
}
