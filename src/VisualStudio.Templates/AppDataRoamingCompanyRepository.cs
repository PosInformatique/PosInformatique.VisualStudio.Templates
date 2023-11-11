//-----------------------------------------------------------------------
// <copyright file="AppDataRoamingCompanyRepository.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.VisualStudio.Templates
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text.Json;
    using System.Text.Json.Serialization;

    internal class AppDataRoamingCompanyRepository : ISolutionRepository
    {
        private readonly string settingsFilePath;

        public AppDataRoamingCompanyRepository()
        {
            this.settingsFilePath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                @"P.O.S Informatique\Visual Studio\Templates\Companies.json");
        }

        public IReadOnlyList<Solution> GetAll()
        {
            this.CreateDirectoryIfNotExists();

            if (!File.Exists(this.settingsFilePath))
            {
                return Array.Empty<Solution>();
            }

            using (var fileStream = File.OpenRead(this.settingsFilePath))
            {
                return JsonSerializer.Deserialize<Solution[]>(fileStream);
            }
        }

        public void Save(Solution solution)
        {
            this.CreateDirectoryIfNotExists();

            // Remove existings solution in the companies files.
            var existingSolutions = this.GetAll().ToList();
            existingSolutions.RemoveAll(s => s.Path.Equals(solution.Path, StringComparison.CurrentCultureIgnoreCase));

            existingSolutions.Add(solution);

            using (var fileStream = File.Create(this.settingsFilePath))
            {
                JsonSerializer.Serialize(fileStream, existingSolutions);
            }
        }

        private void CreateDirectoryIfNotExists()
        {
            var folder = Path.GetDirectoryName(this.settingsFilePath);

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
        }
    }
}
