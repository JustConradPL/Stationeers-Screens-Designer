using System;
using System.Collections.Generic;
using System.Text;

namespace StationeersScreensDesigner.Persistance
{
    public class ButtonDto : ElementDto
    {
        public string Text { get; set; }
        public string Background { get; set; }
        public string TextColor { get; set; }
        public double FontSize { get; set; }
    }

    public class CheckboxDto : ElementDto
    {
        public string Text { get; set; }
        public string Background { get; set; }
        public string CheckColor { get; set; }
        public string TextColor { get; set; }
        public double FontSize { get; set; }
        public bool Checked { get; set; }
    }

    public class GaugeDto : ElementDto
    {
        public double Value { get; set; }
        public double MinValue { get; set; }
        public double MaxValue { get; set; }
        public double Warn { get; set; }
        public double Danger { get; set; }
        public bool Invert { get; set; }
        public string Label { get; set; }
        public string Unit { get; set; }
        public string Background { get; set; }
        public string ArcBorderColor { get; set; }
        public string NeedleColor { get; set; }
        public string NormalColor { get; set; }
        public string WarnColor { get; set; }
        public string DangerColor { get; set; }
        public string ValueTextColor { get; set; }
        public string LabelTextColor { get; set; }
        public double ArcThickness { get; set; }
        public double FontSize { get; set; }
    }

    public class IconDto : ElementDto
    {
        public string IconName { get; set; }
        public int ColorIndex { get; set; }
        public string Tint { get; set; }
    }

    public class LabelDto : ElementDto
    {
        public string Content { get; set; }
        public int FontSize { get; set; }
        public string Color { get; set; }
        public string Align { get; set; }
    }

    public class LineDto : ElementDto
    {
        public double X1 { get; set; }
        public double X2 { get; set; }
        public double Y1 { get; set; }
        public double Y2 { get; set; }
        public string Color { get; set; }
        public double Thickness { get; set; }
    }

    public class PanelDto : ElementDto
    {
        public string Background { get; set; }
    }

    public class ProgressBarDto : ElementDto
    {
        public double Value { get; set; }
        public double MaxValue { get; set; }
        public double MinValue { get; set; }
        public bool Indeterminate { get; set; }
        public string Background { get; set; }
        public string FillColor { get; set; }
        public double Speed { get; set; }
    }

    public class RadioButtonDto : ElementDto
    {
        public string Text { get; set; }
        public string Background { get; set; }
        public string CheckColor { get; set; }
        public string TextColor { get; set; }
        public double FontSize { get; set; }
        public int GroupID { get; set; }
        public bool Checked { get; set; }
    }

    public class ToggleDto : ElementDto
    {
        public bool Value { get; set; }
        public string OnColor { get; set; }
        public string OffColor { get; set; }
        public string KnobColor { get; set; }
    }
}
