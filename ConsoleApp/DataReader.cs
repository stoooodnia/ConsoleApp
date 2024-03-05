using System.Collections.ObjectModel;
using System.Linq;
using System;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp
{
    public class DataReader
    {
        private IEnumerable<ImportedObjectBaseClass> ImportedObjects { get; set; }
        private Dictionary<(string, string), IEnumerable<ImportedObjectBaseClass>> ObjectsDict { get; set; }

        public DataReader()
        {
            ImportedObjects = new Collection<ImportedObjectBaseClass>();
            ObjectsDict = new Dictionary<(string, string), IEnumerable<ImportedObjectBaseClass>>();
        }
       
        public void ImportData(string fileToImport)
        {
            ImportObjectsFromFile(fileToImport);
            // ProcessImportedData();
            // AssignNumberOfChildren();
            // PrintData();
            // Console.ReadLine();
            Console.WriteLine(ImportedObjects);
            Console.WriteLine(ObjectsDict);
        }

        private void PrintData()
        {
            
        }
        private void ImportObjectsFromFile(string fileToImport)
        {
            var streamReader = new StreamReader(fileToImport);
            while (!streamReader.EndOfStream)
            {
                var line = streamReader.ReadLine();
                
                if (string.IsNullOrEmpty(line) || line.Equals(Environment.NewLine)) continue; 
                
                var values = line.Split(';');
                if (values.Length != 7) continue;
                var newObject = new ImportedObject();
                newObject.MapFrom(values);
                ImportedObjects = ImportedObjects.Append(newObject);
                AppendToDictionary(newObject);
            }
        }

        private void AppendToDictionary(ImportedObjectBaseClass newObject)
        {
            var newObjectKey = newObject.GetKey();
            if (ObjectsDict.ContainsKey(newObjectKey))
            {
                ObjectsDict[newObjectKey] = ObjectsDict[newObjectKey].Append(newObject);
            }
            else
            {
                ObjectsDict.Add(newObjectKey, new Collection<ImportedObjectBaseClass>{newObject});
            }
        }
        
        private void AssignNumberOfChildren()
        {
            foreach (var importedObject in ImportedObjects)
            {
                var objKey = importedObject.GetKey();
                importedObject.
            }
            
        //     for (int i = 0; i < ImportedObjects.Count(); i++)
        //     {
        //         var importedObject = ImportedObjects.ToArray()[i];
        //         foreach (var impObj in ImportedObjects)
        //         {
        //             if (impObj.ParentType == importedObject.Type)
        //             {
        //                 if (impObj.ParentName == importedObject.Name)
        //                 {
        //                     importedObject.NumberOfChildren = 1 + importedObject.NumberOfChildren;
        //                 }
        //             }
        //         }
        //     }
        }
        
    }

    public class ImportedObject : ImportedObjectBaseClass
    {
        public string Schema { get; set; }
        public string ParentName { get; set; }
        public string ParentType { get; set; }
        public string DataType { get; set; }
        public string IsNullable { get; set; }
        public double NumberOfChildren { get; set; }

        public override void MapFrom(string[] values)
        {  
            Type = ProcessString(values[0]).ToUpper();
            Name = ProcessString(values[1]);
            Schema = ProcessString(values[2]);
            ParentName = ProcessString(values[3]);
            ParentType = ProcessString(values[4]);
            DataType = ProcessString(values[5]);
            IsNullable = ProcessString(values[6]);
        }
        public override void Print()
        { 
            // foreach (var database in ImportedObjects)
            // {
            //     if (database.Type == "DATABASE")
            //     {
            //         Console.WriteLine($"Database '{database.Name}' ({database.NumberOfChildren} tables)");
            //
            //         // print all database's tables
            //         foreach (var table in ImportedObjects)
            //         {
            //             if (table.ParentType.ToUpper() == database.Type)
            //             {
            //                 if (table.ParentName == database.Name) 
            //                 {
            //                     Console.WriteLine($"\tTable '{table.Schema}.{table.Name}' ({table.NumberOfChildren} columns)");
            //
            //                     // print all table's columns
            //                     foreach (var column in _importedObjects)
            //                     {
            //                         if (column.ParentType.ToUpper() == table.Type)
            //                         {
            //                             if (column.ParentName == table.Name)
            //                             {
            //                                 Console.WriteLine($"\t\tColumn '{column.Name}' with {column.DataType} data type {(column.IsNullable == "1" ? "accepts nulls" : "with no nulls")}");
            //                             }
            //                         }
            //                     }
            //                 }
            //             }
            //         }
            //     }
            // } 
        }

        public override (string, string) GetKey()
        {
            return (ParentType, ParentName);
        }
    }

    public abstract class ImportedObjectBaseClass
    {
        public string Name { get; set; }
        public string Type { get; set; }
        protected static string ProcessString(string stringToProcess)
        {
            // TYPE ToUpper() !!
            return stringToProcess.Trim().Replace(" ", "").Replace(Environment.NewLine, "");
        }
        public abstract void MapFrom(string[] line);
        public abstract void Print();
        public abstract (string, string) GetKey();
    }
}
