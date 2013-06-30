Togglr
======
Just another feature toggle library for .net

Usage
======
Each feature toggle is defined as a c# class that inherites from the "FeatureToggle" -class provided by Togglr-framework.

Definition of a sample feature toggle class:

<pre>
public class SampleFeatureToggle : FeatureToggle
{

}
</pre>

...done.

If you're using the default configuration then you also need to add the following line to FeatureToggles.txt located in your App_Data folder:

<pre>Sample=on</pre>

You now have a feature toggle called "Sample" that is turned on. To turn it off just change the above line in FeatureToggles.txt to:

<pre>Sample=off</pre>

You can override the default identity of your feature toggles (which is the name of the class) by doing the following:

<pre>
public class SampleFeatureToggle : FeatureToggle
{
  public override string Id
  {
    get { return "the new identifier"; }
  }
}
</pre>

You can also override the mono state pattern and create your own implementation of how the value of a feature toggle is retrieved. In the following example an always-on feature toggle has been created:

<pre>
public class SampleFeatureToggle : FeatureToggle
{
  public override bool IsEnabled
  {
    get { return true; }
  }
}
</pre>

You can also temporarily override the value of a feature toggle in a web-request by added a parameter to the requested url. This enables you to get feedback on a given feature by sending a link to a friend/customer where a given feature is temporarily turned on. To do that you just add the parameter "EnableFeature" to the requested url and then you write a comma seperated list of the feature toggle identifiers that you wish to temporarily enable. In the following example the "Sample" feature toggle has been disabled (noted as Sample=off in the FeatureToggles.txt file):

<pre>http://your-application-url?EnableFeature=Sample</pre>

A request to the url above will temporarily enable the "Sample" feature toggle. You can the send that link to reviewers/approvers/etc.

And remember - you can seperate the identifiers by a comma like this:

<pre>http://your-application-url?EnableFeature=Sample1,Sample2,Sample3</pre>

The Togglr framework comes with extensions for the HtmlHelper class that is defined in the MVC framework. This lets you define the markup for your features in the following ways:

Add your feature as a partial view and instead of calling @Html.Partial("partialViewName) you would do @Html.PartialAsFeature("partialViewName", new SampleFeatureToggle())

Add your feature as "inline" by defining it inside an if-scope like this:

<pre>
@if (Html.IsFeatureEnabled(new SampleFeatureToggle()))
{
  &lt;h1&gt;this is markup for the sample feature&lt;/h1&gt;
}
</pre>

You have to include the following using-statement to your views:

<pre>using Togglr.Extensions</pre>

...I would suggest that you add the namespace to your web.config though.

How it works
======
Each feature toggle is based on the mono state pattern. This means that you can (and should if needed) instantiate your feature toggles where ever you need to verify that a given feature is enabled or disabled. You just "new up" the targeted feature toggle and access its "IsEnabled" property. The mono state pattern enables that two or more instances of the same feature toggle class will return the same value from its "IsEnabled" property. 

The framework will use the name of the feature toggle class as an identifier when retrieving the stored feature toggle state. It will ignore "FeatureToggle" in the end of an identifier. This means that a class named "SampleFeatureToggle" will have "Sample" as its identifier by default.

Feature toggle values are stored in a file located in your projects App_Data folder. This is the default configuration that you get out-of-the-box. The file format is very simple (see the section "Default file format" for more details). You add new feature toggles (and their value) to this file when ever you create new feature toggles. But you are not stuck with the default configuration. You can store the feature toggle values where ever you want and then just create a value provider that plugges into the Togglr-framework (see the section "Custom value provider" for more details).

Default file format
======
Each feature toggle is defined on its own line in the text file. The simple format of a feature toggle line is [identifier]=[on|off] as shown here:

<pre>
Sample1=on
Sample2=off
</pre>

You can also add comments to the file by starting the line with a #-sign as shown here:

<pre>
# front page widgets
Sample1=on
Sample2=off

# about page widgets
Sample3=on
Sample4=off
</pre>

Custom value provider
======
If you want to store your feature toggle values in a database you just create your own implementation of IFeatureToggleValueProvider and plug that into the Togglr engine when your application starts.

<pre>
public class CustomValueProvider : IFeatureToggleValueProvider
{
  public FeatureToggleValue GetById(string id)
  {
    // access your database here...
    // return an instance of FeatureToggleValue
  }
}
</pre>

Plug the value provider into the Togglr engine by creating a new configuration and apply that to the engine:

<pre>
IConfiguration cfg = new ConfigurationBuilder()
  .WithValueProvider(new CustomValueProvider())
  .Build();

TogglrEngine.ApplyConfiguration(cfg);
</pre>

...done.
