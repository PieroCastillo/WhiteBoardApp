using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.LogicalTree;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Rendering.SceneGraph;
using Avalonia.Skia;
using Avalonia.Threading;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WhiteBoardApp
{
    enum FrameState
    {
        Nothing,
        Draw,
        Erase
    }

    public partial class WhiteBoard : Control
    {
        bool isDrawing = false;
        FrameState state;
        WriteableBitmap bitmap;
        SKSurface surface;
        SKCanvas canvas;
        Point lastPoint;

        public override void EndInit()
        {
            base.EndInit();

            bitmap = new(new PixelSize((int)Width, (int)Height), new Vector(96, 96), PixelFormat.Rgba8888, AlphaFormat.Premul);
            using(var lockedBitmap = bitmap.Lock())
            {
                var info = new SKImageInfo(lockedBitmap.Size.Width, lockedBitmap.Size.Height, lockedBitmap.Format.ToSkColorType());

                surface = SKSurface.Create(info, lockedBitmap.Address, lockedBitmap.RowBytes);
                canvas = surface.Canvas;
                canvas.Clear(SKColors.White);
            }
        }

        protected override void OnPointerPressed(PointerPressedEventArgs e)
        {
            base.OnPointerPressed(e);

            state = IsErasing ? FrameState.Erase : FrameState.Draw;
            lastPoint = e.GetCurrentPoint(this).Position;
            e.Handled = true;
        }

        protected override void OnPointerMoved(PointerEventArgs e)
        {
            base.OnPointerMoved(e);
            var currentPoint = e.GetCurrentPoint(this);

            switch (state)
            {
                case FrameState.Draw:
                    using (var paint = new SKPaint())
                    {
                        paint.Shader = SKShader.CreateColor(SKColors.Gray);
                        paint.StrokeWidth = 10;
                        paint.IsAntialias = true;
                        paint.StrokeCap = SKStrokeCap.Round;
                        paint.StrokeJoin = SKStrokeJoin.Round;
                        canvas.DrawLine(lastPoint.ToSKPoint(), currentPoint.Position.ToSKPoint(), paint);
                    }
                    break;

                case FrameState.Erase:
                    using (var paint = new SKPaint())
                    {
                        paint.Shader = SKShader.CreateColor(SKColors.White);
                        canvas.DrawCircle(currentPoint.Position.ToSKPoint(), 5, paint);
                    }
                    break;
            }
            lastPoint = currentPoint.Position;
            Dispatcher.UIThread.InvokeAsync(InvalidateVisual, DispatcherPriority.Background);
            e.Handled = true;
        }

        protected override void OnPointerReleased(PointerReleasedEventArgs e)
        {
            base.OnPointerReleased(e);
            var currentPoint = e.GetCurrentPoint(this);
            state = FrameState.Nothing;

            Dispatcher.UIThread.InvokeAsync(InvalidateVisual, DispatcherPriority.Background);
            e.Handled = true;
        }

        public override void Render(DrawingContext context)
        {
            context.DrawImage(bitmap, new Rect(0, 0, Bounds.Width, Bounds.Height));
            Dispatcher.UIThread.InvokeAsync(InvalidateVisual, DispatcherPriority.Background);
        }
    }
}
