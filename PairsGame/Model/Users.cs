using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PairsGame.Model
{
    public class Users : IEquatable
    {
        public Users()
        {
        }

        public Users(string username, string image, int gamesPlayed, int gamesWon)
        {
            this.username=username;
            this.image=image;
            this.gamesPlayed=gamesPlayed;
            this.gamesWon=gamesWon;
        }

        public string username { get; set; }
        public string image { get; set; }

        public int gamesPlayed { get; set; }
        public int gamesWon { get; set; }
    }
}
