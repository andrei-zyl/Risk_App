using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Models;

namespace ViewModel.Interfaces
{
    public interface IMark_frequencyService
    {
        ObservableCollection<Mark_frequencyViewModel> GetAll();
        Mark_frequencyViewModel Get(int id);
        void AddIncidentToMark_frequency(int Mark_frequencyId, IncidentViewModel incident);
        void RemoveIncidentFromMark_frequency(int Mark_frequencyId, int incidentId);
        void CreateMark_frequency(Mark_frequencyViewModel Mark_frequency);
        void DeleteMark_frequency(int Mark_frequencyId);
        void UpdateMark_frequency(Mark_frequencyViewModel Mark_frequency);
    }
}
