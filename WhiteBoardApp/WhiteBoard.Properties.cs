using Avalonia;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiteBoardApp
{
    public partial class WhiteBoard
    {

        private double _Thickness = 5;
        public double Thickness
        {
            get => _Thickness;
            set => SetAndRaise(ThicknessProperty, ref _Thickness, value);
        }

        public static readonly DirectProperty<WhiteBoard, double> ThicknessProperty =
            AvaloniaProperty.RegisterDirect<WhiteBoard, double>(nameof(Thickness), o => o.Thickness, (o, v) => o.Thickness = v, 5);


        private int _MiterLimit  = 20;
        public int MiterLimit
        {
            get => _MiterLimit;
            set => SetAndRaise(MiterLimitProperty, ref _MiterLimit, value);
        }

        public static readonly DirectProperty<WhiteBoard, int> MiterLimitProperty =
            AvaloniaProperty.RegisterDirect<WhiteBoard, int>(nameof(MiterLimit), o => o.MiterLimit, (o, v) => o.MiterLimit = v, 10);

        private PenLineCap _LineCap = PenLineCap.Round;
        public PenLineCap LineCap
        {
            get => _LineCap;
            set => SetAndRaise(LineCapProperty, ref _LineCap, value);
        }

        public static readonly DirectProperty<WhiteBoard, PenLineCap> LineCapProperty =
            AvaloniaProperty.RegisterDirect<WhiteBoard, PenLineCap>(nameof(LineCap), o => o.LineCap, (o, v) => o.LineCap = v);

        private PenLineJoin _LineJoin = PenLineJoin.Bevel;
        public PenLineJoin LineJoin
        {
            get => _LineJoin;
            set => SetAndRaise(LineJoinProperty, ref _LineJoin, value);
        }

        public static readonly DirectProperty<WhiteBoard, PenLineJoin> LineJoinProperty =
            AvaloniaProperty.RegisterDirect<WhiteBoard, PenLineJoin>(nameof(LineJoin), o => o.LineJoin, (o, v) => o.LineJoin = v);

        private IDashStyle _LineStyle = DashStyle.DashDotDot;
        public IDashStyle LineStyle
        {
            get => _LineStyle;
            set => SetAndRaise(LineStyleProperty, ref _LineStyle, value);
        }

        public static readonly DirectProperty<WhiteBoard, IDashStyle> LineStyleProperty =
            AvaloniaProperty.RegisterDirect<WhiteBoard, IDashStyle>(nameof(LineStyle), o => o.LineStyle, (o, v) => o.LineStyle = v, DashStyle.DashDotDot);

        private IBrush _Brush = Brushes.Red;
        public IBrush Brush
        {
            get => _Brush;
            set => SetAndRaise(BrushProperty, ref _Brush, value);
        }

        public static readonly DirectProperty<WhiteBoard, IBrush> BrushProperty =
            AvaloniaProperty.RegisterDirect<WhiteBoard, IBrush>(nameof(Brush), o => o.Brush, (o, v) => o.Brush = v, Brushes.Black);

        public IBrush Background
        {
            get => GetValue(BackgroundProperty);
            set => SetValue(BackgroundProperty, value);
        }

        public static readonly StyledProperty<IBrush> BackgroundProperty =
            AvaloniaProperty.Register<WhiteBoard, IBrush>(nameof(Background), Brushes.White);


        public bool IsErasing
        {
            get => GetValue(IsErasingProperty);
            set => SetValue(IsErasingProperty, value);
        }

        public static readonly StyledProperty<bool> IsErasingProperty =
            AvaloniaProperty.Register<WhiteBoard, bool>(nameof(IsErasing), false);


    }
}
