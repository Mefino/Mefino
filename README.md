<p align="center">
  <img align="center" src="https://raw.githubusercontent.com/Mefino/Mefino/main/img/banner_350x128.png">
</p>

<p align="center">
  Mefino is a work-in-progress Mod Manager for Outward.<br><br>

  <a href="https://discord.gg/zKyfGmy7TR">
    <img src="https://img.shields.io/discord/293810842225606656.svg?color=7289da&label=Discord&logo=discord&style=flat-square" />
  </a>
  
</p>

## How to use

* NOTE: <b>For now, please remove (or make a backup of) all existing mods before installing / testing Mefino.</b> This is not technically required, but the safety of your non-Mefino files is not guaranteed while using Mefino in alpha phase.

1. Download `Mefino.exe` from the release page
2. Run it :) 

<i>If Windows SmartScreen warns you about the file you can disable these warnings through Windows Security > App & Browser Control > Reputation-based protection > Off. The source code for the Mefino launcher is available in its entirety in the `src` folder should you want to rebuild it yourself.</i>

## Temporary Release Guide

1. Create a new repository on GitHub for your package, or use an existing one if you have one for the mod already.
2. Create a file called `mefino-manifest.json` in the root folder of the repository using the example below, and push it to GitHub.
3. Put the contents of your release inside a zip file. <b>The name of the zip is important, it must match the `download_filename` of the package (or the `name` if you do not set a filename).</b>
4. Make a new release in your repository, and include your zip file in the release.
5. Create or edit the `README.md` file in your repository, and put the phrase `outward mefino mod` (case insensitive and special characters ignored) anywhere in the readme. See existing packages for possible examples, it doesn't really matter how you do it as long as your package now shows up in the Mefino browser.

For an example release, you can see [here](https://github.com/Mefino/Mefino.Plugin) for now.

### Package GUIDs

Mefino uses GUIDs (global unique IDs) to identify packages. Half of the GUID is determined by your GitHub username (or `author` for manually-installed packages), and the other half is determined by the `name` for your package.

The format of the GUID is `{author} {name}`. <i>Note the space between the author name and the repository name.</i> For example `sinai-dev SideLoader` or `Mefino Mefino.Plugin`. 

### Manifest

* Please use a JSON validator such as [this](https://jsonlint.com/) to ensure your JSON is valid before a release or update. Be careful with the commas on your dependency and conflict lists.
* The file must be called `mefino-manifest.json`

```json
{
	"author": "",
	"repository": "",
	"name": "",
	"version": "",
	"description": "",
	"tags": [
		"Mechanics",
		"UI",
		"Skills"
	],
	"dependencies": [
		"someAuthor someRepository",
		"someAuthor someRepository"
	],
	"conflicts_with": [
		"someGuy someMod"
	],
	"require_sync": false,
	"download_filename": ""
}
```

#### Required

`name`
* The name for your mod or library. This will affect the install folder of your package, and will be the display name in the Mefino launcher.

`version` (string)
* The version of your latest release, eg `1.0.0.0`
* You must use Semantic versioning (`Major.Minor.Patch.Build`), though only `Major` is required.

`description` (string)
* A short description for your package, just one or two sentences.

#### Optional

`author` and `repository` (string)
* Your GitHub Username and the Repository name where this package is hosted. They are <b>not required</b> to be set for web releases, but if you install a local package manually you <b>do</b> need to set `author` for Mefino to work properly.

`tags` (list of strings)
* A list of tags for your package, used for filtering on the "Browse Mods" tab of Mefino.
* You can <b>only</b> use these accepted tags (case insensitive): `Balancing`, `Characters`, `Classes`, `Items`, `Library`, `Mechanics`, `Quests`, `Skills`, `Utility`, `UI`
* You can suggest another tag in the Discord if you want.
* The `Library` tag will mean your package is hidden unless the user enables "Show Libraries".

`dependencies` (list of strings)
* A list of other package GUIDs which your package depends on.

`conflicts_with`  (list of strings)
* A list of other package GUIDs which your mod conflicts with (meaning they CANNOT be active at the same time).

`require_sync` (boolean)
* Whether your mod should be installed by all players online, it must be `true` or `false`. In the future this will be used for "automatic" online mod syncing.

`download_filename` (string)
* You can use this to set an alternate filename for your package download, ie. Mefino will try to download `<download_filename>.zip`. You will need to upload a zip file with this name in your latest release for the repository. If you don't set this, Mefino will expect to find a `<name>.zip` file in your latest release instead. 

### Multi-package Repositories

You can host multiple packages in one repository by using a different type of manifest.

You can do this by defining a `packages` list in your JSON instead, your `mefino-manifest.json` should look like this:

Notes: 
* `//`-Comments are not valid JSON.
* You will need to set a unique `download_filename` for each package.

```json
{
  "packages": [
    {
      "name": "MyPlugin1",
      // ... etc, fill out manifest as normal here
    },
	{
	  "name": "MyOtherPlugin",
	  // etc...
	} // remember to NOT put a comma after your last package.
  ]
}
```

### Local installs

To install a package locally for testing:
1. Create a folder in the `BepInEx\plugins\` folder which matches your GUID (`{author} {name}`).
2. Put your mefino package contents inside here
3. Put your `mefino-manifest.json` (must be a singular manifest, not a multi-package manifest) inside the folder too, and make sure you set the `author` and `name` correctly.
