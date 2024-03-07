using System.Collections.ObjectModel;

namespace ConsoleApp
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    internal class DataReader
    {
        private IEnumerable<ImportedObject> ImportedObjects { get; set; }
        private Dictionary<(string, string), ICollection<ImportedObject>> ImportedObjectsDict { get; set; }

        public void ImportData(string fileToImport)
        {
            ImportedObjects = new Collection<ImportedObject>();
            ImportedObjectsDict = new Dictionary<(string, string), ICollection<ImportedObject>>();
            
            var streamReader = new StreamReader(fileToImport);
            var columns = streamReader.ReadLine()?.Split(';').Length;
            while (!streamReader.EndOfStream)
            {
                var line = streamReader.ReadLine();
                // prevent null or empty 
                if (string.IsNullOrEmpty(line) || line.Equals(Environment.NewLine)) continue;
                var values = line.Split(';');
                // columns missing value
                if (values.Length != 7) continue;
                
                // clear and correct imported data
                for (var i = 0; i < values.Length; i++)
                {
                    values[i] = values[i].Trim().Replace(" ", "").Replace(Environment.NewLine, "").ToUpper();
                }

                var newObject = CreateObjectFromValues(values);
                ImportedObjects = ImportedObjects.Append(newObject);
                AppendToDictionary(newObject);
                AssignNumberOfChildren();
            }
        }

        public void PrintData()
        {
            foreach (var database in ImportedObjects)
            {
                if (database.Type == "DATABASE")
                {
                    Console.WriteLine($"Database '{database.Name}' ({database.NumberOfChildren} tables)");

                    // print all database's tables
                    foreach (var table in ImportedObjects)
                    {
                        if (table.ParentType.ToUpper() == database.Type)
                        {
                            if (table.ParentName == database.Name)
                            {
                                Console.WriteLine($"\tTable '{table.Schema}.{table.Name}' ({table.NumberOfChildren} columns)");

                                // print all table's columns
                                foreach (var column in ImportedObjects)
                                {
                                    if (column.ParentType.ToUpper() == table.Type)
                                    {
                                        if (column.ParentName == table.Name)
                                        {
                                            Console.WriteLine($"\t\tColumn '{column.Name}' with {column.DataType} data type {(column.IsNullable == "1" ? "accepts nulls" : "with no nulls")}");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private void AppendToDictionary(ImportedObject newObject)
        {
            var newObjectKey = (newObject.ParentType, newObject.ParentName);
            if (ImportedObjectsDict.ContainsKey(newObjectKey))
            {
                ImportedObjectsDict[newObjectKey].Add(newObject);
            }
            else
            {
                ImportedObjectsDict.Add(newObjectKey, new Collection<ImportedObject>{newObject});
            }
        }
        private void AssignNumberOfChildren()
        {
            foreach (var obj in ImportedObjects)
            {
                ImportedObjectsDict.TryGetValue((obj.Type, obj.Name), out var value);
                obj.NumberOfChildren = value?.Count() ?? 0;
            }
        }
        private ImportedObject CreateObjectFromValues(string[] values)
        {
            var newObject = new ImportedObject
            {
                Type = values[0],
                Name = values[1],
                Schema = values[2],
                ParentName = values[3],
                ParentType = values[4],
                DataType = values[5],
                IsNullable = values[6]
            };
            return newObject;
        }
    }

    internal class ImportedObject : ImportedObjectBaseClass
    {
        public string Schema { get; set; }
        public string ParentName { get; set; }
        public string ParentType { get; set; }
        public string DataType { get; set; }
        public string IsNullable { get; set; }
        public double NumberOfChildren { get; set; }
    }

    internal class ImportedObjectBaseClass
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}