using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.IO;

using StationeersScreensDesigner.Models;
using StationeersScreensDesigner.ViewModels;

namespace StationeersScreensDesigner.Persistance
{
    public static class FileOperator
    {
        private static ProjectMapper _mapper = new();
        public static void SaveAs(string projectName, List<ScreenViewModel> screens)
        {
            var sfd = new Microsoft.Win32.SaveFileDialog { Filter = "Designer Files (*.json)|*.json" };
            if (sfd.ShowDialog() == true)
            {
                ProjectDto projectDto = _mapper.ProjectToDto(projectName, screens);

                string json = JsonSerializer.Serialize(projectDto, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(sfd.FileName, json);
            }
        }

        public static List<ScreenViewModel> Load()
        {
            List<ScreenViewModel> Screens = new();
            var ofd = new Microsoft.Win32.OpenFileDialog { Filter = "Designer Files (*.json)|*.json" };
            if (ofd.ShowDialog() == true)
            {
                string json = File.ReadAllText(ofd.FileName);
                var projectDto = JsonSerializer.Deserialize<ProjectDto>(json);

                foreach (var sDto in projectDto.Screens)
                {
                    Screens.Add(_mapper.MapToScreenVm(sDto));
                }

            }
                return Screens;
        }
    }
}
