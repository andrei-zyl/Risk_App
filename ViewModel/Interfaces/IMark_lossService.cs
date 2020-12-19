using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Models;


namespace ViewModel.Interfaces
{
    public interface IMark_lossService
    {
        ObservableCollection<Mark_lossViewModel> GetAll();
        Mark_lossViewModel Get(int id);
        void AddIncidentToMark_loss(int Mark_lossId, IncidentViewModel incident);
        void RemoveIncidentFromMark_loss(int Mark_lossId, int incidentId);
        void CreateMark_loss(Mark_lossViewModel Mark_loss);
        void DeleteMark_loss(int Mark_lossId);
        void UpdateMark_loss(Mark_lossViewModel Mark_loss);
    }
}
