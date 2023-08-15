using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PairsGame.Model
{
    public class GameState
    {
        public string username { get; set; }
        public int rows { get; set; }
        public int columns { get; set; }
        public int level { get; set; }
        
        public List<string> defaultPaths { get; set; }
        public List<bool> defaultBools { get; set; }
        public List<string> imageCollection { get; set; }

        public GameState()
        {
        }

        public GameState(string username, int rows, int columns, int level, List<string> defaultPaths, List<bool> defaultBools, List<string> imageCollection)
        {
            this.username=username;
            this.rows=rows;
            this.columns=columns;
            this.level=level;
            this.defaultPaths=defaultPaths;
            this.defaultBools=defaultBools;
            this.imageCollection=imageCollection;
        }
    }
}
