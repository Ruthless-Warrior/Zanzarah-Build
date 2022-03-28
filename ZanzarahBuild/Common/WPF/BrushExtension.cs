using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Common.Wpf
{
    /*public static class BrushExtension
    {
        public static bool IsEqual(this Brush brush1, Brush brush2)
        {
            if (brush1.GetType() != brush2.GetType())
                return false;
            else
            {
                if (brush1 is SolidColorBrush)
                {
                    return (brush1 as SolidColorBrush).Color ==
                      (brush2 as SolidColorBrush).Color &&
                      (brush1 as SolidColorBrush).Opacity == (brush2 as SolidColorBrush).Opacity;
                }
                else if (brush1 is LinearGradientBrush)
                {
                    bool result = true;
                    result = (brush1 as LinearGradientBrush).ColorInterpolationMode ==
                      (brush2 as LinearGradientBrush).ColorInterpolationMode && result;
                    result = (brush1 as LinearGradientBrush).EndPoint ==
                      (brush2 as LinearGradientBrush).EndPoint && result;
                    result = (brush1 as LinearGradientBrush).MappingMode ==
                      (brush2 as LinearGradientBrush).MappingMode && result;
                    result = (brush1 as LinearGradientBrush).Opacity ==
                      (brush2 as LinearGradientBrush).Opacity && result;
                    result = (brush1 as LinearGradientBrush).StartPoint ==
                      (brush2 as LinearGradientBrush).StartPoint && result;
                    result = (brush1 as LinearGradientBrush).SpreadMethod ==
                      (brush2 as LinearGradientBrush).SpreadMethod && result;
                    result = (brush1 as LinearGradientBrush).GradientStops.Count ==
                      (brush2 as LinearGradientBrush).GradientStops.Count && result;
                    if (result && (brush1 as LinearGradientBrush).GradientStops.Count ==
                              (brush2 as LinearGradientBrush).GradientStops.Count)
                    {
                        for (int i = 0; i < (brush1 as LinearGradientBrush).GradientStops.Count; i++)
                        {
                            result = (brush1 as LinearGradientBrush).GradientStops[i].Color ==
                              (brush2 as LinearGradientBrush).GradientStops[i].Color && result;
                            result = (brush1 as LinearGradientBrush).GradientStops[i].Offset ==
                              (brush2 as LinearGradientBrush).GradientStops[i].Offset && result;
                            if (!result)
                                return result;
                        }
                    }
                    return result;
                }
                else if (brush1 is RadialGradientBrush)
                {
                    bool result = true;
                    result = (brush1 as RadialGradientBrush).ColorInterpolationMode ==
                                 (brush2 as RadialGradientBrush).ColorInterpolationMode && result;
                    result = (brush1 as RadialGradientBrush).GradientOrigin ==
                                (brush2 as RadialGradientBrush).GradientOrigin && result;
                    result = (brush1 as RadialGradientBrush).MappingMode == (brush2 as RadialGradientBrush).MappingMode && result;
                    result = (brush1 as RadialGradientBrush).Opacity == (brush2 as RadialGradientBrush).Opacity && result;
                    result = (brush1 as RadialGradientBrush).RadiusX == (brush2 as RadialGradientBrush).RadiusX && result;
                    result = (brush1 as RadialGradientBrush).RadiusY == (brush2 as RadialGradientBrush).RadiusY && result;
                    result = (brush1 as RadialGradientBrush).SpreadMethod == (brush2 as RadialGradientBrush).SpreadMethod && result;
                    result = (brush1 as RadialGradientBrush).GradientStops.Count == (brush2 as RadialGradientBrush).GradientStops.Count && result;
                    if (result && (brush1 as RadialGradientBrush).GradientStops.Count == (brush2 as RadialGradientBrush).GradientStops.Count)
                    {
                        for (int i = 0; i < (brush1 as RadialGradientBrush).GradientStops.Count; i++)
                        {
                            result = (brush1 as RadialGradientBrush).GradientStops[i].Color ==
                                          (brush2 as RadialGradientBrush).GradientStops[i].Color && result;
                            result = (brush1 as RadialGradientBrush).GradientStops[i].Offset ==
                                              (brush2 as RadialGradientBrush).GradientStops[i].Offset && result;
                            if (!result)
                                return result;
                        }
                    }
                    return result;
                }
                else if (brush1 is ImageBrush)
                {
                    bool result = true;
                    result = (brush1 as ImageBrush).AlignmentX == (brush2 as ImageBrush).AlignmentX && result;
                    result = (brush1 as ImageBrush).AlignmentY == (brush2 as ImageBrush).AlignmentY && result;
                    result = (brush1 as ImageBrush).Opacity == (brush2 as ImageBrush).Opacity && result;
                    result = (brush1 as ImageBrush).Stretch == (brush2 as ImageBrush).Stretch && result;
                    result = (brush1 as ImageBrush).TileMode == (brush2 as ImageBrush).TileMode && result;
                    result = (brush1 as ImageBrush).Viewbox == (brush2 as ImageBrush).Viewbox && result;
                    result = (brush1 as ImageBrush).ViewboxUnits == (brush2 as ImageBrush).ViewboxUnits && result;
                    result = (brush1 as ImageBrush).Viewport == (brush2 as ImageBrush).Viewport && result;
                    result = (brush1 as ImageBrush).ViewportUnits == (brush2 as ImageBrush).ViewportUnits && result;

                    result = (brush1 as ImageBrush).ImageSource == (brush2 as ImageBrush).ImageSource && result;
                    return result;
                }
            }
            return false;
        }
    }*/
}
