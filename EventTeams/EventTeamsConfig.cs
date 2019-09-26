using System;
using System.Collections.ObjectModel;
using Torch;

namespace EventTeams
{
    public class EventTeamsConfig : ViewModel
    {
        private ObservableCollection<String> _FactionTags;

        public ObservableCollection<string> FactionTags
        {
            get { return _FactionTags ?? (_FactionTags = new ObservableCollection<string>()); }
            set { _FactionTags = value; }
        }

    }
}
