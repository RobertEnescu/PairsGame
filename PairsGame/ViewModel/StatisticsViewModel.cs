using Newtonsoft.Json;
using PairsGame.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PairsGame.ViewModel
{
    public class StatisticsViewModel : ViewModelBase
    {
        private ObservableCollection<Users> users;
        public ObservableCollection<Users> Users
        {
            get { return users; }
            set
            {
                users = value;
                SetPropertyChanged("Üsers");
            }
        }
        public StatisticsViewModel()
        {
            Users = new ObservableCollection<Users>();
            using (StreamReader r = new StreamReader("users_json.json"))
            {
                string json = r.ReadToEnd();
                if (json != "")
                {
                    var UsersCol = JsonConvert.DeserializeObject<ObservableCollection<Users>>(json);
                    foreach (var item in UsersCol)
                    {
                        Users a = new Users(item.username, item.image, item.gamesPlayed, item.gamesWon);
                        Users.Add(a);
                    }
                }
            }
        }

    }
}
