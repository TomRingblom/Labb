using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Text.RegularExpressions;

namespace Labb.Infrastructure.Display
{
	public class FeatureViewLocationExpander : IViewLocationExpander
	{
		private const string ChildFeature = "childFeature";
		private const string Feature = "feature";
		private const string ComponentsViewPath = "componentsViewPath";

		private readonly List<string> _viewLocationFormats = new List<string>()
		{
			"/Features/{3}/{1}/{0}.cshtml",
			"/Features/{3}/{0}.cshtml",
			"/Features/{3}/{4}/{1}/{0}.cshtml",
			"/Features/{3}/{4}/{0}.cshtml",
			"/Features/Shared/Views/{1}/{0}.cshtml",
			"/Features/Shared/Views/{0}.cshtml",
			"/Features/Shared/{0}.cshtml",
			"/FormsViews/Views/ElementBlocks/{0}.cshtml"
		};

		public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context,
			IEnumerable<string> viewLocations)
		{
			if (context == null)
			{
				throw new ArgumentNullException(nameof(context));
			}

			if (viewLocations == null)
			{
				throw new ArgumentNullException(nameof(viewLocations));
			}

			if (context.Values.ContainsKey(ComponentsViewPath))
			{
				/* Parameters:
                 * {2} - Area Name
                 * {1} - Controller Name
                 * {0} - View Name
                 * */
				List<string> Paths = new List<string> { 
                    // Handles Custom rendered views
                    "/{0}.cshtml"
				};
				// Generate full View Paths with custom View Name and Components Name
				var CombinedPaths = new List<string>(Paths.Select(x => string.Format(x, context.Values[ComponentsViewPath])));
				// Add in original paths for backward compatibility
				foreach (var path in CombinedPaths)
				{
					yield return path;
				}
			}

			var controllerActionDescriptor = context.ActionContext.ActionDescriptor as ControllerActionDescriptor;
			if (controllerActionDescriptor != null && controllerActionDescriptor.Properties.ContainsKey(Feature))
			{
				var featureName = controllerActionDescriptor.Properties[Feature] as string;
				string childFeatureName = null;
				if (controllerActionDescriptor.Properties.ContainsKey(ChildFeature))
				{
					childFeatureName = controllerActionDescriptor.Properties[ChildFeature] as string;
				}
				var expandedViewLocations = ExpandViewLocations(_viewLocationFormats, featureName, childFeatureName);
				foreach (var item in expandedViewLocations)
				{
					yield return item;
				}
			}

			foreach (var location in viewLocations)
			{
				yield return location;
			}
		}

		public void PopulateValues(ViewLocationExpanderContext context)
		{
			var controllerActionDescriptor = context.ActionContext?.ActionDescriptor as ControllerActionDescriptor;
			if (controllerActionDescriptor == null || !controllerActionDescriptor.Properties.ContainsKey(Feature))
			{
				return;
			}
			context.Values[Feature] = controllerActionDescriptor?.Properties[Feature].ToString();

			if (controllerActionDescriptor.Properties.ContainsKey(ChildFeature))
			{
				context.Values[ChildFeature] = controllerActionDescriptor?.Properties[ChildFeature].ToString();
			}

			Regex DefaultComponentDetector = new Regex(@"^((?:[Cc]omponents))+\/+([\w\.]+)\/+(.*)");
			/*
           * If successful, 
           * Group 0 = FullMatch (ex "Components/MyComponent/Default")
           * Group 1 = Components (ex "Component")
           * Group 2 = Component Name (ex "MyComponent")
           * Group 3 = View Name (ex "Default")
           * */
			var DefaultComponentMatch = DefaultComponentDetector.Match(context.ViewName);

			if (DefaultComponentMatch.Success)
			{
				// Will render Components/ComponentName as the new view name
				context.Values[ComponentsViewPath] = string.Format("Features/{0}/{1}", DefaultComponentMatch.Groups[2].Value, DefaultComponentMatch.Groups[3].Value);
			}
		}

		private IEnumerable<string> ExpandViewLocations(IEnumerable<string> viewLocations,
			string featureName,
			string childFeatureName)
		{
			foreach (var location in viewLocations)
			{
				var updatedLocation = location.Replace("{3}", featureName);
				if (location.Contains("{4}") && string.IsNullOrEmpty(childFeatureName))
				{
					continue;
				}
				else
				{
					updatedLocation = updatedLocation.Replace("{4}", childFeatureName);
				}
				yield return updatedLocation;
			}
		}

	}
}
