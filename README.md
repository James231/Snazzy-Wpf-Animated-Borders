# Snazzy WPF Animated Borders

[![Donate](https://img.shields.io/badge/Donate-PayPal-green.svg)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=MLD56V6HQWCKU&source=url)

.NET 5 WPF app showing how to animate window borders. Watch the [YouTube video](https://youtu.be/kmBO7lE7pXY) or try the demo app on the [releases page](https://github.com/James231/Snazzy-Wpf-Animated-Borders/releases).

[<img width="640" height="360" src="https://cdn.jam-es.com/img/snazzy-wpf-borders/thumbnail.png">](https://youtu.be/kmBO7lE7pXY)

## How to Use

You'll probably want to edit some of the code to your liking, hence you'll want the code in your project files, so it is not on NuGet I'm afraid.

It's easiest to use this project as a template and add your application code to it. But if you want, you can try to transfer what you need into your own project ...

### Add to your own project

1. Follow instructions to install [ModernWpf](https://github.com/Kinnara/ModernWpf). For .NET 5 that includes opening your `.csproj` project file and changing the `<TargetFramework>` to `net5.0-windows10.0.18362.0`.
2. In you `MainWindow.xaml` enable ModernWpf custom title bar by adding the following to the `<Window>` tag:
```xml
<Window ...
    xmlns:ui="http://schemas.modernwpf.com/2019"
    ui:WindowHelper.UseModernWindowStyle="True"
    ui:TitleBar.ExtendViewIntoTitleBar="True"
    BorderThickness="0">
```
2. Copy over the `Components` and `Extensions` folders. You'll need to change the Xaml and C# namespaces to your project namespace. Also reference the components namespace in your `MainWindow.xaml`:
```xml
<Window ...
    xmlns:c="clr-namespace:SnazzyWpfBorders.Components">
```
3. Then refer to the `MainWindow.xaml` in this project, to structure your file correctly. Something like this ...
```xml
<Window ...>
	<Grid>
		<!-- Add animations you want to use here -->
		<c:BorderLoop x:Name="BorderLoop" BColor="Yellow"></c:BorderLoop>
		
		<!-- Place your windows contents within this grid -->
		<Grid Margin="1">
			...
		</Grid>
	</Grid>
</Window>

```
You can change the animation colour by setting the xaml property `BColor`, and animation speed/duration by setting `Duration`, `EdgeDuration`, `FadeOutDuration` or `FadeOutDelay` where applicable.

C# codebehind to invoke the animations:
```cs
private void MyButtonClicked(object sender, RoutedEventArgs e)
{
	// Start the loop animation
	BorderLoop.Start();
}
```
The `<c:BorderDash>` and `<c:BorderTopDash>` animations can be stopped with the `.Stop()` method.


## Dependencies

[ModernWpf](https://github.com/Kinnara/ModernWpf) - While this is a complete UI framework, it is *only* used to removes the default WPF title bar and replace it with a UWP-like title bar. This allows the window borders to be customised. There are other methods of achieving the same thing, but ModernWpf was the easiest no-comprimises solution.

[MahApps.Metro](https://github.com/MahApps/MahApps.Metro) - Not strictly a dependency, I just copied their `GridLengthAnimation.cs` into the project to animate Grid column widths (or row heights).

## License

This code is released under MIT license. This means you can use this for whatever you want. Modify, distribute, sell, fork, and use this as much as you like. Both for personal and commercial use. I hold no responsibility if anything goes wrong.  
  
If you use this, you don't need to refer to this repo, or give me any kind of credit but it would be appreciated. At least a :star: would be nice.  

It took longer than you think to publish and document this code for free. Perhaps you could consider buying me lunch?

[![paypal](https://www.paypalobjects.com/en_US/i/btn/btn_donateCC_LG.gif)](https://www.paypal.com/cgi-bin/webscr?cmd=_s-xclick&hosted_button_id=MLD56V6HQWCKU&source=url)

## Contributing

Pull Requests are welcome. But, note that by creating a pull request you are giving me permission to merge your code and release it under the MIT license mentioned above. At no point will you be able to withdraw merged code from the repository, or change the license under which it has been made available.