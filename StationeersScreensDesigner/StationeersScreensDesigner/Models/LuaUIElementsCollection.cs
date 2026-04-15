using System;
using System.Collections.Generic;
using System.Text;

namespace StationeersScreensDesigner.Models
{
    public class LuaUIElementsCollection
    {
        public static List<LuaUIElement> All { get; } = new()
        {
            new LuaLabel() { Content = "Placeholder", FontSize = 16, Align="left",Color="#FFFFFF",Width=100,Height=20, ZIndex=3},
            new LuaPanel(){Background="#FFFFFF" , Width=10, Height=10},
            new LuaProgressBar(){Background="#FFFFFF",Width=100,Height=10, FillColor="#FF0000", MaxValue = 1, Speed = 1, ZIndex = 2},
            new LuaLine() {Color="#000000",Width=10, Height=10,Thickness=2,X1=10,X2=30,Y1=10,Y2=10},
            new LuaIcon(){ColorIndex=0,Width=100,Height=100,IconName="ss.ui.icons.gas.Oxygen",Tint="#FFFFFF"},
            new LuaButton(){Text="Click me!", Width=80,Height=40,TextColor="#000000",Background="#FFFFFF",FontSize=12},
            new LuaGauge(){Width=200,Height=200,ArcThickness=10,Label="Value",LabelTextColor="#000000",Background="#FFFFFF",FontSize=12,
                Invert = false,MaxValue=1,MinValue=0,NeedleColor="#960d06",DangerColor="#f2771f",Danger=0.8,Warn=0.4,WarnColor="#f2e01f",
            ArcBorderColor="#000000",NormalColor="#9af21f",Unit="",Value=0.5,ValueTextColor="#000000"},
            new LuaRadioButton(){Width=80,Height=40, Background ="#FFFFFF",Checked=false,Text="Radio",GroupID=0,
                TextColor="#000000",CheckColor="#00FF00",FontSize=12,X=10,Y=10},
            new LuaCheckbox(){Width=80,Height=40, Background ="#FFFFFF",Checked=false,Text="Check",
                TextColor="#000000",CheckColor="#00FF00",FontSize=12,X=10,Y=10},
            new LuaToggle(){Width=80,Height=40,OnColor="#00FF00", OffColor="#ABABAB", KnobColor="#FFFFFF" },

        };
    }
}
