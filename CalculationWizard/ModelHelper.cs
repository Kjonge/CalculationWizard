using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AnalysisServices.Tabular;
using Newtonsoft.Json;

namespace CalculationWizard
{
    class ModelException : Exception
    {
        public ModelException(string message) : base(message)
        {
        }

    }

    public class CalculationGroupItem
    {
        public string Expression { get; set; }
        public string Name { get; set; }
        public string FormatString { get; set; }
        public int Ordinal { get; set; }
    }

       class ModelHelper
    {
        // 
        // Connect to the local default instance of Analysis Services 
        // 
        private string _ConnectionString;
        private string _Model;
        private string _dateTable;
        private string _dateColumn;
        private string _columnName;
        private string _groupName;
        private int _calcgroups;
        private IList<CalculationGroupItem> _calculationGroupItems;


        public ModelHelper(string ConnectionString, string Model, string GroupName, string ColumnName, IList<CalculationGroupItem> CalculationItems)
        {
            _ConnectionString = ConnectionString;
            _Model = Model;
            _columnName = ColumnName;
            _groupName = GroupName;
            _calculationGroupItems = CalculationItems;
            _calcgroups = 0;
            initModel();
        }

        public bool initModel()
        {
            try
            {
                using (Server server = new Server())
                {
                    server.Connect(_ConnectionString);
                    Model dekstopModel = server.Databases[_Model].Model;
                    //Turn off implicit measures
                    dekstopModel.DiscourageImplicitMeasures = true;

                    //Upgrade the compat level if needed
                    int compatLevel = server.Databases[0].CompatibilityLevel;
                    if (compatLevel < 1470)
                        server.Databases[0].CompatibilityLevel = 1520;

                    bool _hasDatecolumn = false;
                    foreach (Table table in dekstopModel.Tables)
                    {

                        if(table.CalculationGroup != null)
                        {
                            _calcgroups++;
                        }

                        foreach (Column col in table.Columns)
                        {
                            if (col.DataType == DataType.DateTime && col.IsKey == true)
                            {
                                _dateColumn = col.Name;
                                _dateTable = table.Name;
                                _hasDatecolumn = true;
                                break;
                            }
                        }
                    }
                    //No mark as date key
                    if (!_hasDatecolumn)
                    {
                        throw new ModelException("No date column marked as data table.");
                    }
                }

                foreach (CalculationGroupItem _calculationGroupItem in _calculationGroupItems)
                {
                    string value = _calculationGroupItem.Expression;
                    value = value.Replace("<DC>", "'" + _dateTable + "'[" + _dateColumn + "]");
                    value = value.Replace("<CGC>", "'" + _groupName + "'[" + _columnName + "]");
                    _calculationGroupItem.Expression = value;
                }
              
            }
            catch (Exception ex)
            {
                //throw it again
                throw ex;
            }
            return true;

        }

        public void RemoveCalculationGroup()
        {
            try
            {
                using (Server server = new Server())
                {
                    server.Connect(_ConnectionString);
                    Model dekstopModel = server.Databases[_Model].Model;

                    var model = dekstopModel;
                    foreach(Table table in model.Tables)
                    {
                        if(table.Name == _groupName)
                        {
                            model.Tables.Remove(table);
                        }
                    }                 
                    model.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                //throw it again
                throw ex;
            }
        }

       public void AddCalculationGroups()
        {
            try
            {
                using (Server server = new Server())
                {
                    server.Connect(_ConnectionString);
                    Model dekstopModel = server.Databases[_Model].Model;
                    foreach (Table modeltable in dekstopModel.Tables)
                    {

                        //Did we already add a calc group?
                        if (modeltable.Name == _groupName)
                        {
                            throw new ModelException("This model already contains a table with this name.");
                        }
                    }

                        CalculationGroup calculationGroup = new CalculationGroup()
                    {
                        Description = "None",
                        Precedence = _calcgroups,
                    };

                    foreach (CalculationGroupItem _calculationGroupItem in _calculationGroupItems)
                    {

                        CalculationItem calculationItem = new CalculationItem()
                        {
                            Name = _calculationGroupItem.Name,
                            Expression = _calculationGroupItem.Expression,
                            Ordinal = _calculationGroupItem.Ordinal,
                            

                        };

                        if (_calculationGroupItem.FormatString.Length > 0)
                        {
                            calculationItem.FormatStringDefinition = new FormatStringDefinition()
                            {
                                Expression = _calculationGroupItem.FormatString
                            };
                        }

                        calculationGroup.CalculationItems.Add(calculationItem);
                    }

                    Partition partition = new Partition()
                    {
                        Name = "Partition For " + _groupName,
                        Source = new CalculationGroupSource()
                    };

                    Table table = new Table()
                    {
                        Name = _groupName,
                        CalculationGroup = calculationGroup
                    };

                    table.Columns.Add(new DataColumn()
                    {
                        Name = _columnName,
                        DataType = DataType.String
                    });

                    var model = dekstopModel;
                    table.Partitions.Add(partition);
                    model.Tables.Add(table);
                    table.RequestRefresh(RefreshType.Full);
                    model.SaveChanges();
                }       
            }
            catch (Exception ex)
            {
                //throw it again
                throw ex;
            }
        }
    }
}
