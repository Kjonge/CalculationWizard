using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationWizard
{
    class ConfigHelper
    {
        public class CalcGroupConfigurationItem
        {
            public string Type { get; set; }
            public string Item { get; set; }
            public string Value { get; set; }
            public string Format { get; set; }
            public int Ordinal { get; set; }
        }

        public class CalcGroupConfigurationItems
        {
            public IList<CalcGroupConfigurationItem> CalcGroupConfiguration { get; set; }
        }

        public static CalcGroupConfigurationItems readCalculationItemsfromFile(string fileName)
        {
            CalcGroupConfigurationItems CalcGroupConfigurationItemList;
            try
            {
                string jsonString = File.ReadAllText(fileName);
                CalcGroupConfigurationItemList = JsonConvert.DeserializeObject<CalcGroupConfigurationItems>(jsonString);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return CalcGroupConfigurationItemList;
        }


        public static string ParseGroupName(CalcGroupConfigurationItems calcItemsConfig)
        {
            string TableName = string.Empty;

            TableName = calcItemsConfig.CalcGroupConfiguration.FirstOrDefault(p => p.Item == "groupname").Value;
            return TableName;
        }

        public static string ParseColumnName(CalcGroupConfigurationItems calcItemsConfig)
        {
            string ColumnName = string.Empty;
            ColumnName = calcItemsConfig.CalcGroupConfiguration.FirstOrDefault(p => p.Item == "columnname").Value;
            return ColumnName;
        }

        public static IList<CalculationGroupItem> ParseCalculatedMembers(CalcGroupConfigurationItems calcItemsConfig)
        {
            int countItems = calcItemsConfig.CalcGroupConfiguration.Count(p => p.Type == "CalculationItem");
            IList<CalculationGroupItem> CalculatedMembers = new List<CalculationGroupItem>();
            foreach (CalcGroupConfigurationItem item in calcItemsConfig.CalcGroupConfiguration)
            {
                if(item.Type == "CalculationItem")
                {
                    CalculationGroupItem calcgroupItem = new CalculationGroupItem();
                    calcgroupItem.Name = item.Item;
                    calcgroupItem.Expression = item.Value;
                    calcgroupItem.FormatString = item.Format;
                    calcgroupItem.Ordinal = item.Ordinal;
                    CalculatedMembers.Add(calcgroupItem);
                }
            }
            return CalculatedMembers;
        }

    }
}
