using Lab6_Starter.Model;
using Mapsui;
using Mapsui.Extensions;
using Mapsui.Layers;
using Mapsui.Projections;
using Mapsui.Tiling;
using Mapsui.UI.Maui;
using System.Collections.ObjectModel;
using Map = Mapsui.Map;

namespace Lab6_Starter;

/**
 * Lab 6 Team 5
 * Team members: Alex Robinson, Brady Blocksom, Matthew Steffens, and Zafeer Rahim
 * 
 * Description: A page with a map that can be accessed via a tab on the navigation bar
 * (This file's) Authors: Brady Blocksom and Alex Robinson
 * Date: 10/17/2024
 * 
 * Bugs: While technically fixed by a flag in MainTabbedPage.xaml, the tab bar was eating
 * most of the touch inputs, especially swiping, that the map needs to let you zoom and move
 * it around. That flag consequently disables the swipe interaction to change between tabs,
 * so with the current map we can either have a functional map or swiping to change tabs.
 * While not ideal, a workaround would likely be very confusing and/or messy when swiping
 * to change tabs likely wasn't a super important feature when you can simply click the buttons.
 * 
 * Reflection: This might not look like much, but there was a lot of trial and error with many
 * iterations that went into this lab. Adding a tab gave us some trouble since we weren't
 * immediately sure where the code for it needed to go, but it certainly ended up being valuable
 * practice for getting one to work since it didn't feel very intuitive at the start.
 * Adding a map was a struggle of its own since there are a ton of different options, most
 * requiring payment information even if they seem free or not making much sense in their usage,
 * eventually settling on making a placeholder image until a better alternative could be figured
 * out, which did happen with settling on MapsUI which is *actually* free. All in all, more time
 * went into figuring out how to actually do the work with the tools available to us than is
 * evident from the commits in this pull request.
 * 
 * Each team member helped out at various points during the lab, with Brady doing the initial
 * setup and working through the requirements and procedures for the lab, Alex building off that
 * and adding finishing touches later on, and Matthew and Zafeer providing ideas and effectively
 * being tech support during most of it. While not everyone submitted code, everyone contributed
 * to the progression of the lab by offering ideas, providing insight, and generally helping with
 * getting started with what needed doing during the time we met together for it.
 */
public partial class MapPage : ContentPage
{
	public MapPage()
	{
		InitializeComponent();
        
        // A .net MAUI control that contains a MapsUI map
        MapControl mapControl = new();
        // The MapsUI map from the new control
        Map map = mapControl.Map;
        // A WritableLayer is essentially an overlay for the map that renders special features
        // that you add to it
        WritableLayer pointLayer = new();
        
        // Add a layer to show the map from OpenStreetMap's API
        map.Layers.Add(OpenStreetMap.CreateTileLayer());
        // Place the layer for the points on top of the previous map layer
        map.Layers.Add(pointLayer);
        
        // A point to be placed on the map, with a given longitude and latitude as parameters
        MPoint mpoint = new(-88.4154, 44.2619);
        // The map doesn't use longitude/latitude for placement, so project it onto a new point
        // that can be placed onto the map in the correct location
        MPoint npoint = SphericalMercator.FromLonLat(mpoint.X, mpoint.Y).ToMPoint();
        // Instantly zooms to a given point on the map, with a given resolution for how far to zoom
        // Can optionally give it a duration and easing style for a smoother transition
        map.Home = (n) => n.CenterOnAndZoomTo(npoint, map.Navigator.Resolutions[9]);
        // To zoom to a point after the map is loaded, use map.CenterOnAndZoomTo(); without lambda
        
        // Add a point feature into the WritableLayer from earlier, using the projected MPoint
        // This point will now render on the map which is exactly what we need!
        pointLayer.Add(new PointFeature(npoint));
        // To clear all existing features from the layer, you can use:
        // pointLayer.Clear();

        // You may be able to use PointFeature's Styles property or something similar
        // to change the appearance of the points (default is a white circle)
        
        // Place the map control into MapPage under a grid prepared to contain it
        MapGrid.Add(mapControl);
    }

    private void OnVisitedRadio_Clicked(object sender, CheckedChangedEventArgs e)
    {
        if (!e.Value)
            return;

        // TODO: clear existing points and draw a point for each visited airport
    }

    private void OnUnvisitedRadio_Clicked(object sender, CheckedChangedEventArgs e)
    {
        if (!e.Value)
            return;

        // TODO: clear existing points and draw a point for each unvisited airport
    }

    private void OnBothRadio_Clicked(object sender, CheckedChangedEventArgs e)
    {
        if (!e.Value)
            return;

        // TODO: clear existing points and draw a point for each airport
    }
}