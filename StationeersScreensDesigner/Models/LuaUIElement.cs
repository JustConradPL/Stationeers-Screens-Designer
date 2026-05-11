using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace StationeersScreensDesigner.Models
{
    public abstract partial class LuaUIElement(string name, string? id = null) : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        [PropertyMetadata("Name")]
        public string Name
        {
            get; set
            {
                field = value;
                OnPropertyChanged();
            }
        } = name;

        [PropertyMetadata("ID")]
        public string? ID
        {
            get; set
            {
                field = value?.Replace(" ", "_");
                OnPropertyChanged();
            }
        } = id;

        public bool IsSelected
        {
            get; set
            {
                field = value;
                OnPropertyChanged();
            }
        }

        [PropertyMetadata("ZIndex")]
        public int ZIndex
        {
            get; set
            {
                field = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Defines the coordinate mode for this element: "px" or "%"
        /// </summary>
        public string Unit
        {
            get; set
            {
                field = value;
                // Refresh all string representations when the unit mode flips
                RefreshAllStrings();
                OnPropertyChanged();
            }
        } = "px";

        #region Coordinate Properties

        public double X
        {
            get; set
            {
                field = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(XStr));
            }
        }

        [PropertyMetadata("X", "Position")]
        public string XStr
        {
            get => FormatValue(X);
            set => ParseUnitValue(value, v => X = v);
        }

        public double Y
        {
            get; set
            {
                field = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(YStr));
            }
        }

        [PropertyMetadata("Y", "Position")]
        public string YStr
        {
            get => FormatValue(Y);
            set => ParseUnitValue(value, v => Y = v);
        }

        public double Width
        {
            get; set
            {
                field = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(WidthStr));
            }
        }

        [PropertyMetadata("Width", "Size")]
        public string WidthStr
        {
            get => FormatValue(Width);
            set => ParseUnitValue(value, v => Width = v);
        }

        public double Height
        {
            get; set
            {
                field = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HeightStr));
            }
        }

        [PropertyMetadata("Height", "Size")]
        public string HeightStr
        {
            get => FormatValue(Height);
            set => ParseUnitValue(value, v => Height = v);
        }

        #endregion

        public string? InjectedCode { get; set; }
        public Func<string, string>? CodeInterceptor { get; set; }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string FormatValue(double val) => Unit == "%" ? $"{val}%" : $"{val}px";

        private void RefreshAllStrings()
        {
            OnPropertyChanged(nameof(XStr));
            OnPropertyChanged(nameof(YStr));
            OnPropertyChanged(nameof(WidthStr));
            OnPropertyChanged(nameof(HeightStr));
        }

        protected void ParseUnitValue(string input, Action<double> assigner)
        {
            if (string.IsNullOrWhiteSpace(input)) return;

            input = input.Trim().ToLower();
            bool isPercent = input.EndsWith("%");

            string numberPart = input.Replace("%", "").Replace("px", "").Trim();

            if (double.TryParse(numberPart, out double result))
            {
                Unit = isPercent ? "%" : "px";
                assigner(result);
            }
        }

        public abstract string ToLuaCode();
    }
}
