﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exceptions;
using GridSystem;
using Newtonsoft.Json;
using UnityEngine;
using System.IO;

namespace Blueprints
{
    public static partial class BlueprintManager
    {
		public class LoadedBlueprints : List<Blueprint>
        {
            private static readonly string savePath;

            static LoadedBlueprints()
            {
                var destinationFolder = System.Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                savePath = Path.Combine(destinationFolder, "loadedblueprints.zetta");
            }

            public static List<Blueprint> SerializeBlueprints()
            {
                try
                {
                    var json = File.ReadAllText(savePath);
                    var unverifiedBlueprints = JsonConvert.DeserializeObject<List<Blueprint>>(json);
                    // Get a list of blueprints that are valid
                    return unverifiedBlueprints.Where(x => x.IsValid).ToList();
                }
                catch
                {
                    // if error, return an empty list
                    return new List<Blueprint>();
                }
            }

            public static void DeserializeBlueprints(LoadedBlueprints blueprints)
            {
                var json = JsonConvert.SerializeObject(blueprints, Formatting.Indented);

                using (var sr = File.CreateText(savePath))
                {
                    sr.WriteLine(json);
                }
            }

            public LoadedBlueprints()
                : base(SerializeBlueprints())
            {
            }

            // save blueprints to file
            ~LoadedBlueprints()
            {
                DeserializeBlueprints(this);
            }

            public Blueprint GetFirstWithName(string name)
            {
                return (from blueprint in this
                        where blueprint.Name == name 
                        select blueprint)
                        .FirstOrDefault();
            }

            // override base functionality to use custom blueprint hashing methods
            private new bool Contains(Blueprint pBlueprint)
            {
                var hash = pBlueprint.GetHashCode();
                foreach (var blueprint in this)
                {
                    if (blueprint.GetHashCode() == hash)
                    {
                        return true;
                    }
                }
                return false;
            }

            // override base functionality to only add unique blueprints
            public new void Add(Blueprint blueprint)
            {
                if (!Contains(blueprint))
                {
                    base.Add(blueprint);
                }
                else
                {
                    Debug.Log("Duplicate ship");
                }
            }
        }

        public static LoadedBlueprints loadedBlueprints = new LoadedBlueprints();

        [RuntimeInitializeOnLoadMethod]
        public static void AddDefaultShipToLoadedBlueprints()
        {
            loadedBlueprints.Add(Import(DEFAULT_BLUEPRINT));
        }
    }
}