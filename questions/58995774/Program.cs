using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace ConsoleApp51
{
    class Program
    {
        static void Main(string[] args)
        {
            var json = File.ReadAllText("test.json");
            var rootObject = JsonConvert.DeserializeObject<WindowPropertiesM>(json);

            Console.WriteLine(rootObject.GridProperties.First().Value.Properties.Count);
        }
    }


    public class WindowPropertiesM : BaseM
    {
        private Dictionary<string, GridPropertiesM> _gridProperties;
        private string _mdiSize;
        private string _mdiLocation;

        public Dictionary<string, GridPropertiesM> GridProperties
        {
            get { if (_gridProperties == null) _gridProperties = new Dictionary<string, GridPropertiesM>(); return _gridProperties; }
            set { if (_gridProperties != value) { _gridProperties = value; } }
        }

        public string MDISize
        {
            get { return _mdiSize; }
            set { if (_mdiSize != value) { _mdiSize = value; OnPropertyChanged(); } }
        }

        public string MDILocation
        {
            get { return _mdiLocation; }
            set { if (_mdiLocation != value) { _mdiLocation = value; OnPropertyChanged(); } }
        }

        public void OnPropertyChanged() { }
    }



    public class GridPropertiesM : BaseM
    {
        private Dictionary<string, GridPropertyM> _Properties;

        public Dictionary<string, GridPropertyM> Properties
        {
            get { if (_Properties == null) _Properties = new Dictionary<string, GridPropertyM>(); return _Properties; }
            set { if (_Properties != value) { _Properties = value; } }
        }

    }
    public class GridPropertyM : BaseM
    {
        private int? _sortIndex;
        private string _sortOrder;
        private double _width;
        private bool _visible;
        private int _visibleIndex;

        #region Properties
        public Int32? SortIndex
        {
            get { return _sortIndex; }
            set { if (_sortIndex != value) { _sortIndex = value; OnPropertyChanged(); } }
        }
        public string SortOrder
        {
            get { return _sortOrder; }
            set { if (_sortOrder != value) { _sortOrder = value; OnPropertyChanged(); } }
        }
        public double Width
        {
            get { return _width; }
            set { if (_width != value) { _width = value; OnPropertyChanged(); } }
        }
        public Int32 VisibleIndex
        {
            get { return _visibleIndex; }
            set { if (_visibleIndex != value) { _visibleIndex = value; OnPropertyChanged(); } }
        }
        public bool Visible
        {
            get { return _visible; }
            set { if (_visible != value) { _visible = value; OnPropertyChanged(); } }
        }
        public void OnPropertyChanged() { }
        #endregion
    }

    public class BaseM
    {

    }
}
