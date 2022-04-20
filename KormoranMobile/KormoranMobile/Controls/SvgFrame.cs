using System;
using System.Diagnostics;
using System.IO;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using SKSvg = SkiaSharp.Extended.Svg.SKSvg;

namespace KormoranMobile.Controls
{
	public class SvgFrame : Frame
	{
		private readonly SKCanvasView _canvas = new SKCanvasView();
		
		public static readonly BindableProperty ResourceIdProperty = BindableProperty.Create(
			nameof(ResourceId), 
			typeof(string), 
			typeof(SvgFrame), 
			default(string), propertyChanged: RedrawCanvas);

		public string ResourceId
		{
			get => (string)GetValue(ResourceIdProperty);
			set => SetValue(ResourceIdProperty, value);
		}
		
		public SvgFrame()
		{
			Padding = new Thickness(0);
			BackgroundColor = Color.Transparent;
			HasShadow = false;
			Content = _canvas;
			_canvas.PaintSurface += CanvasViewOnPaintSurface;
		}
		
		private static void RedrawCanvas(BindableObject bindable, object oldvalue, object newvalue)
		{
			SvgFrame svgIcon = bindable as SvgFrame;
			svgIcon?._canvas.InvalidateSurface();
		}

		private void CanvasViewOnPaintSurface(object sender, SKPaintSurfaceEventArgs args)
		{
			SKCanvas canvas = args.Surface.Canvas;
			canvas.Clear();

			if (string.IsNullOrEmpty(ResourceId))
				return;

			string resource = $"KormoranMobile.Resources.Svg.{ResourceId}";
			using (Stream stream = GetType().Assembly.GetManifestResourceStream(resource))
			{
				var svg = new SKSvg();
				svg.Load(stream);

				var info = args.Info;
				var bounds = svg.Picture.CullRect;
				float ratioX = info.Width / bounds.Width,
					ratioY = info.Height / bounds.Height;
				Debug.WriteLine($"{ratioX} - {ratioY}");
				var matrix = SKMatrix.CreateScale(ratioX, ratioY);
				canvas.DrawPicture(svg.Picture, ref matrix);
			}
		}
	}
}