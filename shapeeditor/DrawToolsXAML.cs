using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace shapeeditor
{
    partial class DrawTool
    {
        protected static readonly String HatchBrushXaml =
           @"<VisualBrush  
                xmlns='http://schemas.microsoft.com/winfx/2006/xaml/presentation'
                xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'
            TileMode='Tile' Viewport='0,0,10,10'  ViewportUnits='Absolute' Viewbox='0,0,10,10'  ViewboxUnits='Absolute'>
            <VisualBrush.Visual>
                <Canvas>
                    <Rectangle Fill='White' Width='10' Height='10' />
                    <Path Stroke='Red' Data='M 0 0 l 10 10' />
                    <Path Stroke='Red' Data='M 0 10 l 10 -10' />
                </Canvas>
            </VisualBrush.Visual>
        </VisualBrush>";
    }
}
