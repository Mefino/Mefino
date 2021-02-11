# Mefino [![Mefino](https://raw.githubusercontent.com/Mefino/Mefino/main/img/logo_32x32.png)]()

<p align="center">
  Mefino is a work-in-progress Mod Manager for Outward.

  <a href="https://discord.gg/zKyfGmy7TR">
    <img src="https://img.shields.io/badge/Discord-chat-blue?style=social&logo=discord" />
  </a>
  
</p>


## How to use

* Outward must be closed for you to disable/enable any mod. Currently Mefino does not check if Outward is running so you'll probably just get an error for now. Next release will address it more gracefully.
* <b>Please remove (or make a backup of) all existing mods before installing / testing Mefino.</b> This is not technically required, but the safety of your non-Mefino files is not guaranteed while using Mefino.

1. Download `Mefino.exe` from the release page
2. Run it :)

## Temporary Release Guide

1. Create a new repository on GitHub for your package, the name of this repository will be the name for your release.
2. Create a file called `manifest.json` in the root of the repository using the example below, and push it to GitHub.
3. Put the contents of your release inside a zip file called `mefino-package.zip`. Your manifest does <b>not</b> need to be included in your actual release file. 
4. Make a new release, and include your `mefino-package.zip` in the release.
5. Create or edit the `README.md` file in your repository, and put the phrase `outward mefino mod` (case insensitive and special characters ignored) anywhere in the readme. See existing packages for possible examples, it doesn't really matter how you do it as long as your package now shows up in the Mefino browser.

For an example release, you can see [here](https://github.com/Mefino/Mefino.Plugin) for now.

### Manifest

* Please use a JSON validator such as [this](https://jsonlint.com/) to ensure your JSON is valid before a release or update. Be careful with the commas on your dependency and conflict lists.
* The file must be called `manifest.json`

```json
{
	"author": "",
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
	"override_folder": ""
}
```

`author` and `name` (string) <b>[optional]</b>
* Your GitHub username and Repository name, they are <b>not required</b> to be set for web releases. If you install a local package manually you <b>do</b> need to set these for Mefino to work properly.

`version` (string)
* The version of your latest release, eg `1.0.0.0`

`description` (string)
* A short description for your package, just one or two sentences.

`tags` (list of strings) <b>[optional]</b>
* A list of tags for your package, used for filtering on the "Browse Mods" tab of Mefino.
* You can <b>only</b> use these accepted tags (case insensitive): `Balancing`, `Characters`, `Items`, `Mechanics`, `Quests`, `Skills`, `Skill Trees`, `Utility`, `UI`
* You can suggest another tag in the Discord if you want.

`dependencies` (list of strings)
* A list of other package GUIDs which your package depends on.

`conflicts_with`  (list of strings)
* A list of other package GUIDs which your mod conflicts with (meaning they CANNOT be active at the same time).

`require_sync` (boolean)
* Whether your mod should be installed by all players online, it must be `true` or `false`. In the future this will be used for "automatic" online mod syncing.

`override_folder` (string) <b>[optional]</b>
* An optional special name you can use for the install directory instead of your `name`, ie. instead of installing to `plugins\{author} {name}\` it will install to `plugins\{author} {overide_folder}\`. This does not affect your GUID.

### Local installs

To install a package locally for testing:
1. Create a folder in the `BepInEx\plugins\` folder which matches your install folder path (either `{author} {name}` or `{author} {overide_folder}`).
2. Unzip your `mefino-package.zip` contents inside here
3. Put your `manifest.json` inside the folder too, and make sure you set the `author` and `name` correctly.

### Package GUIDs

Mefino package GUIDs are always `githubUsername repositoryName`, for example `sinai-dev Outward-SideLoader` or `Mefino Mefino.Plugin`.
* <i>Note the space between the author name and the repository name!</i>