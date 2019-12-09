using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIT110Capstone
{
    class Employee
    {
        #region Fields
        private string _name;
        private double _rate;
        private double _hours;
        private DateTime _timeIn;
        #endregion

        #region Properties
        public double Rate
        {
            get { return _rate; }
            set { _rate = value; }
        }
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public double Hours
        {
            get { return _hours; }
            set { _hours = value; }
        }
        public DateTime TimeIn
        {
            get { return _timeIn; }
            set { _timeIn = value; }
        }
        #endregion

        #region Constructors
        public Employee()
        {


        }
        public Employee(string name, double rate, double hours, DateTime timeIn)
        {
            _name = name;
            _rate = rate;
            _hours = hours;
            _timeIn = timeIn;
        }
        #endregion
        

    }
}
