﻿#if __WPF__
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Animation;
#else
using System.Collections.Generic;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;
#endif

namespace XamlFlair.Extensions
{
	internal static class AnimationSettingsExtensions
	{
#if !__UWP__
		internal static EasingFunctionBase GetEase(this AnimationSettings settings)
		{
			EasingFunctionBase ease;

			switch (settings.Easing)
			{
				case EasingType.Back:
				ease = new BackEase();
				break;

				case EasingType.Bounce:
				ease = new BounceEase();
				break;

#if !HAS_UNO
				// Circle easing not supported in Uno
				case EasingType.Circle:
				ease = new CircleEase();
				break;
#endif

				case EasingType.Cubic:
				ease = new CubicEase();
				break;

				case EasingType.Elastic:
				ease = new ElasticEase();
				break;

				case EasingType.Linear:
				ease = null;
				break;

				case EasingType.Quadratic:
				ease = new QuadraticEase();
				break;

				case EasingType.Quartic:
				ease = new QuarticEase();
				break;

				case EasingType.Quintic:
				ease = new QuinticEase();
				break;

				case EasingType.Sine:
				ease = new SineEase();
				break;

				default:
				ease = new CubicEase();
				break;
			}

			if (ease != null)
			{
				ease.EasingMode = settings.EasingMode;
			}

			return ease;
		}
#endif

		internal static AnimationSettings ApplyOverrides(this AnimationSettings settings, AnimationSettings other)
		{
			var updated = new AnimationSettings();

			var kind = other.Kind;
			updated.Kind = kind != DefaultSettings.Kind ? kind : settings.Kind;

			var duration = other.Duration;
			updated.Duration = duration != DefaultSettings.Duration ? duration : settings.Duration;

			var delay = other.Delay;
			updated.Delay = delay != 0 ? delay : settings.Delay;

			var opacity = other.Opacity;
			updated.Opacity = opacity != 1 ? opacity : settings.Opacity;

			var offsetX = other.OffsetX;
			updated.OffsetX = offsetX != Offset.Empty ? offsetX : settings.OffsetX;

			var offsetY = other.OffsetY;
			updated.OffsetY = offsetY != Offset.Empty ? offsetY : settings.OffsetY;

			var rotation = other.Rotation;
			updated.Rotation = rotation != 0 ? rotation : settings.Rotation;

// ColorAnimation supported only on Uno and WPF (not on native UWP due to Composition-only implementations)
#if WINDOWS_UWP || HAS_UNO || __WPF__
			var color = other.Color;
			updated.Color = color != DefaultSettings.Color ? color : settings.Color;

			var colorTarget = other.ColorOn;
			updated.ColorOn = colorTarget != DefaultSettings.ColorOn ? colorTarget : settings.ColorOn;
#endif
			// Blur not supported on Uno
#if !HAS_UNO
			var blur = other.BlurRadius;
			updated.BlurRadius = blur != 0 ? blur : settings.BlurRadius;
#endif

			var scaleX = other.ScaleX;
			updated.ScaleX = scaleX != 1 ? scaleX : settings.ScaleX;

			var scaleY = other.ScaleY;
			updated.ScaleY = scaleY != 1 ? scaleY : settings.ScaleY;

			var origin = other.TransformCenterPoint;
			updated.TransformCenterPoint = origin != DefaultSettings.TransformCenterPoint ? origin : settings.TransformCenterPoint;

#if __WPF__
			var transformOn = other.TransformOn;
			updated.TransformOn = transformOn != DefaultSettings.TransformOn ? transformOn : settings.TransformOn;
#endif

			var easingMode = other.EasingMode;
			updated.EasingMode = easingMode != DefaultSettings.Mode ? easingMode : settings.EasingMode;

			var easing = other.Easing;
			updated.Easing = easing != DefaultSettings.Easing ? easing : settings.Easing;

			var @event = other.Event;
			updated.Event = @event != DefaultSettings.Event ? @event : settings.Event;

#if __UWP__
			var offsetZ = other.OffsetZ;
			updated.OffsetZ = offsetZ != 0 ? offsetZ : settings.OffsetZ;

			var scaleZ = other.ScaleZ;
			updated.ScaleZ = scaleZ != 1 ? scaleZ : settings.ScaleZ;

			var saturation = other.Saturation;
			updated.Saturation = saturation != DefaultSettings.Saturation ? saturation : settings.Saturation;

			var tint = other.Tint;
			updated.Tint = tint != DefaultSettings.Tint ? tint : settings.Tint;
#endif

			return updated;
		}

		internal static List<AnimationSettings> ToSettingsList(this IAnimationSettings settings)
		{
			var animations = new List<AnimationSettings>();

			if (settings is CompoundSettings compound)
			{
				animations.AddRange(compound.Sequence);
			}
			else if (settings is AnimationSettings anim)
			{
				animations.Add(anim);
			}

			return animations;
		}
	}
}