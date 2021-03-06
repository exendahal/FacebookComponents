#load "../common.ios.cake"
#load "../../common.cake"

var TARGET = Argument ("t", Argument ("target", "Default"));

SDK_URL = $"https://origincache.facebook.com/developers/resources/?id=FacebookSDKs-iOS-{SDK_VERSION}.zip";
SDK_FILE = $"FacebookSDKs.zip";
SDK_PATH = $"./externals/FacebookSDKs";
SDK_FRAMEWORKS = new [] { "AccountKit" };
SDK_BUNDLES = new [] { "AccountKitStrings" };

var buildSpec = new BuildSpec () {
	Libs = new ISolutionBuilder [] {
		new DefaultSolutionBuilder {
			SolutionPath = "./source/Facebook.AccountKit.sln",
			Configuration = "Release",
			BuildsOn = BuildPlatforms.Mac,
			OutputFiles = new [] { 
				new OutputFileCopy {
					FromFile = "./source/Facebook.AccountKit/bin/Release/Facebook.AccountKit.dll",
					ToDirectory = "./output/"
				}
			}
		}	
	},

	Samples = new ISolutionBuilder [] {
		new IOSSolutionBuilder {
			SolutionPath = "./samples/FBAccountKitSample/FBAccountKitSample.sln",
			Configuration = "Release",
			Platform = "iPhone",
			BuildsOn = BuildPlatforms.Mac }
	},

	NuGets = new [] {
		new NuGetInfo { NuSpec = "./nuget/Xamarin.Facebook.AccountKit.iOS.nuspec", Version = SDK_FULL_VERSION, RequireLicenseAcceptance = true, BuildsOn = BuildPlatforms.Mac },
	},

	Components = new [] {
		new Component { ManifestDirectory = "./component", BuildsOn = BuildPlatforms.Mac },
	},
};

SetupXamarinBuildTasks (buildSpec, Tasks, Task);

RunTarget (TARGET);
